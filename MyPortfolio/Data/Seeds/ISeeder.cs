using Microsoft.EntityFrameworkCore;

namespace MyPortfolio.Data.Seeds
{
    /// <summary>
    /// Interface for seeding initial data into the database
    /// </summary>
    public interface ISeeder
    {
        /// <summary>
        /// Seed method called during model creation
        /// </summary>
        /// <param name="modelBuilder">The model builder to add seed data to</param>
        void Seed(ModelBuilder modelBuilder);

        /// <summary>
        /// Order in which seeder should execute (lower numbers execute first)
        /// </summary>
        int Order { get; }
    }
}
