using System;
using System.Collections.Generic;
using System.Linq;
using ConnectionSampleCode.Enum;
using ConnectionSampleCode.Extension;
using ConnectionSampleCode.Interface;
using ConnectionSampleCode.Model;

namespace ConnectionSampleCode.HandleUtil
{
    public class QuoteUtil : IQuote
    {
        private readonly FileHandlerUtil _fileHandlerUtil;

        public QuoteUtil()
        {
            _fileHandlerUtil = new FileHandlerUtil();
            _fileHandlerUtil.CreateOrReadJsonDb(EnumFileConstant.QUOTESCONSTANT);
        }

        public void AddQuote(Quotes quote)
        {
            quote.QuotesId = HandleRandom.RandomString(8);
            quote.CreatedDate = $"{DateTime.Now:MMMM dd, yyyy}";
            _fileHandlerUtil.JsonModel.Quotes.Add(quote);

            _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);
        }

        public Quotes FindQuoteBy(string quoteName)
        {
            var findQuote = _fileHandlerUtil.JsonModel.Quotes.
                Find(q => string.Equals(q.QuotesName, quoteName, StringComparison.CurrentCultureIgnoreCase));

            if (findQuote != null)
            {
                _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);
                return findQuote;
            }

            _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);
            return null;
        }

        public Quotes FindQuoteByQuoteId(string quoteId)
        {
            if (quoteId == null) return null;

            var getQuoteById = _fileHandlerUtil.JsonModel.Quotes.Find(x => string.Equals(x.QuotesId, quoteId, StringComparison.CurrentCultureIgnoreCase));

            if (getQuoteById != null)
            {
                return getQuoteById;
            }

            _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);
            return null;
        }

        public bool UpdateQuote(string currentQuoteName, string newQuoteName, string newAuthor, string newType)
        {
            var getCurrentQuote = _fileHandlerUtil.JsonModel.Quotes.Find(q => q.QuotesName == currentQuoteName);
            var indexOfQuote = _fileHandlerUtil.JsonModel.Quotes.IndexOf(getCurrentQuote);

            if (getCurrentQuote != null)
            {
                _fileHandlerUtil.JsonModel.Quotes[indexOfQuote].QuotesName = newQuoteName;
                _fileHandlerUtil.JsonModel.Quotes[indexOfQuote].Author = newAuthor;
                _fileHandlerUtil.JsonModel.Quotes[indexOfQuote].Type = newType;
                _fileHandlerUtil.JsonModel.Quotes[indexOfQuote].LastModifiedDate = $"{DateTime.Now:MMMM dd, yyyy}";

                _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);
                return true;
            }

            _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);
            return false;
        }

        public bool DeleteQuote(string quoteName)
        {
            if (quoteName == null) return false;
            var getQuote = _fileHandlerUtil.JsonModel.Quotes
                .Find(q => string.Equals(q.QuotesName, quoteName, StringComparison.CurrentCultureIgnoreCase));
            _fileHandlerUtil.JsonModel.Quotes.Remove(getQuote);

            if (getQuote == null)
            {
                _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);

                return false;
            }

            _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);

            return true;
        }

        public List<Quotes> GetListQuotes()
        {
            var listQuotes = _fileHandlerUtil.JsonModel.Quotes.ToList();

            _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);

            return listQuotes;
        }

        public IEnumerable<Quotes> GetListQuotetBy(string author)
        {
            var getListQuotesByAuthor = _fileHandlerUtil.JsonModel.Quotes.Where(a => a.Author == author).ToList();

            _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);

            return getListQuotesByAuthor;
        }

        public IEnumerable<string> GetListQuotesNameBy(string author)
        {
            var getListQuotesNameByAuthor = _fileHandlerUtil.JsonModel.Quotes.Where(a => a.Author == author).ToList().OfType<string>();

            _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);

            return getListQuotesNameByAuthor;
        }
    }
}
