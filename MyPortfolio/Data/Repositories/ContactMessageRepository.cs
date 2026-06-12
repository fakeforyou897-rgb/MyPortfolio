using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Data.Repositories
{
    public class ContactMessageRepository : Repository<ContactMessage>, IContactMessageRepository
    {
        public ContactMessageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(m => !m.IsRead)
                .OrderByDescending(m => m.SentAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ContactMessage>> GetMessagesByStatusAsync(MessageStatus status, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(m => m.Status == status)
                .OrderByDescending(m => m.SentAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ContactMessage>> GetRecentMessagesAsync(int count, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .OrderByDescending(m => m.SentAt)
                .Take(count)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetUnreadCountAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(m => !m.IsRead, cancellationToken);
        }

        public async Task MarkAsReadAsync(Guid messageId, CancellationToken cancellationToken = default)
        {
            var message = await GetByIdAsync(messageId, cancellationToken);
            if (message != null && !message.IsRead)
            {
                message.IsRead = true;
                message.ReadAt = DateTime.UtcNow;
                message.Status = MessageStatus.Read;
                Update(message);
            }
        }

        public async Task MarkAsRepliedAsync(Guid messageId, string reply, string repliedBy, CancellationToken cancellationToken = default)
        {
            var message = await GetByIdAsync(messageId, cancellationToken);
            if (message != null)
            {
                message.IsReplied = true;
                message.RepliedAt = DateTime.UtcNow;
                message.Reply = reply;
                message.RepliedBy = repliedBy;
                message.Status = MessageStatus.Replied;
                Update(message);
            }
        }
    }
}
