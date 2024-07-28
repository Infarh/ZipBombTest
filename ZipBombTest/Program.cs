using System.IO.Compression;
using System.Text;
using ZipBombTest.IO;

Console.Clear();

var data = Enumerable.Repeat(0, 1024 * 1024 * 1024).ToByteEnum().ToArray();
var data_stream = Enumerable.Repeat(0, 1024 * 1024 * 1024).ToByteEnum().ToStream();

using (var zip = new ZipArchive(File.Create("test.zip"), ZipArchiveMode.Create))
{
    var entry = zip.CreateEntry("file.bin", CompressionLevel.SmallestSize);
    using var zip_stream = entry.Open();
    data_stream.CopyTo(zip_stream);
}

using (var zip = ZipFile.OpenRead("test.zip"))
{
    var entry = zip.Entries.First();
    var crc = entry.Crc32;
    Console.WriteLine($"{crc:x8}");
}

return;

var compressed_data = new MemoryStream();
using (var deflate = new DeflateStream(compressed_data, CompressionLevel.SmallestSize))
    Enumerable.Repeat(0, 1024 * 1024 * 1024).ToByteEnum().ToStream().CopyTo(deflate);

const string file_name = "0";
var file_name_bytes = Encoding.UTF8.GetBytes(file_name);

LocalFileHeader file_header = new()
{
    ModificationTime = 0,
    ModificationDate = 0,
    CRC32 = 0,
    CompressedSize = (uint)compressed_data.Length,
    UncompressedSize = 1024,
    FileNameLength = (uint)file_name_bytes.Length,
    ExtraFieldLength = 0,
};



Console.WriteLine("End.");

return;

