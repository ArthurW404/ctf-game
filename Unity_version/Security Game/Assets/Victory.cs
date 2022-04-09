using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Victory : MonoBehaviour
{
    public void GoBackHome()
    {
        SceneManager.LoadScene("Menu");
    }
    // Start is called before the first frame update
    void Start()
    {
        var scoreComp = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        var resultText = "Time taken: " + GameMaster.timeElapsed.ToString() + " Seconds";
        foreach (var item in GameMaster.otherScores)
        {
            resultText = resultText + "\n" + item.Key + " : " + item.Value;
        }
        scoreComp.SetText(resultText);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
