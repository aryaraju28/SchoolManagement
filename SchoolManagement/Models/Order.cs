using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Malayalam { get; set; }

        public int Hindi { get; set; }
        public int English { get; set; }
        public string CurrentTime { get; set; }
        public int order { get; set; }

    }
}