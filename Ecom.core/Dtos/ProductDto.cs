using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.core.Dtos
{


    public record AddProudct(string Name, string Description, decimal Price, int Quantity,int? CategoryId);
    public class ProductDto {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public List<PhottoDto> ProductPhotos { get; set; }
        public CategoryDto Category { get; set; }
    }
    public class PhottoDto
    {
        public string ImageName { get; set; }

        public int? ProductId { get; set; }
    }
}
