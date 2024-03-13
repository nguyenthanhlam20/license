﻿namespace ViewModels
{
    public class PagingRequestBase<T>
    {
        public string? SearchTerm { get; set; } = "";
        public string? OldSearchTerm { get; set; } = "";
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; } = 0;
        public int TotalRecord { get; set; } = 0;
        public bool HasNext
        {
            get
            {
                return (CurrentPage < TotalPages);
            }
        }
        public bool HasPrevious
        {
            get
            {
                return (CurrentPage > 1);
            }
        }
        public List<T>? Items { get; set; }
    }
}