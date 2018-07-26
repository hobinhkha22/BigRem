
using System;
using System.Text;
using System.Threading;
using ConnectionSampleCode.Constant;
using ConnectionSampleCode.Extension;
using ConnectionSampleCode.HandleUtil;
using ConnectionSampleCode.Model;


namespace QuoteSampleCode
{
    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            var quoteUtil = new QuoteUtil();
            int choose;
            do
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("1. Get list quotes");
                Console.WriteLine("2. Add quote");
                Console.WriteLine("3. Find quote");
                Console.WriteLine("4. Upate quote");
                Console.WriteLine("5. Delete quote");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");
                choose = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                switch (choose)
                {
                    case 1:
                        foreach (var itemQuote in quoteUtil.GetListQuotes())
                        {
                            Console.WriteLine("Quote Id: " + itemQuote.QuotesId);
                            Console.WriteLine("Quote Name: " + itemQuote.QuotesName);
                            Console.WriteLine("Author: " + itemQuote.Author);
                            Console.WriteLine("Category: " + itemQuote.Type);
                            Console.WriteLine("-----------------------------");
                        }
                        break;
                    case 2: // add quote
                        Console.Write("Quote Name: ");
                        var quoteName = Console.ReadLine();

                        Console.Write("Author: ");
                        var author = Console.ReadLine();

                        Console.WriteLine("Types of Category");
                        string category;
                        var count = 1;
                        var listConstantValue = typeof(TypesQuoteConstant).GetAllPublicConstantValues<string>();
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

                        quoteUtil.AddQuote(new Quotes() { QuotesId = HandleRandom.RandomString(8), QuotesName = quoteName, Author = author, Type = category });
                        HandleRandom.ChooseColorForString("Adding successful", ConsoleColor.Blue);
                        break;
                    case 3: // find quote
                        Console.Write("Quote name: ");
                        var findquoteName = Console.ReadLine();
                        var result = quoteUtil.FindQuoteBy(findquoteName);
                        if (result != null)
                        {
                            HandleRandom.ChooseColorForString("Found Quote", ConsoleColor.Blue);
                            Console.WriteLine("Info quote---");
                            Console.WriteLine("Id: " + result.QuotesId);
                            Console.WriteLine("Quote Name: " + result.QuotesName);
                            Console.WriteLine("Author: " + result.Author);
                            Console.WriteLine("Category: " + result.Type);
                            Console.WriteLine("-----------------------------");
                            break;
                        }
                        HandleRandom.ChooseColorForString("There are no quote you find", ConsoleColor.Blue);
                        break;
                    case 4: // update quote
                        Console.Write("Find quote to UPDATE: ");
                        var placeHoldquoteName = Console.ReadLine();

                        if (quoteUtil.FindQuoteBy(placeHoldquoteName) != null)
                        {
                            var currentquoteResult = quoteUtil.FindQuoteBy(placeHoldquoteName);
                            HandleRandom.ChooseColorForString("Found quote", ConsoleColor.Blue);
                            Console.WriteLine("Quote info---");
                            Console.WriteLine("Id: " + currentquoteResult.QuotesId);
                            Console.WriteLine("Quote Name: " + currentquoteResult.QuotesName);
                            Console.WriteLine("Author: " + currentquoteResult.Author);
                            Console.WriteLine("Category: " + currentquoteResult.Type);
                            Console.WriteLine("-----------------------------");

                            Console.Write("Quote name to update: ");
                            var quoteNameToUpdate = Console.ReadLine();

                            Console.Write("Author to update: ");
                            var authorToUpdate = Console.ReadLine();

                            Console.WriteLine("Types of Category");
                            var counts = 1;
                            var listConstantValues = typeof(TypesQuoteConstant).GetAllPublicConstantValues<string>();
                            listConstantValues.Sort();
                            foreach (var propertyInfo in listConstantValues)
                            {
                                Console.WriteLine(counts++ + ". " + propertyInfo);
                            }

                            Console.Write("Please choose a category to update: ");
                            var categoryToUpdate = Console.ReadLine();

                            if (categoryToUpdate != null)
                            {
                                if (int.Parse(categoryToUpdate) > 0 && int.Parse(categoryToUpdate) <= listConstantValues.Count)
                                {
                                    categoryToUpdate = listConstantValues[int.Parse(categoryToUpdate) - 1];
                                }
                            }

                            if (quoteUtil.UpdateQuote(currentquoteResult.QuotesName, quoteNameToUpdate, authorToUpdate,
                                categoryToUpdate))
                            {
                                HandleRandom.ChooseColorForString("Update quote success", ConsoleColor.Blue);
                                break;
                            }
                            HandleRandom.ChooseColorForString("Update quote Failed", ConsoleColor.DarkRed);
                        }
                        else
                        {
                            HandleRandom.ChooseColorForString("Nothing found quote", ConsoleColor.DarkRed);
                        }
                        break;
                    case 5:
                        Console.Write("Quote name to delete: ");
                        var findquoteToDelete = Console.ReadLine();
                        if (quoteUtil.DeleteQuote(findquoteToDelete))
                        {
                            HandleRandom.ChooseColorForString("Deleted successful", ConsoleColor.Blue);
                            break;
                        }
                        HandleRandom.ChooseColorForString("Nothing quote name to delete", ConsoleColor.DarkRed);
                        break;
                }

            } while (choose != 0);

            HandleRandom.ChooseColorForString("There is no option you chose", ConsoleColor.Blue);
            Thread.Sleep(2000);
        }
    }
}
