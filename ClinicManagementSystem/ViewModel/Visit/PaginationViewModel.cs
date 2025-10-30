namespace ClinicManagementSystem.ViewModel.Visit
{
    public class PaginationViewModel<T>
    {
        public IEnumerable<T> items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
