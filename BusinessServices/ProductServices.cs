using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public class ProductServices : IProductServices
    {

        private readonly UnitOfWork _unitOfWork;
        public ProductServices()
        {
            _unitOfWork = new UnitOfWork();
        }
        public IEnumerable<ProductEntity> GetAllProducts()
        {
            var products = _unitOfWork.ProductRepository.GetAll().ToList();

            if (products.Any())
            {
                Mapper.CreateMap<Product, ProductEntity>();
                var productModel = Mapper.Map<List<Product>, List<ProductEntity>>(products);
                return productModel;
            }
            return null;
        }

        public IEnumerable<ReviewEntity> GetAllReviewsBasedOnProduct(string productTitle)
        {
            var getProduct = _unitOfWork.ProductRepository.Get(p => p.Title.Trim() == productTitle);
            if (getProduct != null)
            {
                var reviews = _unitOfWork.ReviewRepository.GetMany(r => r.Product_Id == getProduct.ID).ToList();
                if (reviews.Any())
                {
                    Mapper.CreateMap<Review, ReviewEntity>();
                    var reviewModel = Mapper.Map<List<Review>, List<ReviewEntity>>(reviews);
                    return reviewModel;
                }
            }
            return null;
        }
        public IEnumerable<ReviewEntity> GetAllReviews()
        {
            var reviews = _unitOfWork.ReviewRepository.GetAll().ToList();
            if (reviews.Any())
            {
                Mapper.CreateMap<Review, ReviewEntity>();
                var reviewModel = Mapper.Map<List<Review>, List<ReviewEntity>>(reviews);
                return reviewModel;
            }
            return null;
        }
        public long AddReview(ReviewEntity item)
        {
            var review = new Review
            {
                Comment = item.Comment,
                Rating = item.Rating,
                User = item.User
            };
            _unitOfWork.ReviewRepository.Insert(review);
            _unitOfWork.Save();
            return review.ID;

        }

        public bool UpdateReview(ReviewEntity item)
        {
            var result = false;
            if (item != null)
            {
                var review = _unitOfWork.ReviewRepository.GetByID(item.Id);
                review.Comment = item.Comment;
                review.User = item.User;
                review.Rating = item.Rating;
                _unitOfWork.ReviewRepository.Update(review);
                _unitOfWork.Save();
                result = true;
            }
            return result;
        }

        public bool DeleteReview(int reviewId)
        {
            var result = false;
            var review = _unitOfWork.ReviewRepository.GetByID(reviewId);
            if (review != null)
            {
                _unitOfWork.ReviewRepository.Delete(review);
                _unitOfWork.Save();
                result = true;
            }
            return result;
        }
    }
}
