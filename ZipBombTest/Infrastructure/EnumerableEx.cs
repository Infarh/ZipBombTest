using ZipBombTest.IO;

namespace ZipBombTest.Infrastructure;

internal static class EnumerableEx
{
    public static EnumToStream ToStream(this IEnumerable<byte> items) => new(items);

    public static IEnumerable<byte> ToByteEnum(this IEnumerable<int> ints) => ints.Select(s => (byte)s);
}