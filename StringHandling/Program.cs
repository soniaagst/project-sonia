using System.Globalization;

string invariant = "iii".ToUpperInvariant();
CultureInfo turkey = new CultureInfo("tr-TR");
Thread.CurrentThread.CurrentCulture = turkey;
string cultured = "iii".ToUpper();
Console.WriteLine(invariant);
Console.WriteLine(cultured);

string whitespaceString = "  ";
string emptyString = "";
string? nullString = null;
Console.WriteLine(string.IsNullOrEmpty(whitespaceString) + " " + string.IsNullOrEmpty(emptyString) + " " + string.IsNullOrEmpty(nullString));
Console.WriteLine(string.IsNullOrWhiteSpace(whitespaceString) + " " + string.IsNullOrWhiteSpace(emptyString) + " " + string.IsNullOrWhiteSpace(nullString));
Console.WriteLine(emptyString == nullString);

string s = "  the hash slinging slasher  ";
Console.WriteLine(s.Contains("hash"));
Console.WriteLine(s.StartsWith("slinging"));
Console.WriteLine(s.EndsWith("slasher  "));
Console.WriteLine(s.IndexOf("hash"));
char[] chars = ['a', ' ', 's'];
Console.WriteLine(s.IndexOfAny(chars));
Console.WriteLine(s.Length);
Console.WriteLine(s.Count());
Console.WriteLine(s.Substring(14,9));
Console.WriteLine(s.Insert(20, "flying "));
Console.WriteLine(s.Remove(6, 5));
Console.WriteLine(s.Replace("hash", "cash"));
Console.WriteLine(s.PadLeft(32, 'w'));
Console.WriteLine(s.Trim());

string[] words = s.Split(' ');
foreach (string word in words)
{
    Console.WriteLine($"- {word}");
}
Console.WriteLine(string.Join("", words));
