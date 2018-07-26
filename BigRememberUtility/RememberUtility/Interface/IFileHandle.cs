using ConnectionSampleCode.Constant;
using ConnectionSampleCode.Enum;

namespace ConnectionSampleCode.Interface
{
    public interface IFileHandle
    {
        void ReadFile(EnumFileConstant readEnumFile);
        void SaveFile(EnumFileConstant saveEnumFile);
    }
}
