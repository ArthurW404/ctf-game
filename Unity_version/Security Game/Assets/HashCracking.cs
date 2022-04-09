using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;
using UnityEngine.SceneManagement;
public class HashCracking : MonoBehaviour
{
    private string passwordPlainText;
    private string hash;

    private TMP_InputField usernameField;
    private TMP_InputField passwordField;
    
    private GameObject PauseMenuCanvas;
    public void onLogin()
    {
        if (passwordField.text == passwordPlainText)
        {
            GameMaster.timeElapsed = (int) Time.timeSinceLevelLoad;
            SceneManager.LoadScene("Victory");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PauseMenuCanvas = GameObject.Find("PauseMenuCanvas");
        PauseMenuCanvas.SetActive(false);
        Debug.Log(PauseMenuCanvas);

        usernameField = this.transform.GetComponentsInChildren<TMP_InputField>()[0];
        passwordField = this.transform.GetComponentsInChildren<TMP_InputField>()[1];
        // Open file with all possible 
        using( Stream fileStream = File.Open("Assets/vulnerable_words.txt", FileMode.Open) ) {
             using( StreamReader reader = new StreamReader(fileStream) )
            {
                var rand = new System.Random();
                // 1390 is size of vulnerable_words file
                var rand_num = rand.Next(0, 1390);
                // Get a number random between 0 and x 
                for( int i = 0; i < rand_num; ++i )
                {
                    passwordPlainText = reader.ReadLine();
                }
            }
        }

        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(passwordPlainText);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            hash = BitConverter.ToString(hashBytes); // .NET 5 +
        }

        // Remove -
        hash = hash.Replace("-", string.Empty);

        Debug.Log(passwordPlainText);
        Debug.Log(hash);

        // Set text on clipboard to hash password detail
        var ClipBoardText = GameObject.Find("ClipboardText");
        var ClipBoardTextComp = ClipBoardText.GetComponent<TextMeshPro>();
        var prev_text = ClipBoardTextComp.text;
        ClipBoardTextComp.SetText(prev_text + "\n" + "Password Hash: " + hash);
        // texts[0].SetText("<mspace=0.7em>" + letter.ToString());
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
