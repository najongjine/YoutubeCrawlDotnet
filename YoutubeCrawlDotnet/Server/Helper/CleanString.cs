using System.Linq;
using System.Text.RegularExpressions;

namespace YoutubeCrawlDotnet.Server.Helper
{
    public class CleanString
    {
        public string RemoveEmojisSChars(string original)
        {
            string emojiRemoved = Regex.Replace(original, @"\p{Cs}", "");
            char[] charsToReplace = new char[] { ':', '[','【','】','「','」','!','$','%','^','/','`','~','*','"',',','_','&', '#' , '^', '@', ']' , '|' };
            string specialCharRemoved = charsToReplace.Aggregate(emojiRemoved, (ch1, ch2) => ch1.Replace(ch2, '-'));
            return specialCharRemoved;
        }
    }
}
