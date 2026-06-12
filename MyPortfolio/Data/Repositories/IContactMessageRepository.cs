using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Data.Repositories
{
    public interface IContactMessageRepository : IRepository<ContactMessage>
    {
        Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<ContactMessage>> GetMessagesByStatusAsync(MessageStatus status, CancellationToken cancellationToken = default);
        Task<IEnumerable<ContactMessage>> GetRecentMessagesAsync(int count, CancellationToken cancellationToken = default);
        Task<int> GetUnreadCountAsync(CancellationToken cancellationToken = default);
        Task MarkAsReadAsync(Guid messageId, CancellationToken cancellationToken = default);
        Task MarkAsRepliedAsync(Guid messageId, string reply, string repliedBy, CancellationToken cancellationToken = default);
    }
}
