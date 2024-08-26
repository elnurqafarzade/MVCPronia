using MVCPronia.Models;


namespace MVCPronia.Areas.ProniaAdmin.ViewModels.Product
{
    public class GetAdminProductVM
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string ImageURL { get; set; }
    }
}
