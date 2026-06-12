namespace MyPortfolio.Models.Entities
{
    /// <summary>
    /// Many-to-many relationship between Project and Tag
    /// </summary>
    public class ProjectTag
    {
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; } = null!;

        public Guid TagId { get; set; }
        public virtual Tag Tag { get; set; } = null!;
    }
}
