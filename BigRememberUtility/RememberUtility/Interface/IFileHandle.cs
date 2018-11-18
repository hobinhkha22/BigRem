using ConnectionSampleCode.Enum;
using ConnectionSampleCode.Model;

namespace ConnectionSampleCode.Interface
{
    public interface IFileHandle
    {
        void ReadFile(EnumFileConstant readEnumFile);

        void SaveFile(EnumFileConstant saveEnumFile);

        void SaveFileTo(string filePath, string tableName);

        void ExportFile<T>(string filePath, string tableName);

        void CheckModelValue(ConfigModel configModel);
        
        void CreateOrReadJsonDb(EnumFileConstant enumFileConstant);

        string GetFileName(EnumFileConstant enumFileConstant);     
    }
}
