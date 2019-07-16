using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ConnectionSampleCode.HandleUtil;
using RememberUtility.Constant;
using RememberUtility.Extension;
using RememberUtility.HandleUtil;
using RememberUtility.Model;
using Console = System.Console;

namespace EntertainmentSampleCode
{
    public class Program
    {
        [STAThread]
        private static void Main()
        {
            var etUtil = new EntertainmentUtil();
            int choose;
            Console.OutputEncoding = Encoding.UTF8;
            do
            {
                Console.WriteLine("1. Get list entertainments");
                Console.WriteLine("2. Add entertainment");
                Console.WriteLine("3. Find entertainment");
                Console.WriteLine("4. Update entertainment");
                Console.WriteLine("5. Delete entertainment");
                Console.WriteLine("6. Export to excel");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");
                choose = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                switch (choose)
                {
                    case 1: // This time it will be the test program
                        Console.WriteLine(WebHealthCheck.TestWebSite("http://genk.vn/nghien-cuu-ve-600-trieu-phu-cho-thay-muc-do-giau-co-cua-ban-phu-thuoc-vao-6-yeu-to-bat-ke-tuoi-tac-hay-thu-nhap-20190127113348631.chn"));
                        break;
                }

            } while (choose != 0);

            HandleRandom.ChooseColorForString(choose == 0 ? "Goodbye" : "There is no any option you choose.",
                ConsoleColor.Blue);
            Thread.Sleep(1500);
        }
    }
}
