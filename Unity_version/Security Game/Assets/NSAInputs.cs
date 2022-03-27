using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NSAInputs : MonoBehaviour
{
    private static readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public GameObject InputPrefab;

    public NSAGame GameInstance;

    public void ValueChangeCheck(char letter, TMP_InputField Input)
    {
        // Debug.Log("Value Changed");
        var value = Input.text.Length != 0 ? Char.ToUpper(Input.text[0]) : '_';
        GameInstance.SetUserMapping(Char.ToUpper(letter), value);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Create input fields
        foreach(var letter in alphabet)
        {
            GameObject newObj = Instantiate(InputPrefab);
            newObj.transform.SetParent(this.transform, false);
            
            var texts = newObj.transform.GetComponentsInChildren<TextMeshProUGUI>();

            texts[0].SetText("<mspace=0.7em>" + letter.ToString());
            TMP_InputField Input = newObj.transform.GetComponentsInChildren<TMP_InputField>()[0];

            // Limit input to be size 1
            Input.characterLimit = 1;
            Input.onValueChanged.AddListener(delegate {ValueChangeCheck(letter, Input); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
