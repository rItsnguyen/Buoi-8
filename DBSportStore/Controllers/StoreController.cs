using DBSportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace DBSportStore.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index(
    int? categoryId,
    string keyword,
    decimal? minPrice,
    decimal? maxPrice,
    string sortOrder,
    int page = 1)
        {
            const int pageSize = 6;

            if (page < 1)
            {
                page = 1;
            }

            // Lấy sản phẩm đang bán.
            var products = db.Product
                .Where(p => p.IsActive);

            // Lọc theo danh mục.
            if (categoryId.HasValue)
            {
                products = products.Where(
                    p => p.CategoryID == categoryId.Value);
            }

            // Tìm kiếm theo tên hoặc mô tả.
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();

                products = products.Where(
                    p => p.ProductName.Contains(keyword)
                      || p.Description.Contains(keyword));
            }

            // Lọc giá từ.
            if (minPrice.HasValue)
            {
                products = products.Where(
                    p => p.Price >= minPrice.Value);
            }

            // Lọc giá đến.
            if (maxPrice.HasValue)
            {
                products = products.Where(
                    p => p.Price <= maxPrice.Value);
            }

            // Sắp xếp sản phẩm.
            switch (sortOrder)
            {
                case "price_asc":
                    products = products
                        .OrderBy(p => p.Price)
                        .ThenBy(p => p.ProductID);
                    break;

                case "price_desc":
                    products = products
                        .OrderByDescending(p => p.Price)
                        .ThenByDescending(p => p.ProductID);
                    break;

                default:
                    // Mặc định: sản phẩm mới nhất trước.
                    products = products
                        .OrderByDescending(p => p.ProductID);
                    break;
            }

            // Đếm tổng sản phẩm sau khi lọc.
            int totalItems = products.Count();

            // Tính tổng số trang.
            int totalPages = (int)Math.Ceiling(
                (double)totalItems / pageSize);

            if (totalPages == 0)
            {
                totalPages = 1;
            }

            // Nếu page vượt quá số trang, chuyển về trang cuối.
            if (page > totalPages)
            {
                page = totalPages;
            }

            // Tạo ViewModel.
            var model = new StoreHomeViewModel
            {
                Categories = db.Category
                    .OrderBy(c => c.CategoryName)
                    .ToList(),

                // Phân trang sau khi lọc và sắp xếp.
                Products = products
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => new ProductCardViewModel
                    {
                        ProductId = p.ProductID,
                        NamePro = p.ProductName,
                        CategoryName = p.Category.CategoryName,
                        Price = p.Price,
                        ImagePro = p.ImageUrl
                    })
                    .ToList(),

                SortOrder = sortOrder,
                SelectedCategoryId = categoryId,
                SearchTerm = keyword,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                CurrentPage = page,
                TotalPages = totalPages,
                TotalItems = totalItems
            };

            return View(model);
        }

        public ActionResult AboutStore()
        {
            return View();
        }
        private DBSportStoreEntities db = new DBSportStoreEntities();
    }
}