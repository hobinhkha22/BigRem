using ConnectionSampleCode.HandleUtil;
using System;
using System.Text;
using System.Threading;
using ConnectionSampleCode.Constant;
using ConnectionSampleCode.Extension;
using ConnectionSampleCode.Model;

namespace BookSampleCode
{
    public class Program
    {
        private static void Main()
        {
            LoggerUtil.HandleLogPath(FileConstant.LoggerFolderName, FileConstant.LoggerName);
            var bookUtil = new BooksUtil();

            int choose;
            do
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("1. Get list books");
                Console.WriteLine("2. Add book");
                Console.WriteLine("3. Find book");
                Console.WriteLine("4. Delete book");
                Console.WriteLine("5. Upate book");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");
                choose = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                switch (choose)
                {
                    case 1:

                        foreach (var itemBook in bookUtil.GetListBooks())
                        {
                            Console.WriteLine("Book Name: " + itemBook.BookName);
                            Console.WriteLine("Author: " + itemBook.Author);
                            Console.WriteLine("Category: " + itemBook.Category);
                            Console.WriteLine("-----------------------------");
                        }
                        break;
                    case 2: // add book
                        Console.Write("Book Name: ");
                        var bookName = Console.ReadLine();

                        Console.Write("Author: ");
                        var author = Console.ReadLine();

                        Console.WriteLine("Types of Category");
                        string category;
                        var count = 1;
                        var listConstantValue = typeof(CategoriesBookConstant).GetAllPublicConstantValues<string>();
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

                        bookUtil.AddBook(new Books() { BookId = HandleRandom.RandomString(8), BookName = bookName, Author = author, Category = category });
                        HandleRandom.ChooseColorForString("Adding successful", ConsoleColor.Blue);
                        break;
                    case 3: // find book
                        Console.Write("Book name: ");
                        var findBookName = Console.ReadLine();
                        var result = bookUtil.FindBookBy(findBookName);
                        if (result != null)
                        {
                            Console.WriteLine("Info Book---");
                            Console.WriteLine("Id: " + result.BookId);
                            Console.WriteLine("Book Name: " + result.BookName);
                            Console.WriteLine("Author: " + result.Author);
                            Console.WriteLine("Category: " + result.Category);
                            Console.WriteLine("-----------------------------");
                            break;
                        }
                        HandleRandom.ChooseColorForString("There are no book you find", ConsoleColor.Blue);
                        break;
                    case 4:
                        Console.Write("Book name to delete: ");
                        var findBookToDelete = Console.ReadLine();
                        if (bookUtil.DeleteBook(findBookToDelete))
                        {
                            HandleRandom.ChooseColorForString("Deleted successful", ConsoleColor.Blue);
                            break;
                        }
                        HandleRandom.ChooseColorForString("Nothing book name to delete", ConsoleColor.DarkRed);
                        break;
                    case 5: // update book
                        Console.Write("Find book to UPDATE: ");
                        var placeHoldBookName = Console.ReadLine();

                        if (bookUtil.FindBookBy(placeHoldBookName) != null)
                        {
                            var currentBookResult = bookUtil.FindBookBy(placeHoldBookName);
                            HandleRandom.ChooseColorForString("Found book", ConsoleColor.Blue);
                            Console.WriteLine("Book Info---");
                            Console.WriteLine("Id: " + currentBookResult.BookId);
                            Console.WriteLine("Book Name: " + currentBookResult.BookName);
                            Console.WriteLine("Author: " + currentBookResult.Author);
                            Console.WriteLine("Category: " + currentBookResult.Category);
                            Console.WriteLine("-----------------------------");

                            Console.Write("Book name to update: ");
                            var bookNameToUpdate = Console.ReadLine();

                            Console.Write("Author to update: ");
                            var authorToUpdate = Console.ReadLine();

                            Console.WriteLine("Types of Category");
                            var counts = 1;
                            var listConstantValues = typeof(CategoriesBookConstant).GetAllPublicConstantValues<string>();
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

                            if (bookUtil.UpdateBook(currentBookResult.BookName, bookNameToUpdate, authorToUpdate,
                                categoryToUpdate))
                            {
                                HandleRandom.ChooseColorForString("Update book success", ConsoleColor.Blue);
                                break;
                            }
                            HandleRandom.ChooseColorForString("Update book Failed", ConsoleColor.DarkRed);
                        }
                        else
                        {
                            HandleRandom.ChooseColorForString("Nothing found book", ConsoleColor.DarkRed);
                        }
                        break;
                }

            } while (choose != 0);

            HandleRandom.ChooseColorForString("There is no option you chose", ConsoleColor.Blue);
            Thread.Sleep(2000);
        }
    }
}
