using System.Collections;
using Crypto;

static class Global
{
    public static readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static readonly string[] quotes = new string[] {
        "Life is not a problem to be solved, but a reality to be experienced.",
        "God is dead. God remains dead. And we have killed him.",
        "Without music, life would be a mistake.",
        "Be kind, for everyone you meet is fighting a hard battle.",
        "Kind words do not cost much. Yet they accomplish much.",
        "In oppressing, one becomes oppressed.",
    };
}

namespace Crypto
{
    public class SubstitutionCipher
    {
        private Dictionary<char, char> cipher;

        // Cipher, except it maps the from value to key, used for building cipher text
        // private Dictionary<char, char> reverse_cipher;
        public SubstitutionCipher()
        {
            cipher = new Dictionary<char, char>();
            // reverse_cipher = new Dictionary<char, char>();
            // Generate permutation

            var AlphaPool = new List<char>(Global.alphabet);
            foreach (char character in Global.alphabet)
            {
                var rand = new Random();
                var rand_num = rand.Next(0, AlphaPool.Count);
                cipher.Add(character, AlphaPool[rand_num]);
                // reverse_cipher.Add(AlphaPool[rand_num], character);
                AlphaPool.RemoveAt(rand_num);
            }
        }

        public char get(char key)
        {
            // If not in dict, return itself
            if (cipher.ContainsKey(key))
            {
                return cipher[key];
            }
            return key;
        }

        public string GetCipherText(string plaintext)
        {

            string cipher_text = plaintext.Aggregate("", (accum, letter) =>
            {
                if (cipher.ContainsKey(letter))
                {
                    return accum + cipher[letter];
                }
                else
                {
                    return accum + letter;
                }
            });
            return cipher_text;
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
        private SubstitutionCipher cipher;
        private string cipher_text;
        private Dictionary<char, char> user_mapping;
        private string target_quote;

        public NSAGame()
        {
            this.cipher = new SubstitutionCipher();
            cipher.Print();
            this.user_mapping = new Dictionary<char, char>();
            foreach (char letter in Global.alphabet)
            {
                this.user_mapping.Add(letter, '_');
            }

            // Generate a random quote
            var rand = new Random();
            var rand_num = rand.Next(0, Global.quotes.Length);
            target_quote = Global.quotes[rand_num].ToUpper();

            cipher_text = cipher.GetCipherText(target_quote);
        }

        // Returns true if correct
        public bool SetUserMapping(char key, char value)
        {

            if (this.user_mapping.ContainsKey(key))
            {
                Console.WriteLine($"{key}, {value}");

                this.user_mapping[key] = value;
            }

            Console.WriteLine($"quote =  {this.target_quote}");
            Console.WriteLine($"user = {this.GetUserQuote()}");
            if (this.target_quote == this.GetUserQuote())
            {
                return true;
            }

            return false;
        }

        // Returns text based on user's translation
        public string GetUserQuote()
        {
            return cipher_text.Aggregate("", (accum, curr) =>
            {
                if (user_mapping.ContainsKey(curr))
                {
                    return accum + user_mapping[curr];
                }
                else
                {
                    return accum + curr;
                }
            });
        }
        public void PrintUserQuote()
        {
            Console.WriteLine(this.GetUserQuote());

            // Print original word
            Console.WriteLine(cipher_text);
        }
    }
}


namespace TestingGame
{
    public class TestNSAGame
    {
        public static void RunTestGame()
        {
            var nsagame = new Game.NSAGame();
            while (true)
            {
                nsagame.PrintUserQuote();
                Console.Write("Enter a letter and what it's substitute letter is: ");
                var input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("\nDeath");
                    return;
                }
                string[] inputs = input.Split(" ");
                if (inputs.Length != 2 || inputs[0].Length != 1 || inputs[1].Length != 1)
                {
                    Console.WriteLine("\nExpected input: [letter] [letter]");
                    continue;
                }

                var key = inputs[0].ToUpper()[0];
                var value = inputs[1].ToUpper()[0];
                bool result = nsagame.SetUserMapping(key,  value);
                if (result == true)
                {
                    Console.WriteLine("Congrats! You got it!");
                    return;
                }
            }
        }
    }
}