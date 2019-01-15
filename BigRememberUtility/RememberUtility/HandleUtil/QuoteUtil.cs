using System;
using System.Collections.Generic;
using System.Linq;
using RememberUtility.Enum;
using RememberUtility.Extension;
using RememberUtility.Interface;
using RememberUtility.Model;

namespace RememberUtility.HandleUtil
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
            try
            {
                _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);

                return _fileHandlerUtil.JsonModel.Quotes.
                    Find(q => string.Equals(q.QuotesName, quoteName, StringComparison.CurrentCultureIgnoreCase));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Quotes FindQuoteByQuoteId(string quoteId)
        {
            try
            {
                _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);

                return _fileHandlerUtil.JsonModel.Quotes.Find(x => string.Equals(x.QuotesId,
                    quoteId, StringComparison.CurrentCultureIgnoreCase));
            }
            catch (Exception)
            {
                return null;
            }

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
            try
            {
                _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);

                return _fileHandlerUtil.JsonModel.Quotes.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Quotes> GetListQuotetBy(string author)
        {
            try
            {
                _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);

                return _fileHandlerUtil.JsonModel.Quotes.Where(a => a.Author == author).ToList(); ;
            }
            catch (Exception)
            {
                return null;
            }


        }

        public IEnumerable<string> GetListQuotesNameBy(string author)
        {
            try
            {
                _fileHandlerUtil.SaveFile(EnumFileConstant.QUOTESCONSTANT);

                return _fileHandlerUtil.JsonModel.Quotes.Where(a => a.Author == author).ToList().OfType<string>();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
