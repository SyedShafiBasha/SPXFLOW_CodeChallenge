using BusinessEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Product_PoC.Controllers
{
    public class HomeController : Controller
    {
        static HttpClient client = new HttpClient();
        static HomeController()
        {
            GetBaseUrl();
        }
        public async Task<ActionResult> Index()
       {
            List<ProductEntity> productInfo = new List<ProductEntity>();
            HttpResponseMessage response = await client.GetAsync("api/Product/GetAllProducts");
            if (response.IsSuccessStatusCode)
            {
                var productResponse = response.Content.ReadAsStringAsync().Result;
                productInfo = JsonConvert.DeserializeObject<List<ProductEntity>>(productResponse);

            }
            return View(productInfo);
        }

        public ActionResult Edit(ReviewEntity reviewId)
        {
            return View(reviewId);
        }
        [HttpPost]
        public async Task<ActionResult> Update(ReviewEntity reviewId)
        {
            var response = await client.PutAsJsonAsync("api/Product/UpdateProductReview", reviewId);
            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult CreateNewProductReview()
        {

            return View();
        }

        static void GetBaseUrl()
        {
            client.BaseAddress = new Uri("http://localhost:57082/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet]
        public async Task<JsonResult> GetProductDetails()
        {
            var list = new List<DropDownProduct>();
            DropDownProduct dropdownProduct = new DropDownProduct();
            HttpResponseMessage response = await client.GetAsync("api/Product/GetAllProducts");
            if (response.IsSuccessStatusCode)
            {
                var productResponse = response.Content.ReadAsStringAsync().Result;
                var listOfProduct = JsonConvert.DeserializeObject<List<ProductEntity>>(productResponse);
                foreach (var item in listOfProduct)
                {
                    list.Add(new DropDownProduct
                    {
                        ProductId = item.ID,
                        ProductName = item.Title
                    });
                }
            }
            return Json(new SelectList(list, "Value", "Text"), JsonRequestBehavior.AllowGet);
        }
    }
}