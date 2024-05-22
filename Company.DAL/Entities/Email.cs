using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Entities
{
    public class Email
    {
        public string Subject { get; set; }
        public string Recipient { get; set; }
        public string Body { get; set; }
    }
}
