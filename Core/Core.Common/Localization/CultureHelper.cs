using PostSharp.Patterns.Contracts;
using System;
using System.Globalization;

namespace ConventionsAide.Core.Common.Localization
{
    public static class CultureHelper
    {
        public static bool IsRtl => CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;

        public static IDisposable Use([NotNull] string culture, string uiCulture = null)
        {
            return Use(new CultureInfo(culture), (uiCulture == null) ? null : new CultureInfo(uiCulture));
        }

        public static IDisposable Use([NotNull] CultureInfo culture, CultureInfo uiCulture = null)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            CultureInfo currentUiCulture = CultureInfo.CurrentUICulture;
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = uiCulture ?? culture;
            return new DisposeAction(delegate
            {
                CultureInfo.CurrentCulture = currentCulture;
                CultureInfo.CurrentUICulture = currentUiCulture;
            });
        }

        public static bool IsValidCultureCode(string cultureCode)
        {
            if (cultureCode.IsNullOrWhiteSpace())
            {
                return false;
            }

            try
            {
                CultureInfo.GetCultureInfo(cultureCode);
                return true;
            }
            catch (CultureNotFoundException)
            {
                return false;
            }
        }

        public static string GetBaseCultureName(string cultureName)
        {
            if (!cultureName.Contains("-"))
            {
                return cultureName;
            }

            return cultureName.Left(cultureName.IndexOf("-", StringComparison.Ordinal));
        }
    }
}
