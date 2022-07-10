namespace ConventionsAide.Core.Contracts
{
    public class LimitedResultRequestDto
    {
        public static int DefaultMaxResultCount { get; set; } = 10;
        public static int MaxMaxResultCount { get; set; } = 1000;
        public virtual int MaxResultCount { get; set; } = DefaultMaxResultCount;
    }
}