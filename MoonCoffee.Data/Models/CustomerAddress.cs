using System;
using System.ComponentModel.DataAnnotations;

namespace MoonCoffee.Data.Models
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(100)]
        public string LineAddress1 { get; set; }
        [MaxLength(100)]
        public string LineAddress2 { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(20)]
        public string Country { get; set; }
        [MaxLength(10)]
        public string PostalCode { get; set; }


    }
}