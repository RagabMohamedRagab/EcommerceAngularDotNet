using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.core.Dtos
{


    public class AddProudct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal OldPrice { get; set; }
        public int NewPrice { get; set; }
        public int Quantity { get; set; }
        public int? CategoryId { get; set; }

        public IFormFileCollection file {  get; set; }
    }    
    
    
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
