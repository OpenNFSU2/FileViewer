using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSU2.Compression
{
    public class JDLZCompression : ICompression
    {
        const int HEADER_SIZE = 16;

        public byte[] Decompress(byte[] input)
        {
            if (input.Length < HEADER_SIZE || input[0] != 'J' || input[1] != 'D' || input[2] != 'L' || input[3] != 'Z' || input[4] != 0x02)
            {
                throw new InvalidDataException("Input header is not JDLZ!");
            }

            int flags1 = 1, flags2 = 1;
            int t, length;
            int inPos = HEADER_SIZE, outPos = 0;

            // TODO: Can we always trust the header's stated length?
            byte[] output = new byte[BitConverter.ToInt32(input, 8)];

            while ((inPos < input.Length) && (outPos < output.Length))
            {
                if (flags1 == 1)
                {
                    flags1 = input[inPos++] | 0x100;
                }
                if (flags2 == 1)
                {
                    flags2 = input[inPos++] | 0x100;
                }

                if ((flags1 & 1) == 1)
                {
                    if ((flags2 & 1) == 1) // 3 to 4098(?) iterations, backtracks 1 to 16(?) bytes
                    {
                        // length max is 4098(?) (0x1002), assuming input[inPos] and input[inPos + 1] are both 0xFF
                        length = (input[inPos + 1] | ((input[inPos] & 0xF0) << 4)) + 3;
                        // t max is 16(?) (0x10), assuming input[inPos] is 0xFF
                        t = (input[inPos] & 0x0F) + 1;
                    }
                    else // 3(?) to 34(?) iterations, backtracks 17(?) to 2064(?) bytes
                    {
                        // t max is 2064(?) (0x810), assuming input[inPos] and input[inPos + 1] are both 0xFF
                        t = (input[inPos + 1] | ((input[inPos] & 0xE0) << 3)) + 17;
                        // length max is 34(?) (0x22), assuming input[inPos] is 0xFF
                        length = (input[inPos] & 0x1F) + 3;
                    }

                    inPos += 2;

                    for (int i = 0; i < length; ++i)
                    {
                        output[outPos + i] = output[outPos + i - t];
                    }

                    outPos += length;
                    flags2 >>= 1;
                }
                else
                {
                    if (outPos < output.Length)
                    {
                        output[outPos++] = input[inPos++];
                    }
                }
                flags1 >>= 1;
            }
            return output;
        }
    }
}
