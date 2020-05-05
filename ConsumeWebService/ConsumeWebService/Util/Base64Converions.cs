using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Text;

namespace ConsumeWebService.Util
{
    public static class Base64Converions
    {
        public static String EncodeBase64(this String text, Encoding encoding = null) 
        {
            if (null == text) 
            {
                return null;
            }

            encoding = encoding ?? Encoding.UTF8;
            var bytes = encoding.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        public static String DecodeBase64(this String encodedText, Encoding encoding = null) 
        {
            if (null == encodedText) 
            {
                return null;
            }

            encoding = encoding ?? Encoding.UTF8;
            var bytes = Convert.FromBase64String(encodedText);
            return encoding.GetString(bytes);
        }
    }
}
