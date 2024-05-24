namespace CompanyMvc.ViewModels
{
    public class UserVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public IEnumerable<string> Roles { get; set; }=new List<string>();
    }
}
