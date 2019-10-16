using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeModels
{
    public class Order
    {
        [Key]
        public int OrId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Amount { get; set; }
  
        public Customer customer { get; set; }
        public List<Item> items { get; set; }
    }
}
