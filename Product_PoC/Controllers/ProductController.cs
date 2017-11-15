using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataModel;
using BusinessEntities;
using BusinessServices;


namespace Product_PoC.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductServices _productServices;

        public ProductController()
        {
            _productServices = new ProductServices();
        }

        [HttpGet]
        [ActionName("GetAllProducts")]
        public HttpResponseMessage GetProducts()
        {
            var products = _productServices.GetAllProducts();
            if (products != null)
            {
                var productEntities = products as List<ProductEntity> ?? products.ToList();
                if (productEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, productEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Products not found");
        }

        [HttpGet]
        [ActionName("GetAllReviews")]
        public HttpResponseMessage GetReviews()
        {
            var reviews = _productServices.GetAllReviews();
            if (reviews != null)
            {
                var reviewsEntities = reviews as List<ReviewEntity> ?? reviews.ToList();
                if (reviewsEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, reviewsEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Products not found");
        }

        [HttpGet]
        [ActionName("GetAllReviewsBasedOnProduct")]
        public HttpResponseMessage GetReviws(string productTitle)
        {
            var reviews = _productServices.GetAllReviewsBasedOnProduct(productTitle);
            if (reviews != null)
            {
                var reivewEntities = reviews as List<ReviewEntity> ?? reviews.ToList();
                if (reivewEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, reivewEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No reviews found");
        }



        [ActionName("NewProductReview")]
        public HttpResponseMessage Post([FromBody] ReviewEntity reviewEntity)
        {
            if (reviewEntity != null && reviewEntity.Id == 0)
            {
                var resultId = _productServices.AddReview(reviewEntity);
                if (resultId > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Review not found");
        }

        [ActionName("UpdateProductReview")]
        public HttpResponseMessage Put([FromBody]ReviewEntity reviewEntity)
        {
            if (reviewEntity != null && reviewEntity.Id > 0)
            {
                var updateResult = _productServices.UpdateReview(reviewEntity);
                if (updateResult)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Review not found for update"); ;
        }

        [ActionName("DeleteProductReview")]
        public HttpResponseMessage Delete(int id)
        {
            if (id > 0)
            {
                var deleteResult = _productServices.DeleteReview(id);
                if (deleteResult)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No review found to delete");
        }

    }
}