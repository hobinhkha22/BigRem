using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionSampleCode.Constant;
using ConnectionSampleCode.Extension;
using ConnectionSampleCode.Interface;
using ConnectionSampleCode.Model;

namespace ConnectionSampleCode.HandleUtil
{
    public class EntertainmentUtil : IEntertainment
    {
        private readonly FileHandlerUtil _fileHandlerUtil;

        public EntertainmentUtil()
        {
            _fileHandlerUtil = new FileHandlerUtil();
        }

        public void AddEntertainment(Entertainment et)
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.ENTERTAINMENTCONSTAT);

            et.CreatedDate = $"{DateTime.Now:MMMM dd, yyyy}";
            et.EnterId = HandleRandom.RandomString(8);
            _fileHandlerUtil.JsonModel.Entertainment.Add(et);

            _fileHandlerUtil.SaveFile(EnumFileConstant.ENTERTAINMENTCONSTAT);
        }

        public Entertainment FindEntertainmentBy(string enterName)
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.ENTERTAINMENTCONSTAT);

            var findEt = _fileHandlerUtil.JsonModel.Entertainment.Find(e =>
                string.Equals(e.EnterName, enterName, StringComparison.CurrentCultureIgnoreCase));

            if (findEt != null)
            {
                _fileHandlerUtil.SaveFile(EnumFileConstant.ENTERTAINMENTCONSTAT);
                return findEt;
            }

            _fileHandlerUtil.SaveFile(EnumFileConstant.ENTERTAINMENTCONSTAT);
            return null;

        }

        public Entertainment FindEntertainmentByEnterId(string enterId)
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.ENTERTAINMENTCONSTAT);

            var getEtById = _fileHandlerUtil.JsonModel.Entertainment.Find(x =>
                string.Equals(x.EnterId, enterId, StringComparison.CurrentCultureIgnoreCase));

            if (getEtById != null)
            {
                _fileHandlerUtil.SaveFile(EnumFileConstant.ENTERTAINMENTCONSTAT);
                return getEtById;
            }

            _fileHandlerUtil.SaveFile(EnumFileConstant.ENTERTAINMENTCONSTAT);
            return null;
        }

        public bool UpdateEntertainment(string currentEnterName, string enterNewName, string newLink, string newCategory)
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.ENTERTAINMENTCONSTAT);

            var getCurrentEt = _fileHandlerUtil.JsonModel.Entertainment.
                Find(x => string.Equals(x.EnterName, currentEnterName, StringComparison.CurrentCultureIgnoreCase));
            var indexOfEt = _fileHandlerUtil.JsonModel.Entertainment.IndexOf(getCurrentEt);

            if (getCurrentEt != null)
            {
                _fileHandlerUtil.JsonModel.Entertainment[indexOfEt].EnterName = enterNewName;
                _fileHandlerUtil.JsonModel.Entertainment[indexOfEt].Links = newLink;
                _fileHandlerUtil.JsonModel.Entertainment[indexOfEt].Category = newCategory;
                _fileHandlerUtil.JsonModel.Entertainment[indexOfEt].LastModifiedDate = $"{DateTime.Now:MMMM dd, yyyy}";
                _fileHandlerUtil.SaveFile(EnumFileConstant.ENTERTAINMENTCONSTAT);

                return true;
            }

            _fileHandlerUtil.SaveFile(EnumFileConstant.ENTERTAINMENTCONSTAT);
            return false;
        }

        public bool DeleteEntertainment(string enterName)
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.ENTERTAINMENTCONSTAT);

            if (enterName == null) return false;

            var getEt = _fileHandlerUtil.JsonModel.Entertainment.
                Find(e => string.Equals(e.EnterName, enterName, StringComparison.CurrentCultureIgnoreCase));
            _fileHandlerUtil.JsonModel.Entertainment.Remove(getEt);

            _fileHandlerUtil.SaveFile(EnumFileConstant.ENTERTAINMENTCONSTAT);

            return true;
        }

        public List<Entertainment> GetListEntertainments()
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.ENTERTAINMENTCONSTAT);

            var listEt = _fileHandlerUtil.JsonModel.Entertainment.ToList();

            _fileHandlerUtil.SaveFile(EnumFileConstant.ENTERTAINMENTCONSTAT);

            return listEt;
        }
    }
}
