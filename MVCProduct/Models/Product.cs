using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApplicationExam.Models
{
    public class Product
    {
        [Key]
        public int ProductId { set; get; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "please enter ProductName")]
        public String ProductName { set; get; }

        public decimal Rate { set; get; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "please enter Description")]
        public string Description { set; get; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "please enter CategoryName")]

        public string CategoryName { set; get; }
    }
}