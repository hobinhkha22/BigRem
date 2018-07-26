using System;
using System.Text;
using System.Threading;
using ConnectionSampleCode.Extension;
using ConnectionSampleCode.HandleUtil;
using ConnectionSampleCode.Model;

namespace UserSampleCode
{
    public class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            var userUtil = new UserUtil();
            int choose;
            do
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("1. Get list Users");
                Console.WriteLine("2. Add user");
                Console.WriteLine("3. Find user");
                Console.WriteLine("4. Upate user");
                Console.WriteLine("5. Delete user");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");
                choose = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                switch (choose)
                {
                    case 1:
                        foreach (var itemUsers in userUtil.GetListUsers())
                        {
                            Console.WriteLine("User Name: " + itemUsers.Username);
                            Console.WriteLine("Password encrypt: " + itemUsers.PasswordEncrypt);
                            Console.WriteLine("Created date: " + itemUsers.CreatedDate);
                            Console.WriteLine("Role: " + itemUsers.UserRole);
                            Console.WriteLine("-----------------------------");
                        }
                        break;
                    case 2: // add user
                        Console.Write("User Name: ");
                        var username = Console.ReadLine();

                        Console.Write("Password: ");
                        var password = Console.ReadLine();


                        userUtil.AddUser(new UserLogin() { Username = username, PasswordEncrypt = HandleRandom.Encrypt(password) });
                        HandleRandom.ChooseColorForString("Adding successful", ConsoleColor.Blue);
                        break;
                    case 3: // find user
                        Console.Write("User name: ");
                        var findUserName = Console.ReadLine();
                        var result = userUtil.CheckUser(findUserName);
                        if (result != null)
                        {
                            HandleRandom.ChooseColorForString("Found User", ConsoleColor.Blue);
                            Console.WriteLine("User info---");
                            Console.WriteLine("Id: " + result.UserId);
                            Console.WriteLine("User Name: " + result.Username);
                            Console.WriteLine("Password encrypt: " + result.PasswordEncrypt);
                            Console.WriteLine("Role: " + result.UserRole);
                            Console.WriteLine("Created Date: " + result.CreatedDate);
                            Console.WriteLine("Last modified date: " + result.LastModifiedDate);
                            Console.WriteLine("-----------------------------");
                            break;
                        }
                        HandleRandom.ChooseColorForString("There are no user you find", ConsoleColor.Blue);
                        break;
                    case 4: // update user
                        Console.Write("Find user to UPDATE: ");
                        var placeHoldUsername = Console.ReadLine();

                        if (userUtil.CheckUser(placeHoldUsername) != null)
                        {
                            var currentUserResult = userUtil.CheckUser(placeHoldUsername);
                            HandleRandom.ChooseColorForString("Found User", ConsoleColor.Blue);
                            Console.WriteLine("Info User---");
                            Console.WriteLine("Id: " + currentUserResult.UserId);
                            Console.WriteLine("User Name: " + currentUserResult.Username);
                            Console.WriteLine("Password encrypt: " + currentUserResult.PasswordEncrypt);
                            Console.WriteLine("Role: " + currentUserResult.UserRole);
                            Console.WriteLine("-----------------------------");

                            Console.Write("User name to update: ");
                            var usernameToUpdate = Console.ReadLine();

                            Console.Write("Password to update: ");
                            var passwordToUpdate = Console.ReadLine();

                            if (userUtil.UpdateUser(currentUserResult.Username, usernameToUpdate, passwordToUpdate))
                            {
                                HandleRandom.ChooseColorForString("Update user success", ConsoleColor.Blue);
                                break;
                            }
                            HandleRandom.ChooseColorForString("Update user failed", ConsoleColor.DarkRed);
                        }
                        else
                        {
                            HandleRandom.ChooseColorForString("Nothing found user", ConsoleColor.DarkRed);
                        }
                        break;
                    case 5:
                        Console.Write("User name to delete: ");
                        var findUserToDelete = Console.ReadLine();
                        if (userUtil.DeleteUser(findUserToDelete))
                        {
                            HandleRandom.ChooseColorForString("Deleted successful", ConsoleColor.Blue);
                            break;
                        }
                        HandleRandom.ChooseColorForString("Nothing user name to delete", ConsoleColor.DarkRed);
                        break;
                }

            } while (choose != 0);

            HandleRandom.ChooseColorForString("There is no option you chose", ConsoleColor.Blue);
            Thread.Sleep(2000);
        }
    }
}
