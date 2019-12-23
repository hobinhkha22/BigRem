using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionSampleCode.Constant
{
    public class SSHRemoteConstant
    {
        public const string Server = "192.168.1.4";
        public const string UserName = "pi";
        public const string Password = "";

        public const string CommandToSend = "echo hello world";
        public const string CreaTeFile = "touch /home/pi/Documents/createfromremote.txt";
    }
}
