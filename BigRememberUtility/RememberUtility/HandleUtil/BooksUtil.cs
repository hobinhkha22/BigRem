using System;
using System.Collections.Generic;
using System.Linq;
using ConnectionSampleCode.Enum;
using ConnectionSampleCode.Interface;
using ConnectionSampleCode.Model;

namespace ConnectionSampleCode.HandleUtil
{
    public class BooksUtil : IBooks
    {
        private readonly FileHandlerUtil _fileHandlerUtil;

        public BooksUtil()
        {
            _fileHandlerUtil = new FileHandlerUtil();
        }

        public void AddBook(Books books)
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.BOOKCONSTANT);

            books.CreatedDate = $"{DateTime.Now:MMMM dd, yyyy}";
            _fileHandlerUtil.JsonModel.Books.Add(books);

            _fileHandlerUtil.SaveFile(EnumFileConstant.BOOKCONSTANT);
        }

        public Books FindBookBy(string bookName)
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.BOOKCONSTANT);

            var getBooks = _fileHandlerUtil.JsonModel.Books.
                Find(x => string.Equals(x.BookName, bookName, StringComparison.CurrentCultureIgnoreCase));

            if (getBooks == null) return null;
            _fileHandlerUtil.SaveFile(EnumFileConstant.BOOKCONSTANT);

            return getBooks;
        }

        public bool UpdateBook(string currentBookName, string bookName, string author, string category)
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.BOOKCONSTANT);

            var getCurrenBooks = _fileHandlerUtil.JsonModel.Books.Find(x => x.BookName == currentBookName);
            var indexOfBook = _fileHandlerUtil.JsonModel.Books.IndexOf(getCurrenBooks);

            if (getCurrenBooks != null)
            {
                _fileHandlerUtil.JsonModel.Books[indexOfBook].BookName = bookName;
                _fileHandlerUtil.JsonModel.Books[indexOfBook].Author = author;
                _fileHandlerUtil.JsonModel.Books[indexOfBook].Category = category;
                _fileHandlerUtil.JsonModel.Books[indexOfBook].LastModifiedDate = $"{DateTime.Now:MMMM dd, yyyy}";

                _fileHandlerUtil.SaveFile(EnumFileConstant.BOOKCONSTANT);

                return true;
            }

            _fileHandlerUtil.SaveFile(EnumFileConstant.BOOKCONSTANT);
            return false;
        }

        public Books FindBookByBookId(string bookId)
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.BOOKCONSTANT);

            var getBookById = _fileHandlerUtil.JsonModel.Books.Find(x =>
                string.Equals(x.BookId, bookId, StringComparison.CurrentCultureIgnoreCase));

            if (getBookById != null)
            {
                _fileHandlerUtil.SaveFile(EnumFileConstant.BOOKCONSTANT);
                return getBookById;
            }

            _fileHandlerUtil.SaveFile(EnumFileConstant.BOOKCONSTANT);
            return null;
        }

        public bool DeleteBook(string bookName)
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.BOOKCONSTANT);
            if (bookName == null) return false;

            // Make it all lower case words
            var getCurrentBook = _fileHandlerUtil.JsonModel.Books.
                Find(x => string.Equals(x.BookName, bookName, StringComparison.CurrentCultureIgnoreCase));
            if (getCurrentBook == null)
            {
                _fileHandlerUtil.SaveFile(EnumFileConstant.BOOKCONSTANT);
                return false;
            }
            _fileHandlerUtil.JsonModel.Books.Remove(getCurrentBook);

            _fileHandlerUtil.SaveFile(EnumFileConstant.BOOKCONSTANT);
            return true;
        }

        public List<Books> GetListBooks()
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.BOOKCONSTANT);

            var list = _fileHandlerUtil.JsonModel.Books.ToList();

            _fileHandlerUtil.SaveFile(EnumFileConstant.BOOKCONSTANT);

            return list;
        }
    }
}
