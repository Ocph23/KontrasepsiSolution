using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.Models
{
    public class ErrorMessage
    {
        public string Title { get; set; }
        public int Status{ get; set; }
        public string Detail { get; set; }
        public string Messgae { get; set; }
        public string TraceId { get; set; }
    }
}
