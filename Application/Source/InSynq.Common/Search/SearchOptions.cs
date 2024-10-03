using InSynq.Common.Sorting;

namespace InSynq.Common.Search;

public class SearchOptions
{
    public int Skip { get; set; }

    public int Take { get; set; }

    public List<SortingOptions> SortingOptions { get; set; }

    public string Filter { get; set; }

    public bool? TotalCount { get; set; }
}