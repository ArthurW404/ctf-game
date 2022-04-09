using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

using System;
using Crypto;
using System.Linq;
using System.Timers;

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
            get
            {
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
                this.user_mapping[key] = value;
            }

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
    private GameObject PauseMenuCanvas;
    private GameObject GameCanvas;

    public void SetUserMapping(char key, char value)
    {
        bool result = nsagame.SetUserMapping(key, value);
        OnUpdate?.Invoke(this, EventArgs.Empty);

        if (result == true)
        {
            GameMaster.timeElapsed = ((int)Time.timeSinceLevelLoad);
            // Change to victory scene
            Debug.Log("Victory");
            SceneManager.LoadScene("Victory");
            return;
        }

    }

    public string GetUserQuote()
    {
        if (nsagame != null)
        {
            // Partition into 25 character pieces, then alternate between plain and cipher
            var chunksize = 20;
            var plaintext = nsagame.GetUserQuote();
            var cipher = nsagame.cipher_text;

            var result_str = "";
            string[] plaintext_words = plaintext.Split(' ');
            string[] cipher_words = cipher.Split(' ');

            string plaintext_sentence = "";
            string cipher_sentence = "";

            // Assuming word is not more than 20 characters 
            for (var i = 0; i < plaintext_words.Length; ++i)
            {
                if (plaintext_sentence.Length + plaintext_words[i].Length >= chunksize)
                {
                    // New line
                    result_str += plaintext_sentence + "\n";
                    result_str += cipher_sentence + "\n";
                    plaintext_sentence = "";
                    cipher_sentence = "";
                }
                plaintext_sentence += plaintext_words[i] + " ";
                cipher_sentence += cipher_words[i] + " ";
            }

            if (plaintext.Length != 0)
            {
                result_str += plaintext_sentence + "\n";
                result_str += cipher_sentence + "\n";
            }
            return result_str;
        }
        // Print original word
        // Console.WriteLine(nsagame.cipher_text);
        return "";
    }
    // Start is called before the first frame update


    async void Start()
    {
        PauseMenuCanvas = GameObject.Find("PauseMenuCanvas");
        PauseMenuCanvas.SetActive(false);
        GameCanvas = GameObject.Find("Game");
        GameCanvas.SetActive(false);
        nsagame = new Game.NSAGameHandler();
        OnUpdate?.Invoke(this, EventArgs.Empty);

        
        await Task.Delay(1800);
        GameCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuCanvas.SetActive(!PauseMenuCanvas.activeSelf);
        }
    }
}
