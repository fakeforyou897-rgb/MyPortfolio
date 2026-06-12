using MyPortfolio.Models.Common;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Models.ViewModels
{
    /// <summary>
    /// Query parameters for filtering and searching projects
    /// </summary>
    public class ProjectQueryParameters : QueryParameters
    {
        public Guid? CategoryId { get; set; }
        
        public List<Guid>? TagIds { get; set; }
        
        public ProjectStatus? Status { get; set; }
        
        public Priority? Priority { get; set; }
        
        public bool? IsFeatured { get; set; }
        
        public bool? IsPublished { get; set; }
        
        public string? Technology { get; set; }
        
        public int? MinRating { get; set; }
        
        public int? MaxRating { get; set; }
        
        public DateTime? StartDateFrom { get; set; }
        
        public DateTime? StartDateTo { get; set; }
        
        public DateTime? EndDateFrom { get; set; }
        
        public DateTime? EndDateTo { get; set; }
    }
}
