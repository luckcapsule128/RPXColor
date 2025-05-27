using System.IO;
using System;

try
{
    if (args.Length != 2)
    {
        throw new ArgumentException("Incorrect number of arguments");
    }

    string output = args[1];

    byte[] rpxFile = File.ReadAllBytes(args[0]);

    byte[] searchPattern = HexStringToByteArray("4A4A4AFF00006AFF080062FF29005AFF41004AFF4A0000FF410000FF291000FF182900FF003110FF003100FF002910FF002041FF000000FF000000FF000000FF737373FF003183FF3100ACFF4A0094FF62007BFF6A0039FF6A2000FF5A3100FF414A00FF185A00FF105A00FF005A31FF004A5AFF101010FF000000FF000000FFACACACFF4A73B4FF625AD5FF8352E6FFA452ACFFAC4A83FFB4624AFF947331FF7B7329FF5A8300FF398B31FF318B5AFF398B8BFF393939FF000000FF000000FFB4B4B4FF8B9CB4FF8B8BACFF9C8BBDFFA483BDFFAC8B9CFFAC948BFF9C8B7BFF9C9C73FF94A47BFF83A47BFF7B9C83FF73948BFF8B8B8BFF000000FF000000FF");

    byte[] replacePattern = HexStringToByteArray("656565FF00127DFF18008EFF360082FF56005DFF5A0018FF4F0500FF381900FF1D3100FF003D00FF004100FF003B17FF002E55FF000000FF000000FF000000FFAFAFAFFF194EC8FF472FE3FF6B1FD7FF931BAEFF9E1A5EFF993200FF7B4B00FF5B6700FF267A00FF008200FF007A3EFF006E8AFF000000FF000000FF000000FFFFFFFFFF64A9FFFF8E89FFFFB676FFFFE06FFFFFEF6CC4FFF0806AFFD8982CFFB9B40AFF83CB0CFF5BD63FFF4AD17EFF4DC7CBFF4C4C4CFF000000FF000000FFFFFFFFFFC7E5FFFFD9D9FFFFE9D1FFFFF9CEFFFFFFCCF1FFFFD4CBFFF8DFB1FFEDEAA4FFD6F4A4FFC5F8B8FFBEF6D3FFBFF1F1FFB9B9B9FF000000FF000000FF");

    int k = HexScan(rpxFile, searchPattern);

    Console.WriteLine($"Found at {k:X}");


}


catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
    Console.WriteLine("Usage: RPXColor.exe <input.rpx> <output.rpx>");
}

static byte[] HexStringToByteArray(string hex)
{
    hex = hex.Replace("\n", "").Replace("/r", "").Replace(" ", "");

    if (hex.Length % 2 != 0)
    {
        throw new FormatException("String sequence must be even for bytes");
    }

    byte[] slicedBytes = new byte[hex.Length / 2];

    for (int i = 0; i < hex.Length; i += 2)
    {
        slicedBytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
    }

    return slicedBytes;
}

static int HexScan(byte[] file, byte[] pattern)
{

    for (int i = 0; i <= file.Length - pattern.Length; i++)
    {
        bool match = true;

        for (int j = 0; j < pattern.Length; j++)
        {
            if (file[i + j] != pattern[j])
            {
                match = false;
                break;
            }
        }

        if (match)
        {
            return i;
        }

    }

    return -1;

}