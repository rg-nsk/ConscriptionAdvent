using ConscriptionAdvent.Domain.Enums;

namespace ConscriptionAdvent.Domain.ExtensionMethods.EnumExtensions
{
    public static class OccupationStatusExtensions
    {
        public static string ToOccupationStatusString(this OccupationStatus source)
        {
            switch (source)
            {
                case OccupationStatus.NotWorkNotStudy: return "Нигде не работал и не учился";
                case OccupationStatus.StudyInEducationInstitution: return "Учился в образовательном учреждении";
                case OccupationStatus.WorkOnAgriculture: return "Работал в сельском хозяйстве";
                case OccupationStatus.WorkOnStateEnterprise: return "Работал на государственном предприятии";
                case OccupationStatus.WorkOnCommercialEnterprise: return "Работал на коммерческом предприятии";
                case OccupationStatus.WorkOther: return "Работал на предприятии иных форм";
            }

            return string.Empty;
        }

        public static OccupationStatus ToOccupationStatusEnum(this string source)
        {
            switch (source)
            {
                case "Нигде не работал и не учился": return OccupationStatus.NotWorkNotStudy;
                case "Учился в образовательном учреждении": return OccupationStatus.StudyInEducationInstitution;
                case "Работал в сельском хозяйстве": return OccupationStatus.WorkOnAgriculture;
                case "Работал на государственном предприятии": return OccupationStatus.WorkOnStateEnterprise;
                case "Работал на коммерческом предприятии": return OccupationStatus.WorkOnCommercialEnterprise;
                case "Работал на предприятии иных форм": return OccupationStatus.WorkOther;
                case "Работает в сельском хозяйстве": return OccupationStatus.WorkOnAgriculture;
                case "Работает на государственном предприятии": return OccupationStatus.WorkOnStateEnterprise;
                case "Работает на коммерческом предприятии": return OccupationStatus.WorkOnCommercialEnterprise;
                case "Работает на предприятии иных форм": return OccupationStatus.WorkOther;
            }

            return OccupationStatus.None;
        }
    }
}
