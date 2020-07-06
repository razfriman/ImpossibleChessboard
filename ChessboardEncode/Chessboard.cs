using System;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace ChessboardEncode
{
    public class Chessboard
    {
        public bool[] Values { get; }
        public int Bits => (int) Math.Log2(Values.Length);
        public int Size => (int) Math.Sqrt(Values.Length);

        public Chessboard(bool[] values) => Values = values;

        public int FindFlipForKey(int index)
        {
            Contract.Assert(index >= 0 && index < Values.Length);
            var current = DecodeBoard();
            var currentBits = new BitArray(new[] {current});
            var keyBits = new BitArray(new[] {index});
            var deltaBits = currentBits.Xor(keyBits);
            return ToInt(deltaBits);
        }

        

        public int DecodeBoard()
        {
            var bits = new bool[Bits];
            var bitArrays = Values.Select((x, idx) => new BitArray(new[] {x ? idx : 0})).ToArray();
            for (var i = 0; i < Bits; i++)
            {
                var sum = bitArrays.Sum(x => x[i] ? 1 : 0);
                bits[i] = sum % 2 == 1;
            }

            var bitArray = new BitArray(bits);
            return ToInt(bitArray);
        }

        public (int x, int y) IndexToPosition(int index)
        {
            index %= Values.Length;
            var x = index / Size;
            var y = index % Size;
            return (x, y);
        }

        public int PositionToIndex(int x, int y) => y * Size + x;

        public string Dump()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Join("", Enumerable.Repeat('=', Size)));
            for (var x = 0; x < Size; x++)
            {
                for (var y = 0; y < Size; y++)
                {
                    sb.Append(Values[x * Size + y] ? "1" : "0");
                }

                sb.AppendLine();
            }

            sb.Append(string.Join("", Enumerable.Repeat('=', Size)));
            return sb.ToString();
        }

        public static Chessboard Random(int n)
        {
            Contract.Assert(n >= 0, $"{nameof(n)} must be greater than or equal to 0");
            Contract.Assert((n & (n - 1)) == 0, $"{nameof(n)} must be a power of 2");

            var rand = new Random();
            var values = Enumerable.Range(0, n * n).Select(x => rand.NextDouble() >= 0.5).ToArray();
            return new Chessboard(values);
        }
        
        private static int ToInt(BitArray bitArray)
        {
            var result = new int[1];
            bitArray.CopyTo(result, 0);
            return result.First();
        }
    }
}