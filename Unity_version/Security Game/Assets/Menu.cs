using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class Menu : MonoBehaviour
{

    private Transform _ActiveMenu;

    // Start is called before the first frame update
    private Transform _HomeMenu;
    private Transform _LevelsMenu;
    public void StartNSAGame () 
    {
        SceneManager.LoadScene("NSAGame");
    }

   public void StartBirthdayAttack () 
    {
        SceneManager.LoadScene("BirthdayAttack");
    }


   public void StartHashCracking () 
    {
        SceneManager.LoadScene("HashCracking");
    }

    public void StartQuiz () 
    {
        SceneManager.LoadScene("Quiz");
    }

    public void GotoSelectLevels () 
    {
        _ActiveMenu.gameObject.SetActive(false);
        _LevelsMenu.gameObject.SetActive(true);
        _ActiveMenu = _LevelsMenu;
    }

    public void GoBackHome()
    {
        _ActiveMenu.gameObject.SetActive(false);
        _HomeMenu.gameObject.SetActive(true);
        _ActiveMenu = _HomeMenu;
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Start()
    {
        _HomeMenu = this.gameObject.transform.GetChild(1);
        _LevelsMenu= this.gameObject.transform.GetChild(2);
        _ActiveMenu = _HomeMenu;
        // _HomeMenu.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
