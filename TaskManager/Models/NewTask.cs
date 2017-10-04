using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class NewTask
    {
        public string Description { get; set; }
        public long Priority { get; set; }
        public DateTime Deadline { get; set; }
    }
}