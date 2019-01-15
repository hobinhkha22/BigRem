using System;
using System.Collections.Generic;
using System.Linq;
using RememberUtility.Enum;
using RememberUtility.Extension;
using RememberUtility.Interface;
using RememberUtility.Model;
using log4net;

namespace RememberUtility.HandleUtil
{
    public class BooksUtil : IBooks
    {
        private readonly FileHandlerUtil _fileHandlerUtil;
        private static readonly ILog Logs = LogManager.GetLogger(typeof(BooksUtil));

        public BooksUtil()
        {
            _fileHandlerUtil = new FileHandlerUtil();
            _fileHandlerUtil.CreateOrReadJsonDb(EnumFileConstant.BOOKCONSTANT);            
        }

        public void AddBook(Books books)
        {
            if (books != null)
            {
                var checkDuplicate = FindBookBy(books.BookName);
                if (checkDuplicate == null)
                {
                    books.CreatedDate = $"{DateTime.Now:MMMM dd, yyyy}";
                    books.BookId = HandleRandom.RandomString(10);

                    _fileHandlerUtil.JsonModel.Books.Add(books);

                    Logs.Info($"[AddBook] Adding '{books.BookName}' successful.");

                    _fileHandlerUtil.SaveFile(EnumFileConstant.BOOKCONSTANT);
                }
                else
                {
                    // Duplicate book name
                    Logs.Warn($"[AddBook] '{books.BookName}' have duplicate. Add failed");
                    _fileHandlerUtil.SaveFile(EnumFileConstant.BOOKCONSTANT);
                }
            }
        }

        public Books FindBookBy(string bookName)
        {
            try
            {
                return _fileHandlerUtil.JsonModel.Books.
                    Find(x => string.Equals(x.BookName, bookName, StringComparison.CurrentCultureIgnoreCase)); ;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateBook(string currentBookName, string bookName, string author, string category)
        {
            var getCurrenBooks = _fileHandlerUtil.JsonModel.Books.Find(x => x.BookName == currentBookName);
            var indexOfBook = _fileHandlerUtil.JsonModel.Books.IndexOf(getCurrenBooks);

            if (getCurrenBooks != null)
            {
                _fileHandlerUtil.JsonModel.Books[indexOfBook].BookName = bookName;
                _fileHandlerUtil.JsonModel.Books[indexOfBook].Author = author;
                _fileHandlerUtil.JsonModel.Books[indexOfBook].Category = category;
                _fileHandlerUtil.JsonModel.Books[indexOfBook].LastModifiedDate = $"{DateTime.Now:MMMM dd, yyyy}";

                _fileHandlerUtil.SaveFile(EnumFileConstant.BOOKCONSTANT);
                Logs.Info($"[UpdateBook] Update '{bookName}' successful");

                return true;
            }

            return false;
        }

        public Books FindBookByBookId(string bookId)
        {
            try
            {
                return _fileHandlerUtil.JsonModel.Books.Find(x =>
           string.Equals(x.BookId, bookId, StringComparison.CurrentCultureIgnoreCase));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteBook(string bookName)
        {
            if (bookName == null) return false;

            // Make it all lower case words
            var getCurrentBook = _fileHandlerUtil.JsonModel.Books.
                Find(x => string.Equals(x.BookName, bookName, StringComparison.CurrentCultureIgnoreCase));
            if (getCurrentBook == null)
            {
                return false;
            }

            _fileHandlerUtil.JsonModel.Books.Remove(getCurrentBook);
            _fileHandlerUtil.SaveFile(EnumFileConstant.BOOKCONSTANT);

            Logs.Info($"[DeleteBook] Deleting '{bookName}' successful");

            return true;
        }

        public List<Books> GetListBooks()
        {
            try
            {
                _fileHandlerUtil.SaveFile(EnumFileConstant.BOOKCONSTANT);

                return _fileHandlerUtil.JsonModel.Books.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void BackupDatabase(EnumFileConstant enumFile, string backUpFolder)
        {
            _fileHandlerUtil.BackUpFileWithFolder(enumFile, backUpFolder);
        }

        public void SaveBookToExcel(string filePath, string tableName)
        {
            _fileHandlerUtil.ExportFile<Books>(
                filePath.ToLower().EndsWith(".xlsx") ? filePath : filePath.Insert(filePath.Length, ".xlsx"), tableName);
        }

        public bool CreateJsonDb(EnumFileConstant enumFileConstant)
        {
            _fileHandlerUtil.CreateOrReadJsonDb(enumFileConstant);

            return true;
        }

        public void SaveBookDb()
        {
            _fileHandlerUtil.SaveFile(EnumFileConstant.BOOKCONSTANT);
        }


    } // End class
}
