using ZipBombTest.IO;

Console.Clear();

using (var src = new EnumToStream(Enumerable.Range(0, 100).Select(i => (byte)i)))
{

}

Console.WriteLine("End.");

return;

