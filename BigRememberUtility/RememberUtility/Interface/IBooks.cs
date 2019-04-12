using System.Collections.Generic;
using RememberUtility.Enum;
using RememberUtility.Model;

namespace RememberUtility.Interface
{
    internal interface IBooks
    {
        void AddBook(Books books);

        Books FindBookBy(string bookName);

        Books FindBookByBookId(string bookId);

        Books FindBookByBookAuthor(string author);

        bool DeleteBook(string bookName);

        bool UpdateBook(string currentBookName, string bookName, string author, string category);

        List<Books> GetListBooks();

        void SaveBookToExcel(string filePath, string tableName);

        void SaveBookDb();

        bool CreateJsonDb(EnumFileConstant enumFileConstant);        
    }
}
