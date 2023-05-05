using System.Text.RegularExpressions;

namespace CakeShop.Extension
{
    public static class Extension
    {
        public static string ToVnd(this double donGia)
        {
            return donGia.ToString("#,##0")+ " vnd";

        }

        public static string ToTitleCase(string str )
        {
            string result = str;
            if(!string.IsNullOrEmpty( str ) )
            {
                var words = str.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    var s= words[i];
                    if( s.Length > 0 )
                    {
                        words[i] = s[0].ToString().ToUpper()+s.Substring(1);
                    }
                }
                result= string.Join(" ", words);
            }
            return result;
        }

    }
}
