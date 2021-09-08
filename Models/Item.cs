using System;
using System.Globalization;
namespace ManageAndStorage.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int ItemId { get; set; }//forgion key referinced to Product(id)
        public string Name { get; set; }
        public DateTime SaleDate { get; set; }
        public int NumberOfItems { get; set; }
        public double Sum { get; set; }
        public int TempPage { get; set; }
    }
}