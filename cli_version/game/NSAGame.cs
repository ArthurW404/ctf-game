using System.Collections;
using Crypto;

namespace Crypto
{
    public class SubstitutionCipher
    {
        private readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private Dictionary<char, char> cipher;
        public SubstitutionCipher()
        {
            cipher = new Dictionary<char, char>();
            // Generate permutation

            var AlphaPool = new List<char>(alphabet);
            foreach (char character in alphabet) 
            {
                var rand = new Random();
                var rand_num = rand.Next(0, AlphaPool.Count); 
                cipher.Add(character, AlphaPool[rand_num]);
                AlphaPool.RemoveAt(rand_num);
            }

            // var tempAlpha = new List<char>(alphabet);
            // while (tempAlpha.Count > 0)
            // {
            //     var rand = new Random();
            //     var rand_num = rand.Next(1, tempAlpha.Count);
            //     cipher.Add(tempAlpha[0], tempAlpha[rand_num]);
            //     // cipher.Add(tempAlpha[rand_num], tempAlpha[0]);
            //     // tempAlpha.RemoveAt(rand_num);
            //     tempAlpha.RemoveAt(0);
            // }
        }

        public void Print()
        {
            foreach (char key in cipher.Keys) 
            {
                var value = cipher[key];
                Console.WriteLine($"{key} : {value}");
            }
        }
    }
}

namespace Game
{
    public class NSAGame
    {
        public NSAGame()
        {
        }
    }
}


namespace TestingGame
{
    public class TestSubstitution
    {
        public static void RunTestGame()
        {
            var cipher = new SubstitutionCipher();
            cipher.Print();
        }
    }

}