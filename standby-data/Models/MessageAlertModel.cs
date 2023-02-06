using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using standby_data.Enums;

namespace standby_data.Models
{
    public class MessageAlertModel
    {
        public MessageAlertEnum Type { get; set; } = MessageAlertEnum.Undefined;
        public string Message { get; set; } = "null";
        public string Desc { get; set; } = "null";
    }
}