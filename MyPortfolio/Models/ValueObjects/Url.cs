using System;

namespace MyPortfolio.Models.ValueObjects
{
    /// <summary>
    /// Value object representing a URL
    /// </summary>
    public class Url : IEquatable<Url>
    {
        public string Value { get; private set; }

        private Url(string value)
        {
            Value = value;
        }

        public static Url Create(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be empty", nameof(url));

            url = url.Trim();

            if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult) ||
                (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
            {
                throw new ArgumentException("Invalid URL format", nameof(url));
            }

            return new Url(url);
        }

        public static Url? CreateOrDefault(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return null;

            try
            {
                return Create(url);
            }
            catch
            {
                return null;
            }
        }

        public bool Equals(Url? other)
        {
            if (other is null) return false;
            return Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            return obj is Url url && Equals(url);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(Url url) => url.Value;
    }
}
