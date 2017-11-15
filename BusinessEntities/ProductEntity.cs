using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class ProductEntity
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Brand { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }

    public class DropDownProduct
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
