using NUnit.Framework;

namespace ReversiTest
{
    public class Tests
    {
        [Test]
        public void TestGame0()
        {
            Reversi.Reversi game0 = new Reversi.Reversi();
            Assert.True(game0.CheckValidStroke("D", 3));
            Assert.True(game0.CheckValidStroke("C", 4));
            Assert.True(game0.CheckValidStroke("F", 5));
            Assert.True(game0.CheckValidStroke("E", 6));
            Assert.False(game0.CheckValidStroke("A", 1));
            Assert.False(game0.CheckValidStroke("H", 8));
        }

        [Test]
        public void TestGameB1()
        {
            string playerBlack = "B";
            string[,] gridReversi1 = new string[8, 8] { 
                { ".", ".", "B", "W", ".", "B", ".", "."}, 
                { "W", "W", "B", "B", "B", "B", "B", "B"},
                { ".", "W", "W", "W", "W", "W", ".", "."},
                { ".", "B", "B", "B", "W", ".", ".", "."},
                { "B", "B", "B", "W", "B", ".", ".", "."},
                { ".", "B", "W", "W", "B", ".", ".", "."},
                { "B", "W", ".", "W", ".", ".", ".", "."},
                { ".", ".", ".", ".", ".", ".", ".", "."}
            };
            Reversi.Reversi gameB1 = new Reversi.Reversi(gridReversi1, playerBlack);
            Assert.True(gameB1.CheckValidStroke("H", 1));
            Assert.True(gameB1.CheckValidStroke("H", 4));
            Assert.True(gameB1.CheckValidStroke("A", 1));
            Assert.True(gameB1.CheckValidStroke("E", 6));
            Assert.False(gameB1.CheckValidStroke("A", 8));
            Assert.False(gameB1.CheckValidStroke("A", 7));
            Assert.False(gameB1.CheckValidStroke("C", 7));
            Assert.False(gameB1.CheckValidStroke("G", 1));
            
        }
        
        [Test]
        public void TestGameW1()
        {
            string playerWhite = "W";
            string[,] gridReversi1 = new string[8, 8] { 
                { ".", ".", "B", "W", ".", "B", ".", "."}, 
                { "W", "W", "B", "B", "B", "B", "B", "B"},
                { ".", "W", "W", "W", "W", "W", ".", "."},
                { ".", "B", "B", "B", "W", ".", ".", "."},
                { "B", "B", "B", "W", "B", ".", ".", "."},
                { ".", "B", "W", "W", "B", ".", ".", "."},
                { "B", "W", ".", "W", ".", ".", ".", "."},
                { ".", ".", ".", ".", ".", ".", ".", "."}
            };
            Reversi.Reversi gameW1 = new Reversi.Reversi(gridReversi1, playerWhite);
            Assert.True(gameW1.CheckValidStroke("A", 7));
            Assert.True(gameW1.CheckValidStroke("A", 8));
            Assert.True(gameW1.CheckValidStroke("D", 1));
            Assert.True(gameW1.CheckValidStroke("E", 6));
            Assert.False(gameW1.CheckValidStroke("G", 3));
            Assert.False(gameW1.CheckValidStroke("E", 8));
            Assert.False(gameW1.CheckValidStroke("C", 7));
            Assert.False(gameW1.CheckValidStroke("G", 1));
        }
    }
}