using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBSportStore.Models
{
    public class StoreHomeViewModel
    {
        //Dang sach dm lay truc tiep tu bang Category
        public List<Category> Categories { get; set; }
        //Danh sach san pham dang ban de trang chu tiep tuc hien thi
        public List<ProductCardViewModel> Products { get; set; }
        // Mã danh mục người dùng đang chọn.
        // int? cho phép null = không lọc danh mục.
        public int? SelectedCategoryId { get; set; }
        // Từ khóa người dùng đang tìm kiếm.
        public string SearchTerm { get; set; }// Trang hiện tại.
        public int CurrentPage { get; set; }

        // Tổng số trang.
        public int TotalPages { get; set; }
        // Giá tối thiểu người dùng nhập.
        // decimal? cho phép để trống ô Giá từ.
        public decimal? MinPrice { get; set; }

        // Giá tối đa người dùng nhập.
        // decimal? cho phép để trống ô Giá đến.
        public decimal? MaxPrice { get; set; }
        // Tổng số sản phẩm tìm thấy
        public int TotalItems { get; set; }
        public string SortOrder { get; set; }
    }
}