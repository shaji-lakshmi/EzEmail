using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emailLibrary
{
    public class Email
    {
        public String SenderName { get; set; }

        public String Subject { get; set; }

        public String EmailBody { get; set; }

        public String CreatedTime { get; set; }
    }
}
