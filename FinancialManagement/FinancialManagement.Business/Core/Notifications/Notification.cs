using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagement.Business.Core.Notifications
{
    public class Notification
    {
        public string Message { get; set; }

        public Notification(string Message)
        {
            this.Message = Message;
        }
    }
}
