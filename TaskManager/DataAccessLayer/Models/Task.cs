using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.DataAccessLayer.Models
{
    public class Task: BaseModel<long>
    {
        public DateTime CreatedDate { get; set; }
        public long Priority { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
    }
}