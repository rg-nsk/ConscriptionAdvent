using ConscriptionAdvent.Domain.Enums;

namespace ConscriptionAdvent.Domain.ExtensionMethods.EnumExtensions
{
    public static class VaccinationTypeExtensions
    {
        public static string ToVaccinationTypeString(this VaccinationType source)
        {
            switch (source)
            {
                case VaccinationType.K: return "К";
                case VaccinationType.K_1: return "К-1";
                case VaccinationType.K_2: return "К-2";
                case VaccinationType.Otkaz: return "отказ";
                case VaccinationType.Antitela: return "антитела";
                case VaccinationType.MedOtvod: return "мед.отвод";
            }

            return string.Empty;
            //return null;
        }

        public static VaccinationType ToVaccinationTypeEnum(this string source)
        {
            switch (source)
            {
                case "К": return VaccinationType.K;
                case "К-1": return VaccinationType.K_1;
                case "К-2": return VaccinationType.K_2;
                case "отказ": return VaccinationType.Otkaz;
                case "антитела": return VaccinationType.Antitela;
                case "мед.отвод": return VaccinationType.MedOtvod;
            }

            return VaccinationType.None;
        }
    }
}
