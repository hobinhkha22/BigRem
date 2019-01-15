using RememberUtility.Enum;
using RememberUtility.Model;

namespace RememberUtility.Interface
{
    public interface IFileHandle
    {
        void SaveFile(EnumFileConstant saveEnumFile);

        void SaveFileTo(string filePath, string tableName);

        void ExportFile<T>(string filePath, string tableName);

        void CheckModelValue(ConfigModel configModel);
        
        void CreateOrReadJsonDb(EnumFileConstant enumFileConstant);

        string GetFileName(EnumFileConstant enumFileConstant);

        void BackUpFileWithFolder(EnumFileConstant saveEnumFile, string backUpFolder);
        
    }
}
