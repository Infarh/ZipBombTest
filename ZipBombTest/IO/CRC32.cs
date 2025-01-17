namespace ZipBombTest.IO;

public class CRC32(uint Polynomial = 0xEDB88320)
{
    public static uint[] GetTable(uint Polynomial = 0xEDB88320)
    {
        var table = new uint[__TableLength];

        FillTable(table, Polynomial);

        return table;
    }

    public static void FillTable(uint[] table, uint Polynomial)
    {
        for (uint i = 0; i < __TableLength; i++)
        {
            ref var crc = ref table[i];
            crc = i << 24;
            const uint mask = 0x80_00_00_00U;
            for (var bit = 0; bit < 8; bit++)
                crc = (crc & mask) != 0
                    ? crc << 1 ^ Polynomial
                    : crc << 1;
        }
    }

    public static uint Hash(
        byte[] data,
        uint poly = 0xEDB88320,
        uint crc = 0xFFFFFFFF)
    {
        foreach (var b in data)
            crc = crc << 8 ^ Table(b ^ crc >> 24, poly);

        return crc;

        static uint Table(uint i, uint poly)
        {
            var table = i << 24;
            const uint mask = 0b10000000_00000000_00000000_00000000U;
            for (var bit = 0; bit < 8; bit++)
                table = (table & mask) != 0
                    ? table << 1 ^ poly
                    : table << 1;

            return table;
        }
    }

    private const int __TableLength = 256;

    private readonly uint[] _Table = GetTable(Polynomial);

    public uint Compute(uint crc, byte b) => _Table[(crc ^ b) & 0xff] ^ (crc >> 8);

    public uint ContinueCompute(uint crc, byte[] bytes)
    {
        foreach (var b in bytes)
            crc = crc << 8 ^ _Table[crc >> 24 ^ b];

        return crc;
    }

    public uint ContinueCompute(uint crc, Span<byte> bytes)
    {
        foreach (var b in bytes)
            crc = crc << 8 ^ _Table[b ^ crc >> 24];

        return crc;
    }

    public uint ContinueCompute(uint crc, ReadOnlySpan<byte> bytes)
    {
        foreach (var b in bytes)
            crc = crc << 8 ^ _Table[b ^ crc >> 24];

        return crc;
    }

    public uint ContinueCompute(uint crc, IEnumerable<byte> bytes)
    {
        foreach (var b in bytes)
            crc = crc << 8 ^ _Table[b ^ crc >> 24];

        return crc;
    }

    public void Compute(ref uint crc, byte[] bytes)
    {
        foreach (var b in bytes)
            crc = crc << 8 ^ _Table[b ^ crc >> 24];
    }

    public void Compute(ref uint crc, params IEnumerable<byte> bytes)
    {
        foreach (var b in bytes)
            crc = crc << 8 ^ _Table[b ^ crc >> 24];
    }
}
