// See https://aka.ms/new-console-template for more information
using HtmlAgilityPack;
using System.Text;
using System.Xml.Linq;

var url = @"https://docs.google.com/document/d/e/2PACX-1vQGUck9HIFCyezsrBSnmENk5ieJuYwpt7YHYEzeNJkIb9OSDdx-ov2nRNReKQyey-cwJOoEKUhLmN9z/pub";
Console.WriteLine(SomeMethod(null));
Console.ReadKey(); 
string SomeMethod(string originalZip)
{
    string zip = "Zip Not Available";
    zip = GetZip(zip, originalZip);
    return zip;
}

string GetZip_Mine(string originalZip)
{
    return originalZip?.PadLeft(5, '0') ?? "Zip Not Available";
}

string GetZip(string zip, string OriginalZip)
{
    int zipLength = OriginalZip?.ToString().Length ?? 0;
    int NumOfLeadingZeroes = (zipLength > 0 && zipLength < 5) ? 5 - zipLength : 0;



    if (!(OriginalZip is null))
    {
        zip = OriginalZip.ToString();
        for (int i = 0; i < NumOfLeadingZeroes; i++)
        {
            zip = "0" + zip;
        }
    }



    return zip;
}
int[] MoveZerosToEnd(int[] input)
{
    var times = input.Count(c => c == 0);
    for (int i = 0; i < times; i++)
    {
        for (int j = 0; j < input.Length - 1; j++)
        {
            var first = input[j];
            var second = input[j + 1];
            if (first == 0 && second != 0)
            {
                input[j] = second;
                input[j + 1] = first;
            }
        }
        Console.WriteLine(string.Join(',', input));
        Console.ReadKey();
    }
    return input;
}
string IdentifyAdjacent(string s, int k)
{
    if (s.Length < k)
        return s;
    for (int i = s.Length - 1; i >= 0; i--)
    {
        if (k > s.Length)
            return s;
        var current = s[i];
        bool remove = false;
        if (i == 0)
            return s;
        for (int j = 1; j < k; j++)
        {
            if (current != s[i - j])
                break;
            if (j == (k - 1))
                remove = true;
        }
        if (remove)
        {
            s = s.Remove(i - (k - 1), k);
            break;
        }
    }
    return IdentifyAdjacent(s, k);
}
static string[] UniqueNames(string[] names1, string[] names2)
{
    var list = new List<string>(names1);
    foreach (var name in names2)
    {
        if (!list.Contains(name))
            list.Add(name);

    }
    return [.. list];
}
void Run(string url)
{
    using (var http = new HttpClient())
    {
        var stream = http.GetStreamAsync(url);
        using (var reader = new StreamReader(stream.Result))
        {
            var indexes = new List<int>();
            var json = reader.ReadToEnd();
            var bytes = Encoding.Unicode.GetBytes(json);
            var rows = new List<Dictionary<int, string>>();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(json);
            var test = doc.DocumentNode.SelectNodes("table");
            for (var i = 0; i < bytes.Length; i = i + 2)
            {
                var val = i + 1;
                if (bytes[val] > 0)
                {
                    indexes.Add(i);
                    var newbytes = new byte[354];
                    var k = 0;
                    for (int j = i - 176; j < val + 176; j++)
                    {
                        newbytes[k] = bytes[j];
                        k++;
                    }

                    var row = GetRowNumber(newbytes);
                    var col = GetColNumber(newbytes);
                    while (rows.Count < row + 1)
                    {
                        rows.Add([]);
                    }

                    rows[row][col] = Encoding.Unicode.GetString([bytes[val - 1], bytes[val]]);
                }

            }
            var fs = new FileStream("result.txt", FileMode.Create);
            using (var writer = new StreamWriter(fs))
            {
                for (byte i = 120; i < 143; i++)
                    writer.Write(Encoding.Unicode.GetString([i, 37]));
                writer.Write(writer.NewLine);
                writer.Write(writer.NewLine);
                for (int q = rows.Count - 1; q >= 0; q--)
                {
                    var row = rows[q];
                    for (int x = 0; x <= row.Keys.Max(); x++)
                    {
                        if (row.TryGetValue(x, out string? value))
                            writer.Write(value);
                        else
                            writer.Write(Encoding.Unicode.GetString([120, 37]));
                    }
                    writer.Write(writer.NewLine);
                }
            }
        }
    }
}
int GetColNumber(byte[] newbytes)
{
    int current = 0;
    var sb = new StringBuilder();
    string currentChar;
    do
    {
        currentChar = Encoding.Unicode.GetString([newbytes[current], newbytes[current + 1]]);
        if (currentChar != "<" && currentChar != ">")
            sb.Append(currentChar);
        current = current + 2;
    } while (currentChar != "<");
    return int.Parse(sb.ToString());
}

int GetRowNumber(byte[] originalBytes)
{
    var originalString = Encoding.Unicode.GetString(originalBytes);
    var newString = originalString.TrimEnd('<').TrimStart('>');
    var lastChar = newString[^1];
    return int.Parse(lastChar.ToString());
}
