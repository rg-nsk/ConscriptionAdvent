using ConscriptionAdvent.Domain.Enums;

namespace ConscriptionAdvent.Domain.ExtensionMethods.EnumExtensions
{
    public static class ThemesExtensions
    {
        public static string ToThemeString(this Themes source)
        {
            switch (source)
            {
                case Themes.LightGreen: return "LightGreen";
                case Themes.LightBlue: return "LightBlue";
                case Themes.LightIndigo: return "LightIndigo";
                case Themes.LightAmber: return "LightAmber";
                case Themes.LightPurple: return "LightPurple";
                case Themes.DarkBlue: return "DarkBlue";
                case Themes.DarkRed: return "DarkRed";
                case Themes.DarkTeal: return "DarkTeal";
                case Themes.DarkPink: return "DarkPink";
                case Themes.DarkPurple: return "DarkPurple";
                case Themes.DarkIndigo: return "DarkIndigo";
                case Themes.DarkAmber: return "DarkAmber";
                case Themes.DarkOrange: return "DarkOrange";
                case Themes.DarkLime: return "DarkLime";
            }

            return string.Empty;
        }

        public static Themes ToThemeEnum(this string source)
        {
            switch (source)
            {
                case "LightGreen": return Themes.LightGreen;
                case "LightBlue": return Themes.LightBlue;
                case "LightIndigo": return Themes.LightIndigo;
                case "LightAmber": return Themes.LightAmber;
                case "LightPurple": return Themes.LightPurple;
                case "DarkBlue": return Themes.DarkBlue;
                case "DarkRed": return Themes.DarkRed;
                case "DarkTeal": return Themes.DarkTeal;
                case "DarkPink": return Themes.DarkPink;
                case "DarkPurple": return Themes.DarkPurple;
                case "DarkIndigo": return Themes.DarkIndigo;
                case "DarkAmber": return Themes.DarkAmber;
                case "DarkOrange": return Themes.DarkOrange;
                case "DarkLime": return Themes.DarkLime;
            }

            return Themes.LightGreen;
        }
    }
}
