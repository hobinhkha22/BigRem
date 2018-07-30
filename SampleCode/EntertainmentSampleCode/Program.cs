using System;
using System.Threading;
using ConnectionSampleCode.Constant;
using ConnectionSampleCode.Extension;
using ConnectionSampleCode.HandleUtil;
using ConnectionSampleCode.Model;
using Console = System.Console;

namespace EntertainmentSampleCode
{
    public class Program
    {
        private static void Main()
        {
            var etUtil = new EntertainmentUtil();
            int choose;

            do
            {
                Console.WriteLine("1. Get list entertainments");
                Console.WriteLine("2. Add entertainment");
                Console.WriteLine("3. Find entertainment");
                Console.WriteLine("4. Update entertainment");
                Console.WriteLine("5. Delete entertainment");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");
                choose = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                switch (choose)
                {
                    case 1: // add et
                        foreach (var itemEt in etUtil.GetListEntertainments())
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
                            HandleRandom.ChooseColorForString("--- Entertainment info ---", ConsoleColor.White);
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
                }

            } while (choose != 0);

            HandleRandom.ChooseColorForString("There is no option you chose.", ConsoleColor.Blue);
            Thread.Sleep(1500);
        }
    }
}
