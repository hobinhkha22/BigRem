using System.Collections.Generic;
using ConnectionSampleCode.Model;

namespace ConnectionSampleCode.Interface
{
    internal interface IBooks
    {
        void AddBook(Books books);

        Books FindBookBy(string bookName);

        Books FindBookByBookId(string bookId);

        bool DeleteBook(string bookName);

        bool UpdateBook(string currentBookName, string bookName, string author, string category);

        List<Books> GetListBooks();
    }
}
