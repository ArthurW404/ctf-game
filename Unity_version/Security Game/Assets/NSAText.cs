using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NSAText : MonoBehaviour
{
    public TextMeshProUGUI m_MyText;
    private NSAGame nsagame;
    // Start is called before the first frame update
    void Start()
    {
        nsagame = GetComponentInParent<NSAGame>();
        nsagame.OnUpdate += OnNSAGameUpdate;
        m_MyText = GetComponent<TextMeshProUGUI>();
    }

    private void OnNSAGameUpdate(object sender, System.EventArgs e)
    {
        m_MyText.SetText("<mspace=0.7em>" + nsagame?.GetUserQuote());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
