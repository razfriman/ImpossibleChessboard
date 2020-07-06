using Xunit;

namespace ChessboardEncode.Test
{
    public class ChessboardTest
    {
        [Theory]
        [InlineData(new[]
        {
            true, true,
            true, true
        }, 2, 4)]
        [InlineData(
            new[]
            {
                true, true, false, true,
                true, true, false, true,
                true, true, false, true,
                true, true, false, true
            },
            4, 16)]
        [InlineData(
            new[]
            {
                true, true, false, true, true, true, false, true,
                true, true, false, true, true, true, false, true,
                true, true, false, true, true, true, false, true,
                true, true, false, true, true, true, false, true,
                true, true, false, true, true, true, false, true,
                true, true, false, true, true, true, false, true,
                true, true, false, true, true, true, false, true,
                true, true, false, true, true, true, false, true
            }, 8, 64)]
        public void ChessboardWithValuesHasCorrectSize(bool[] values, int expectedSize, int expectedLength)
        {
            var chessboard = new Chessboard(values);

            var size = chessboard.Size;
            var length = chessboard.Values.Length;

            Assert.Equal(expectedSize, size);
            Assert.Equal(expectedLength, length);
        }

        [Theory]
        [InlineData(2, 4)]
        [InlineData(4, 16)]
        [InlineData(8, 64)]
        public void ChessboardWithRandomHasCorrectSize2By2(int expectedSize, int expectedLength)
        {
            var chessboard = Chessboard.Random(expectedSize);

            var size = chessboard.Size;
            var length = chessboard.Values.Length;

            Assert.Equal(expectedSize, size);
            Assert.Equal(expectedLength, length);
        }

        [Fact]
        public void DumpSucceeds()
        {
            var chessboard = new Chessboard(new[] {true, true, true, true});

            var str = chessboard.Dump();

            Assert.Equal(
                @"==
11
11
==", str);
        }

        [Theory]
        [InlineData(new[] {true, true, true, true}, 0)]
        [InlineData(new[] {true, false, false, true}, 3)]
        [InlineData(
            new[]
            {
                true, true, false, true,
                true, true, false, false,
                true, false, false, true,
                true, true, false, true
            },
            14)]
        [InlineData(
            new[]
            {
                true, true, false, true, false, true, true, false,
                true, true, false, false, false, true, true, false,
                true, true, false, true, false, true, true, false,
                true, true, false, true, false, true, true, false,
                true, false, false, true, false, true, false, false,
                true, true, false, true, false, true, true, false,
                true, true, false, true, true, true, true, false,
                true, true, false, true, false, true, true, false,
            }, 56)]
        public void DecodeBoardSucceeds(bool[] values, int expectedIndex)
        {
            var chessboard = new Chessboard(values);

            var index = chessboard.DecodeBoard();

            Assert.Equal(expectedIndex, index);
        }

        [Fact]
        public void IndexToPositionSucceeds()
        {
            var chessboard = new Chessboard(new[] {true, true, true, true});

            var index = chessboard.IndexToPosition(1);

            Assert.Equal((0, 1), index);
        }

        [Fact]
        public void IndexToPositionWithWrapSucceeds()
        {
            var chessboard = new Chessboard(new[] {true, true, true, true});

            var index = chessboard.IndexToPosition(6);

            Assert.Equal((1, 0), index);
        }

        [Fact]
        public void PositionToIndexSucceeds()
        {
            var chessboard = new Chessboard(new[] {true, true, true, true});

            var index = chessboard.PositionToIndex(1, 1);

            Assert.Equal(3, index);
        }

        [Fact]
        public void FindFlipForKeySucceeds()
        {
            var chessboard = new Chessboard(new[] {true, true, false, true});

            var flipIndex = chessboard.FindFlipForKey(3);

            Assert.Equal(1, flipIndex);
        }

        [Fact]
        public void FindFlipForKeyMatchesDecodedBoard2By2()
        {
            var values = new[] {true, true, false, true};
            var targetIndex = 0;
            var chessboard = new Chessboard(values);
            var flipIndex = chessboard.FindFlipForKey(targetIndex);

            values[flipIndex] = !values[flipIndex];
            var resultChessboard = new Chessboard(values);
            var resultIndex = resultChessboard.DecodeBoard();

            Assert.Equal(targetIndex, resultIndex);
        }

        [Fact]
        public void FindFlipForKeyMatchesDecodedBoard8By8()
        {
            var values = new[]
            {
                true, true, false, true, false, true, true, false,
                true, true, false, true, false, true, true, false,
                true, true, true, true, false, true, true, false,
                true, true, false, true, false, true, true, false,
                true, true, false, true, false, true, true, false,
                true, true, false, true, false, true, true, false,
                true, true, false, true, false, true, true, false,
                true, true, false, true, false, true, true, false,
            };
            var targetIndex = 33;
            var chessboard = new Chessboard(values);
            var flipIndex = chessboard.FindFlipForKey(targetIndex);

            values[flipIndex] = !values[flipIndex];
            var resultChessboard = new Chessboard(values);
            var resultIndex = resultChessboard.DecodeBoard();

            Assert.Equal(targetIndex, resultIndex);
        }
    }
}