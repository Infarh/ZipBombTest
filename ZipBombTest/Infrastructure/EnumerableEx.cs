using ZipBombTest.IO;

namespace ZipBombTest.Infrastructure;

internal static class EnumerableEx
{
    public static EnumToStream ToStream(this IEnumerable<byte> items) => new(items);
}