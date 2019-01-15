using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
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
                    case 1: // add et
                        foreach (var itemEt in etUtil.GetListEntertainments().GetRange(4, 3))
                        {
                            Console.WriteLine("Entertainment Name: " + itemEt.EnterName);
                            Console.WriteLine("Links: " + itemEt.Links);
                            Console.WriteLine("Category: " + itemEt.Category);
                            Console.WriteLine("Created date: " + itemEt.CreatedDate);
                            Console.WriteLine("Last modified date: " + itemEt.LastModifiedDate);
                            Console.WriteLine("------------------------------");
                        }
                        break;

                    case 2: // add Entertainment
                        Console.Write("Link: ");
                        var link = Console.ReadLine();

                        Console.Write("ET name: ");
                        var etName = Console.ReadLine();

                        HandleRandom.ChooseColorForString("Types of Category", ConsoleColor.DarkGreen);

                        string category;
                        var count = 1;
                        var listConstantValue = typeof(CategoriesEntertainmentConstant).GetAllPublicConstantValues<string>();
                        listConstantValue.Sort();
                        foreach (var propertyInfo in listConstantValue)
                        {
                            Console.WriteLine(count++ + ". " + propertyInfo);
                        }

                        Console.WriteLine("Please choose a category: ");
                        category = Console.ReadLine();

                        if (category != null)
                        {
                            if (int.Parse(category) > 0 && int.Parse(category) <= listConstantValue.Count)
                            {
                                category = listConstantValue[int.Parse(category) - 1];
                            }
                        }

                        etUtil.AddEntertainment(new Entertainment() { EnterId = HandleRandom.RandomString(8), EnterName = etName, Links = link, Category = category });
                        HandleRandom.ChooseColorForString("Adding succesful", ConsoleColor.Blue);

                        break;

                    case 3: // find et
                        Console.Write("Et Name: ");
                        var findEtName = Console.ReadLine();
                        var result = etUtil.FindEntertainmentBy(findEtName);
                        if (result != null)
                        {
                            HandleRandom.ChooseColorForString("--- Entertainment info ---", ConsoleColor.DarkRed);
                            HandleRandom.ChooseColorForString("Found ET", ConsoleColor.Blue);
                            Console.WriteLine("Id: " + result.EnterId);
                            Console.WriteLine("Et Name: " + result.EnterName);
                            Console.WriteLine("Link: " + result.Links);
                            Console.WriteLine("Category: " + result.Category);
                            Console.WriteLine("Created date: " + result.CreatedDate);
                            Console.WriteLine("Last modeified date: " + result.LastModifiedDate);
                            Console.WriteLine("---------------------------");

                            break;
                        }

                        HandleRandom.ChooseColorForString("There are no '" + findEtName + "' you find", ConsoleColor.Blue);
                        break;

                    case 4: // update Et
                        Console.Write("Et Name to update: ");
                        var placeHoldEtName = Console.ReadLine();

                        if (etUtil.FindEntertainmentBy(placeHoldEtName) != null)
                        {
                            var currentEtResult = etUtil.FindEntertainmentBy(placeHoldEtName);
                            HandleRandom.ChooseColorForString("Found ET", ConsoleColor.Blue);
                            Console.WriteLine("Info Entertainment---");
                            Console.WriteLine("Id: " + currentEtResult.EnterId);
                            Console.WriteLine("Et Name: " + currentEtResult.EnterName);
                            Console.WriteLine("Link: " + currentEtResult.Links);
                            Console.WriteLine("Category: " + currentEtResult.Category);
                            Console.WriteLine("---------------------------");

                            Console.Write("Et name to update: ");
                            var etNameToUpdate = Console.ReadLine();

                            Console.Write("Link to update: ");
                            var linkToUpdate = Console.ReadLine();

                            Console.WriteLine("Types of Category");
                            var counts = 1;
                            var listConstantValues = typeof(CategoriesEntertainmentConstant).GetAllPublicConstantValues<string>();
                            listConstantValues.Sort();
                            foreach (var propertyInfo in listConstantValues)
                            {
                                Console.WriteLine(counts++ + ". " + propertyInfo);
                            }

                            HandleRandom.ChooseColorForString("Please choose a category to update: ", ConsoleColor.DarkBlue);
                            var categoryToUpdate = Console.ReadLine();

                            if (categoryToUpdate != null)
                            {
                                if (int.Parse(categoryToUpdate) > 0 && int.Parse(categoryToUpdate) <= listConstantValues.Count)
                                {
                                    categoryToUpdate = listConstantValues[int.Parse(categoryToUpdate) - 1];
                                }
                            }

                            if (etUtil.UpdateEntertainment(currentEtResult.EnterName, etNameToUpdate, linkToUpdate,
                                categoryToUpdate))
                            {
                                HandleRandom.ChooseColorForString("Update Entertainment success", ConsoleColor.Blue);
                                break;
                            }
                            HandleRandom.ChooseColorForString("Update entertainment failed", ConsoleColor.DarkRed);
                        }
                        else
                        {
                            HandleRandom.ChooseColorForString("Nothing found entertainment.", ConsoleColor.DarkRed);
                        }

                        break;
                    case 5: // delete et
                        Console.Write("Et name to delete:");
                        var findEtToDelete = Console.ReadLine();
                        if (etUtil.DeleteEntertainment(findEtToDelete))
                        {
                            HandleRandom.ChooseColorForString("Deleted successful", ConsoleColor.Blue);
                            break;
                        }
                        HandleRandom.ChooseColorForString("Nothing entertainment name to delete.", ConsoleColor.DarkRed);
                        break;
                    case 6: // export to excel file
                        var saveFile = new SaveFileDialog();
                        Console.Write("Table name: ");
                        var tableName = Console.ReadLine();
                        var filePath = "";
                        if (saveFile.ShowDialog() == DialogResult.OK)
                        {
                            filePath = Path.GetFullPath(saveFile.FileName);
                        }

                        etUtil.SaveFileTo(filePath, tableName);
                        break;
                }

            } while (choose != 0);

            HandleRandom.ChooseColorForString(choose == 0 ? "Goodbye" : "There is no any option you choose.",
                ConsoleColor.Blue);
            Thread.Sleep(1500);
        }
    }
}
