using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class BirthdayAttackGame : MonoBehaviour
{

    private int targetHash;
    // private string username;
    // private string password;
    private MD5 hash;
    private TMP_InputField usernameField;
    private TMP_InputField passwordField;
    private GameObject PauseMenuCanvas;

    public void onLogin()
    {
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(passwordField.text);
        byte[] hashBytes = hash.ComputeHash(inputBytes);

        // get last 4 byte
        // Get a 4 bit number
        var resultHash = hashBytes[0] & 0xF;
        Debug.Log(resultHash);
        if (resultHash == targetHash)
        {
            SceneManager.LoadScene("Victory");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        PauseMenuCanvas = GameObject.Find("PauseMenuCanvas");
        PauseMenuCanvas.SetActive(false);
        targetHash = new System.Random().Next(16);
        Debug.Log(targetHash);
        hash = MD5.Create();
        usernameField = this.transform.GetComponentsInChildren<TMP_InputField>()[0];
        passwordField = this.transform.GetComponentsInChildren<TMP_InputField>()[1];
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
