using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taller6.Models
{
    public class Entity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
    }
}