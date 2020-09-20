using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Models
{
    public class ProductType
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2,ErrorMessage ="Min length is 2 characters")]
        [MaxLength(30,ErrorMessage ="Max length is 30 characters")]
        
        public string Name { get; set; }
    }
}
