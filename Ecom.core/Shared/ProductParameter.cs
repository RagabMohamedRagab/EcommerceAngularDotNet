using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.core.Shared
{
    public record ProductParameter(string? sort,string? Search, int? CategoryId, int PageNumber, int pageSize = 5);
}
