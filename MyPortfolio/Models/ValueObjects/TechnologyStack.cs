using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPortfolio.Models.ValueObjects
{
    /// <summary>
    /// Value object representing a technology stack
    /// </summary>
    public class TechnologyStack : IEquatable<TechnologyStack>
    {
        public IReadOnlyList<string> Technologies { get; private set; }

        private TechnologyStack(IEnumerable<string> technologies)
        {
            Technologies = technologies.ToList().AsReadOnly();
        }

        public static TechnologyStack Create(IEnumerable<string> technologies)
        {
            if (technologies == null || !technologies.Any())
                throw new ArgumentException("Technology stack must contain at least one technology", nameof(technologies));

            var cleanedTechs = technologies
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => t.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (!cleanedTechs.Any())
                throw new ArgumentException("Technology stack must contain at least one valid technology", nameof(technologies));

            return new TechnologyStack(cleanedTechs);
        }

        public static TechnologyStack CreateFromString(string technologies, char separator = ',')
        {
            if (string.IsNullOrWhiteSpace(technologies))
                throw new ArgumentException("Technologies string cannot be empty", nameof(technologies));

            var techList = technologies.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return Create(techList);
        }

        public string ToCommaSeparatedString()
        {
            return string.Join(", ", Technologies);
        }

        public bool Contains(string technology)
        {
            return Technologies.Any(t => t.Equals(technology, StringComparison.OrdinalIgnoreCase));
        }

        public bool Equals(TechnologyStack? other)
        {
            if (other is null) return false;
            return Technologies.SequenceEqual(other.Technologies, StringComparer.OrdinalIgnoreCase);
        }

        public override bool Equals(object? obj)
        {
            return obj is TechnologyStack stack && Equals(stack);
        }

        public override int GetHashCode()
        {
            return Technologies.Aggregate(0, (current, tech) => current ^ tech.GetHashCode());
        }

        public override string ToString()
        {
            return ToCommaSeparatedString();
        }

        public static implicit operator string(TechnologyStack stack) => stack.ToCommaSeparatedString();
    }
}
