using System.Collections.Generic;
using RememberUtility.Enum;
using RememberUtility.Model;

namespace RememberUtility.Interface
{
    public interface IEntertainment
    {
        void AddEntertainment(Entertainment et);

        Entertainment FindEntertainmentBy(string enterName);

        Entertainment FindEntertainmentByEnterId(string enterId);

        bool UpdateEntertainment(string currentEnterName, string enterNewName, string newLink, string newCategory);

        bool DeleteEntertainment(string enterName);

        List<Entertainment> GetListEntertainments();

        void SaveFileTo(string filePath, string tableName);
        void BackupDatabase(EnumFileConstant enumFile, string backUpFolder);
    }
}
