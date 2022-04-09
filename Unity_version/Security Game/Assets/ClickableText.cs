using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class ClickableText : MonoBehaviour
{
    public Action<string> OnClick;

    // Name of gameobject in hierarchy
    public string Name;
    public TextMeshPro Text;
    // Start is called before the first frame update
    public void SetText(string text)
    {
        Text.SetText(text);
    }

    void Awake()
    {
        Text = this.gameObject.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked");
            // Whatever you want it to do.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Select stage    
                if (hit.transform.name == Name)
                {
                    OnClick(Name);
                }
            }
        }
    }
}
