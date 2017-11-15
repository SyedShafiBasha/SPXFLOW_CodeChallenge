using DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class ReviewEntity
    {
        public long Id { get; set; }

        [Required]
        [Range(1, 5)]
        public int? Rating { get; set; }
        [StringLength(256, MinimumLength = 3, ErrorMessage = "Please enter minimum three character ")]
        public string Comment { get; set; }
        public string User { get; set; }

        public string Product_Id { get; set; }

        public Product Product { get; set; }
    }
}
