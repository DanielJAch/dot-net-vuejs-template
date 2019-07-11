namespace DotNETVueJSTemplate.Services.Validation
{
    public static class Constants
    {
        public static string UsPhoneRegEx => @"^((\+1)|(1))?\s?-?[(]?[23456789]\d{2}[)]?\s?-?\s?[23456789]\d{2}\s?-?\s?\d{4}$";

        public static string LowerCaseRegEx => @"[a-z]";

        public static string UpperCaseRegEx => @"[A-Z]";

        public static string DigitRegEx => @"\d";

        public static string SpecialCharacterRegEx => @"[^a-zA-Z\d\s]";

        public static string Base64StringRegEx => @"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$";

        public static string ImageFileExtensionRegEx => @"^image\/(png|gif|jpg)";

        public static int DaysAfterBillingPeriodStartToEditAllotmentInfo => 3;
    }
}
