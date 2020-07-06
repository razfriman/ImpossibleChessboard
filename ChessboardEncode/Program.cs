using System;

namespace ChessboardEncode
{
    public class Program
    {
        public static void Main()
        {
            var values = new[]
            {
                true, true, false, true, false, true, true, false,
                true, true, false, true, false, true, true, false,
                true, true, true , true, false, true, true, false,
                true, true, false, true, false, true, true, false,
                true, true, false, true, false, true, true, false,
                true, true, false, true, false, true, true, false,
                true, true, false, true, false, true, true, false,
                true, true, false, true, false, true, true, false,
            };
            var targetIndex = 33;
            var chessboard = new Chessboard(values);
            var flipIndex = chessboard.FindFlipForKey(targetIndex);
            var (flipX, flipY) = chessboard.IndexToPosition(flipIndex);
            values[flipIndex] = !values[flipIndex];
            var resultChessboard = new Chessboard(values);
            Console.WriteLine("Original Board");
            Console.WriteLine(chessboard.Dump());
            Console.WriteLine("Flipped Board");
            Console.WriteLine(resultChessboard.Dump());
            Console.WriteLine($"flipX: {flipX} flipY: {flipY} (flipIndex: {flipIndex})");
        }
    }
}