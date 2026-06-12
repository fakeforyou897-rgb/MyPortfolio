using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models.Common
{
    /// <summary>
    /// Base query parameters for pagination, sorting, and filtering
    /// </summary>
    public class QueryParameters
    {
        private const int MaxPageSize = 100;
        private int _pageSize = 10;

        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, MaxPageSize)]
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public string? SearchTerm { get; set; }
        
        public string? SortBy { get; set; }
        
        public string SortOrder { get; set; } = "asc"; // "asc" or "desc"

        public bool IsDescending => SortOrder?.ToLower() == "desc";
    }
}
