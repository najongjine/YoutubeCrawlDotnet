using System.Text.RegularExpressions;

namespace YoutubeCrawlDotnet.Server.Helper
{
  public class CleanString
  {
    public string RemoveEmojisSChars(string original)
    {
      string emojiRemoved = Regex.Replace(original, @"\p{Cs}", "");
      string specialCharRemoved = Regex.Replace(emojiRemoved, "[【】「」!$%^/`~*'\",_&#^@]", "");
      return specialCharRemoved;
    }
  }
}
