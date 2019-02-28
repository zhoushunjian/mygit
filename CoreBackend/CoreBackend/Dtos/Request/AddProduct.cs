using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackend.Dtos
{
    public class AddProduct
    {
        [Display(Name = "产品名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Name { get; set; }

        [Display(Name = "价格")]
        [Range(1, double.MaxValue, ErrorMessage = "{0}必须大于{1}")]
        public decimal Price { get; set; }

        [Display(Name = "描述")]
        [MaxLength(100, ErrorMessage = "{0}的长度不可以超过{1}")]
        public string Description { get; set; }
    }
}
