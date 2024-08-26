using MVCPronia.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCPronia.Areas.ProniaAdmin.ViewModels.Product
{
    public class UpdateProductVM
    {

        public IFormFile? MainPhoto { get; set; }
        public IFormFile? HoverPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public int? CategoryId { get; set; }
        public List<int>? TagIds { get; set; }
        public List<int>? ColorIds { get; set; }
        public List<int>? SizeIds { get; set; }


        public List<int>? ImageIds { get; set; }

        [Required]
        public decimal? Price { get; set; }
        public string SKU { get; set; }

        public List<Category>? Categories { get; set; }
        public List<Tag>? Tags { get; set; }
        public List<Size>? Sizes { get; set; }
        public List<Color>? Colors { get; set; }

        public List<ProductImage>? ImageURL { get; set; }
    }
}
