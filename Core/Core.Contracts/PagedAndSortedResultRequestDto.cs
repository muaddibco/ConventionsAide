namespace ConventionsAide.Core.Contracts
{
    public class PagedAndSortedResultRequestDto : PagedResultRequestDto
    {
        public virtual string? Sorting { get; set; }
    }
}
