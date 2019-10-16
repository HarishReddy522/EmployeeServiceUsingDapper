using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeModels
{
    [Table("Employee")]
    public class EmployeeModel
    {
        [Key]
        public int E_Id { get; set; }

        [Required(ErrorMessage ="Employee Name is Mandatory")]
        public string EName { get; set; }

        [Required(ErrorMessage = "PhoneNumber is Mandatory")]
        [MaxLength(10,ErrorMessage ="PhoneNumber can't be more than 10 digits")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "salary is Mandatory")]
        public int salary { get; set; }
        public Department Department{ get; set; }




    }
}
