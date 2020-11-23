using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassModel.Model.Fiber
{
    public class User
    {
        [Key]
        public int IDUser { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public int Role { get; set; }
        public Boolean Active { get; set; }
        public int Status { get; set; }
    }
}
