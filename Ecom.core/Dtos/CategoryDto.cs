using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.core.Dtos
{
    public record CategoryDto(string Name,string Description);

    public record UpdateCategory(string Name, string Description,int id);
}
