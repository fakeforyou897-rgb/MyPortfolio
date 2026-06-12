using MyPortfolio.Models.Common;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Models.ViewModels
{
    /// <summary>
    /// Query parameters for filtering and searching contact messages
    /// </summary>
    public class ContactMessageQueryParameters : QueryParameters
    {
        public MessageStatus? Status { get; set; }
        
        public Priority? Priority { get; set; }
        
        public bool? IsRead { get; set; }
        
        public bool? IsReplied { get; set; }
        
        public bool? IsSpam { get; set; }
        
        public DateTime? SentFrom { get; set; }
        
        public DateTime? SentTo { get; set; }
        
        public string? Email { get; set; }
    }
}
