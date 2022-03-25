using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System;
using Crypto;
using System.Linq;

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
                var rand = new System.Random();
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
                Debug.Log($"{key} : {value}");
            }
        }
    }
}

namespace Game
{
    public class NSAGameHandler
    {
        private SubstitutionCipher cipher;
        private string _cipher_text;
        public string cipher_text
        {
            get {
                return _cipher_text;
            }
            private set 
            {
                _cipher_text = value;
            }
        }
        private Dictionary<char, char> user_mapping;
        private string target_quote;

        public NSAGameHandler()
        {
            this.cipher = new SubstitutionCipher();
            cipher.Print();
            this.user_mapping = new Dictionary<char, char>();
            foreach (char letter in Global.alphabet)
            {
                this.user_mapping.Add(letter, '_');
            }

            // Generate a random quote
            var rand = new System.Random();
            var rand_num = rand.Next(0, Global.quotes.Length);
            target_quote = Global.quotes[rand_num].ToUpper();

            cipher_text = cipher.GetCipherText(target_quote);
        }

        // Returns true if correct
        public bool SetUserMapping(char key, char value)
        {

            if (this.user_mapping.ContainsKey(key))
            {
                Debug.Log($"{key}, {value}");

                this.user_mapping[key] = value;
            }

            Debug.Log($"quote =  {this.target_quote}");
            Debug.Log($"user = {this.GetUserQuote()}");
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
    }
}


public class NSAGame : MonoBehaviour
{
    public event EventHandler OnUpdate;
    public Game.NSAGameHandler nsagame;
    public Transform TextTransform;
    
    public string GetUserQuote()
    {
        if (nsagame != null) {
            // Partition into 25 character pieces, then alternate between plain and cipher
            var chunksize = 20;
            var plaintext = nsagame.GetUserQuote();
            var cipher = nsagame.cipher_text; 
            var num_chunks = plaintext.Length % chunksize == 0 ?  plaintext.Length / chunksize : plaintext.Length / chunksize + 1;
            var plaintextChunks = Enumerable.Range(0, num_chunks)
                .Select(i => plaintext.Substring(i * chunksize, Math.Min(chunksize, plaintext.Length - i * chunksize))).ToArray();
            var cipherChunks = Enumerable.Range(0, num_chunks)
                .Select(i => cipher.Substring(i * chunksize, Math.Min(chunksize, cipher.Length  - i * chunksize))).ToArray();
            var result_str = "";
            for (var i = 0; i < plaintextChunks.Length; ++i) 
            {
                result_str += plaintextChunks[i] + "\n";
                result_str += cipherChunks[i] + "\n";
            }
            return result_str;
        }
        // Print original word
        // Console.WriteLine(nsagame.cipher_text);
        return "";
    }
    // Start is called before the first frame update

    void Start()
    {
        nsagame = new Game.NSAGameHandler();
        Debug.Log(nsagame.GetUserQuote());
        OnUpdate?.Invoke(this, EventArgs.Empty);

        // var test = this.transform.GetChild(0);
        // // Debug.Log(test);
        // TextTransform = test.GetChild(0);
        // Debug.Log(TextTransform.gameObject);
        // var text = nsagame.GetUserQuote();
        // Debug.Log(nsagame);
        // Debug.Log(text);
        // Debug.Log(TextTransform.GetComponent<UnityEngine.UI.Text>().text);
        // TextTransform.GetComponent<UnityEngine.UI.Text>().text = nsagame.GetUserQuote();
        // while (true)
        // {
        //     nsagame.PrintUserQuote();
        //     Console.Write("Enter a letter and what it's substitute letter is: ");
        //     var input = Console.ReadLine();
        //     if (input == null)
        //     {
        //         Console.WriteLine("\nDeath");
        //         return;
        //     }
        //     string[] inputs = input.Split(' ');
        //     if (inputs.Length != 2 || inputs[0].Length != 1 || inputs[1].Length != 1)
        //     {
        //         Console.WriteLine("\nExpected input: [letter] [letter]");
        //         continue;
        //     }

        //     var key = inputs[0].ToUpper()[0];
        //     var value = inputs[1].ToUpper()[0];
        //     bool result = nsagame.SetUserMapping(key,  value);
        //     if (result == true)
        //     {
        //         Console.WriteLine("Congrats! You got it!");
        //         return;
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnUpdate?.Invoke(this, EventArgs.Empty);
        }
    }
}
