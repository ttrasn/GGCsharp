using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace backend.components.pagination;

public class Request
{
    [DefaultValue(1)]
    [Range(1, int.MaxValue)]
    public int? Page { get; set; }

    [DefaultValue(20)] [Range(1, 1000)] public int? PageSize { get; set; }

    /// <summary>Comma seperated sorts, put minus(-) before column name for sorting result in descending, otherwise it will sort by ascending.(Example : -Id)</summary>
    public string? Sort { get; set; }
}