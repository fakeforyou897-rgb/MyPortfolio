using MyPortfolio.Models.Common;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Models.ViewModels
{
    /// <summary>
    /// Query parameters for filtering and searching blog posts
    /// </summary>
    public class BlogPostQueryParameters : QueryParameters
    {
        public Guid? CategoryId { get; set; }
        
        public List<Guid>? TagIds { get; set; }
        
        public BlogPostStatus? Status { get; set; }
        
        public bool? IsFeatured { get; set; }
        
        public bool? IsPublished { get; set; }
        
        public string? Author { get; set; }
        
        public DateTime? PublishedFrom { get; set; }
        
        public DateTime? PublishedTo { get; set; }
        
        public int? MinReadingTime { get; set; }
        
        public int? MaxReadingTime { get; set; }
        
        public bool? AllowComments { get; set; }
    }
}
