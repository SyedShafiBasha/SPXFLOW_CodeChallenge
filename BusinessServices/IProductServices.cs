using BusinessEntities;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IProductServices
    {
        IEnumerable<ProductEntity> GetAllProducts();
        IEnumerable<ReviewEntity> GetAllReviews();
        IEnumerable<ReviewEntity> GetAllReviewsBasedOnProduct(string productTitle);
        long AddReview(ReviewEntity item);
        bool UpdateReview(ReviewEntity item);
        bool DeleteReview(int id);
    }
}
