using System.ComponentModel.DataAnnotations;

namespace MVCPronia.Models
{
	public class Size : BaseEntity
	{
        [Required]
        public string Name { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }

    }
}
