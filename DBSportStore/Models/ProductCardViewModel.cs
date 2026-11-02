using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
//viewmodel tam chua dl hien thi tren Product Card
namespace DBSportStore.Models
{
    public class ProductCardViewModel
    {
        public int ProductId { get; set; } //ma sp
        public string NamePro { get; set; } //ten sp
        public string CategoryName { get; set; } //ten danh muc
        public decimal Price { get; set; } //gia sp
        public string ImagePro { get; set; } //hinh anh sp
        public string ShortDescription { get; set; } //mo ta sp
        
    }
}