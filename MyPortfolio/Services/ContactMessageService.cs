using MyPortfolio.Data.UnitOfWork;
using MyPortfolio.Extensions;
using MyPortfolio.Models.Common;
using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;
using MyPortfolio.Models.ViewModels;
using MyPortfolio.Services.Interfaces;

namespace MyPortfolio.Services
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingService _mappingService;

        public ContactMessageService(IUnitOfWork unitOfWork, IMappingService mappingService)
        {
            _unitOfWork = unitOfWork;
            _mappingService = mappingService;
        }

        // Query operations
        public async Task<ContactMessage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ContactMessages.GetByIdAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<ContactMessage>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ContactMessages.GetAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<ContactMessage>> GetUnreadAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ContactMessages.FindAsync(m => !m.IsRead, cancellationToken);
        }

        public async Task<IEnumerable<ContactMessage>> GetByStatusAsync(MessageStatus status, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ContactMessages.FindAsync(m => m.Status == status, cancellationToken);
        }

        public async Task<IEnumerable<ContactMessage>> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ContactMessages.FindAsync(m => m.Email == email, cancellationToken);
        }

        public async Task<IEnumerable<ContactMessage>> GetRecentAsync(int count, CancellationToken cancellationToken = default)
        {
            var query = _unitOfWork.ContactMessages.Query()
                .OrderByDescending(m => m.CreatedAt)
                .Take(count);
            
            return await query.ToListAsync();
        }

        public async Task<int> GetUnreadCountAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ContactMessages.CountAsync(m => !m.IsRead, cancellationToken);
        }

        // Search and filter
        public async Task<PagedResult<ContactMessage>> SearchAsync(ContactMessageQueryParameters parameters, CancellationToken cancellationToken = default)
        {
            var query = _unitOfWork.ContactMessages.Query();

            // Apply search
            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.ApplySearch(parameters.SearchTerm, m => m.Name, m => m.Email, m => m.Subject, m => m.Message);
            }

            // Apply filters
            if (parameters.Status.HasValue)
            {
                query = query.Where(m => m.Status == parameters.Status.Value);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Email))
            {
                query = query.Where(m => m.Email == parameters.Email);
            }

            if (parameters.IsRead.HasValue)
            {
                query = query.Where(m => m.IsRead == parameters.IsRead.Value);
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(parameters.SortBy))
            {
                query = query.ApplySort(parameters.SortBy, parameters.SortDescending);
            }
            else
            {
                query = query.OrderByDescending(m => m.CreatedAt);
            }

            // Apply pagination
            return await query.ToPagedResultAsync(parameters.PageNumber, parameters.PageSize, cancellationToken);
        }

        // Command operations
        public async Task<ContactMessage> CreateAsync(ContactMessageViewModel model, CancellationToken cancellationToken = default)
        {
            var message = _mappingService.MapToEntity(model);

            await _unitOfWork.ContactMessages.AddAsync(message, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return message;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var message = await GetByIdAsync(id, cancellationToken);
            if (message == null)
                return false;

            _unitOfWork.ContactMessages.Remove(message);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var message = await GetByIdAsync(id, cancellationToken);
            if (message == null)
                return false;

            _unitOfWork.ContactMessages.SoftDelete(message);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> RestoreAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var message = await _unitOfWork.ContactMessages.GetByIdAsync(id, cancellationToken);
            if (message == null)
                return false;

            _unitOfWork.ContactMessages.Restore(message);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        // Business operations
        public async Task<bool> MarkAsReadAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var message = await GetByIdAsync(id, cancellationToken);
            if (message == null)
                return false;

            message.IsRead = true;
            _unitOfWork.ContactMessages.Update(message);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> MarkAsUnreadAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var message = await GetByIdAsync(id, cancellationToken);
            if (message == null)
                return false;

            message.IsRead = false;
            _unitOfWork.ContactMessages.Update(message);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> UpdateStatusAsync(Guid id, MessageStatus status, CancellationToken cancellationToken = default)
        {
            var message = await GetByIdAsync(id, cancellationToken);
            if (message == null)
                return false;

            message.Status = status;
            _unitOfWork.ContactMessages.Update(message);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> AddReplyAsync(Guid id, string reply, CancellationToken cancellationToken = default)
        {
            var message = await GetByIdAsync(id, cancellationToken);
            if (message == null)
                return false;

            message.Reply = reply;
            message.Status = MessageStatus.Replied;
            _unitOfWork.ContactMessages.Update(message);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        // Validation
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ContactMessages.AnyAsync(m => m.Id == id, cancellationToken);
        }
    }
}
