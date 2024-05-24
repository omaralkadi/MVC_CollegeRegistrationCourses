namespace CompanyMvc.ViewModels
{
    public class PaginationVM<T> where T : class
    {
        public IEnumerable<T> Entity { get; set; }
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }
}
