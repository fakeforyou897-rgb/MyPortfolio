using MyPortfolio.Models.Common;
using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;
using MyPortfolio.Models.ViewModels;

namespace MyPortfolio.Services.Interfaces
{
    public interface IContactMessageService
    {
        // Query operations
        Task<ContactMessage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ContactMessage>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<ContactMessage>> GetUnreadAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<ContactMessage>> GetByStatusAsync(MessageStatus status, CancellationToken cancellationToken = default);
        Task<IEnumerable<ContactMessage>> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<IEnumerable<ContactMessage>> GetRecentAsync(int count, CancellationToken cancellationToken = default);
        Task<int> GetUnreadCountAsync(CancellationToken cancellationToken = default);
        
        // Search and filter
        Task<PagedResult<ContactMessage>> SearchAsync(ContactMessageQueryParameters parameters, CancellationToken cancellationToken = default);
        
        // Command operations
        Task<ContactMessage> CreateAsync(ContactMessageViewModel model, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> RestoreAsync(Guid id, CancellationToken cancellationToken = default);
        
        // Business operations
        Task<bool> MarkAsReadAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> MarkAsUnreadAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> UpdateStatusAsync(Guid id, MessageStatus status, CancellationToken cancellationToken = default);
        Task<bool> AddReplyAsync(Guid id, string reply, CancellationToken cancellationToken = default);
        
        // Validation
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
