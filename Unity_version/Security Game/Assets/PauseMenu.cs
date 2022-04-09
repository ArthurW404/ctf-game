using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public void onRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void onClose()
    {
        Debug.Log("Closing");
        // Make this become invinsible
        this.gameObject.SetActive(false);
    }

    public void onQuit()
    {
        SceneManager.LoadScene("Menu");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
