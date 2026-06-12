namespace MyPortfolio.Models.Entities
{
    /// <summary>
    /// Many-to-many relationship between BlogPost and Tag
    /// </summary>
    public class BlogPostTag
    {
        public Guid BlogPostId { get; set; }
        public virtual BlogPost BlogPost { get; set; } = null!;

        public Guid TagId { get; set; }
        public virtual Tag Tag { get; set; } = null!;
    }
}
