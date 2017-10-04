using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.DataAccessLayer.Models
{
    public class BaseModel<T>
    {
        public T Id { get; set; }
    }
}