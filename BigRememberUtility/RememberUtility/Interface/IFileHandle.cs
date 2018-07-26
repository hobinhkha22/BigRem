using ConnectionSampleCode.Constant;

namespace ConnectionSampleCode.Interface
{
    public interface IFileHandle
    {
        void ReadFile(EnumFileConstant readEnumFile);
        void SaveFile(EnumFileConstant saveEnumFile);
    }
}
