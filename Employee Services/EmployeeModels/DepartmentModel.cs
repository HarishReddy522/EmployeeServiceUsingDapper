using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeModels
{
    public class Department
    {
        [Key]
        //[HiddenInput(DisplayValue =false)]
        public int D_Id { get; set; }

        [Required(ErrorMessage = "Department Name is Mandatory")]
        [Display(Name ="Department Name")]
        public string DName { get; set; }

        [Required(ErrorMessage = "Location is Mandatory")]
        [Display(Name = "Location")]
        public string DLOcation { get; set; }

       public List<EmployeeModel> employees { get; set; }

    }
}
