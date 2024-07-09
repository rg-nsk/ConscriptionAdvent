using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.ExtensionMethods.EnumExtensions
{
    public static class IssuedByListExtensions
    {
        public static string ToIssuedByListString(this IssuedByList source)
        {
            switch (source)
            {
                case IssuedByList.MainNSO: return "ГУ МВД России по Новосибирской области";
            }

            //return source.
            return string.Empty;
            //return null;
        }

        public static IssuedByList ToIssuedByListEnum(this string source)
        {
            switch (source)
            {
                case "ГУ МВД России по Новосибирской области": return IssuedByList.MainNSO;
            }

            return IssuedByList.None;
        }
    }
}
