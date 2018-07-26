using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionSampleCode.Model;

namespace ConnectionSampleCode.Interface
{
    public interface IQuote
    {
        void AddQuote(Quotes quote);

        Quotes FindQuoteBy(string quoteName);

        Quotes FindQuoteByQuoteId(string quoteId);

        bool UpdateQuote(string currentQuoteName, string newQuoteName, string newAuthor, string newType);

        bool DeleteQuote(string quoteName);

        List<Quotes> GetListQuotes();

        IEnumerable<Quotes> GetListQuotetBy(string author);

        IEnumerable<string> GetListQuotesNameBy(string author);
    }
}
