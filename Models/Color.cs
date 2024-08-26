using System.ComponentModel.DataAnnotations;

namespace MVCPronia.Models
{
	public class Color : BaseEntity
	{
        [Required]
        public string Name { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }

    }
}
