using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MyPortfolio.Data.Seeds
{
    /// <summary>
    /// Master seeder that orchestrates all database seeders
    /// Automatically discovers and executes all ISeeder implementations
    /// </summary>
    public class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Get all seeder types that implement ISeeder
            var seeders = GetSeedersFromAssembly()
                .OrderBy(s => s.Order)
                .ToList();

            // Execute each seeder in order
            foreach (var seeder in seeders)
            {
                seeder.Seed(modelBuilder);
            }
        }

        /// <summary>
        /// Discovers all ISeeder implementations in the current assembly
        /// </summary>
        /// <returns>List of seeder instances ordered by execution order</returns>
        private static List<ISeeder> GetSeedersFromAssembly()
        {
            var seeders = new List<ISeeder>();
            var assembly = typeof(DataSeeder).Assembly;

            // Find all types that implement ISeeder
            var seederTypes = assembly.GetTypes()
                .Where(t => typeof(ISeeder).IsAssignableFrom(t) && 
                           t != typeof(ISeeder) &&
                           !t.IsAbstract);

            // Create instance of each seeder
            foreach (var seederType in seederTypes)
            {
                try
                {
                    var instance = Activator.CreateInstance(seederType) as ISeeder;
                    if (instance != null)
                    {
                        seeders.Add(instance);
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(
                        $"Failed to instantiate seeder '{seederType.Name}'. " +
                        $"Make sure it has a parameterless constructor.",
                        ex);
                }
            }

            return seeders;
        }
    }
}
