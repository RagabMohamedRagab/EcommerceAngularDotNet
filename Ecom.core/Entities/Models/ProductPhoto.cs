using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.core.Entities.Models
{
    public class ProductPhoto:BaseEntity<int>
    {
     

        public string ImageName {  get; set; }

        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
