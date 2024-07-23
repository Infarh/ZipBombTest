using ZipBombTest.IO;

Console.Clear();

var dest = new MemoryStream(100);

using (var src = Enumerable.Range(0, 100).Select(i => (byte)i).ToStream())
{
    src.CopyTo(dest);
}

var result = dest.ToArray();

for (var i = 0; i < result.Length; i++)
{
    Console.Write("{0,2},", result[i]);
    if ((i + 1) % 20 == 0)
        Console.WriteLine();
}

Console.WriteLine();
Console.WriteLine("End.");

return;

