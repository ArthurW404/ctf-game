using Game;
using System.Collections.Generic;

namespace Game
{
    public class TestGame
    {
        private (int x, int y) coordinate;
        private int[,] map;
        public TestGame(int x = 0, int y = 0)
        {
            map = new int[10, 10];
            coordinate = (x, y);
        }

        public void PrintMap()
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int i = 0; i < map.GetLength(1); i++)
                {
                    if (coordinate == (i, row))
                    {
                        Console.Write("P ");
                    }
                    else
                    {
                        Console.Write($"{map[row, i]} ");
                    }
                }
                Console.Write("\n");
            }
            Console.Write("\n");

        }

        public void Move(string direction)
        {
            switch (direction)
            {
                case "w":
                    if (coordinate.y > 0)
                    {
                        coordinate.y--;
                    }
                    break;
                case "s":
                    if (coordinate.y < 9)
                    {
                        coordinate.y++;
                    }
                    break;
                case "a":
                    if (coordinate.x > 0)
                    {
                        coordinate.x--;
                    }
                    break;
                case "d":
                    if (coordinate.x < 9)
                    {
                        coordinate.x++;
                    }
                    break;
                default:
                    break;
            }
        }

    }
}

namespace TestingGame
{
    public class TestTestGame
    {
        public static void RunTestGame()
        {
            var game = new TestGame(5, 5);

            while (true)
            {
                Console.Write("Enter input: ");
                var input = Console.ReadLine();
                if (input != null)
                    game.Move(input);
                game.PrintMap();
            }
        }
    }
}