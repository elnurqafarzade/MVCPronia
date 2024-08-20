using System.ComponentModel.DataAnnotations;

namespace MVCPronia.Models
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "Ad mutleqdir.")]
        [MaxLength(25, ErrorMessage = "Uzunlugu 25den cox ola bilmez")]
        public string Name { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
