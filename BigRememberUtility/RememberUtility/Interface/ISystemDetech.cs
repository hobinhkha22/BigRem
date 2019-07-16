using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RememberUtility.Interface
{
    interface ISystemDetech
    {
        void DetectOS();

        void DetectArchecture();

        void DetectUser();
    }
}
