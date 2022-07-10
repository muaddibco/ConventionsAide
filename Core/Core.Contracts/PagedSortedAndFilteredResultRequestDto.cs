namespace ConventionsAide.Core.Contracts
{
    public class PagedSortedAndFilteredResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
