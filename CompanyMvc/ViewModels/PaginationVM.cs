namespace CompanyMvc.ViewModels
{
    public class PaginationVM
    {
        public IEnumerable<EmployeeVM> Employees { get; set; }
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }
}
