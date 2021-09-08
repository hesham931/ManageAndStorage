using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ManageAndStorage.Models
{
    public class Product
    {
        public int id { get; set; }
        public int ProductId { get; set; }//forgion key return to BussniessType(id)
        public int DisplayId { get; set; }
        public string Name { get; set; }

        [DisplayName("Sale Price")]
        public double SalePrice { get; set; }

        [DisplayName("Buying Price")]
        public double BuyingPrice { get; set; }

        [DisplayName("Avaiable Items")]
        public int AvilableItems { get; set; }

        [DisplayName("Item Location")]
        public string ItemLocation { get; set; }
        //localization languages
    }
}