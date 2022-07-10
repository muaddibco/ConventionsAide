namespace ConventionsAide.Core.Contracts
{
    public class PagedResultRequestDto
    {
        public static int DeafultMaxCount { get; } = 10;
        public virtual int SkipCount { get; set; } = 0;
        public virtual int MaxCount { get; set; } = DeafultMaxCount;
    }
}
