using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Menu : MonoBehaviour
{

    public Transform ActiveMenu;

    // Start is called before the first frame update
    public Transform HomeMenu;
    public Transform LevelsMenu;

    public void GotoSelectLevels () 
    {
        ActiveMenu.gameObject.SetActive(false);
        LevelsMenu.gameObject.SetActive(true);
        ActiveMenu = LevelsMenu;
    }

    public void GoBackHome()
    {
        ActiveMenu.gameObject.SetActive(false);
        HomeMenu.gameObject.SetActive(true);
        ActiveMenu = HomeMenu;
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Start()
    {
        Debug.Log("Starting!");
        HomeMenu = this.gameObject.transform.GetChild(1);
        LevelsMenu= this.gameObject.transform.GetChild(2);
        ActiveMenu = HomeMenu;
        Debug.Log(HomeMenu);
        Debug.Log(LevelsMenu);
        // HomeMenu.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
