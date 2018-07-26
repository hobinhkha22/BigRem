using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionSampleCode.Model;

namespace ConnectionSampleCode.Interface
{
    public interface IEntertainment
    {
        void AddEntertainment(Entertainment et);

        Entertainment FindEntertainmentBy(string enterName);

        Entertainment FindEntertainmentByEnterId(string enterId);

        bool UpdateEntertainment(string currentEnterName, string enterNewName, string newLink, string newCategory);

        bool DeleteEntertainment(string enterName);

        List<Entertainment> GetListEntertainments();
    }
}
