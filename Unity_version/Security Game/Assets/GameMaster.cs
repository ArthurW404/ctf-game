using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static int timeElapsed;
    public static Dictionary<string, string> otherScores;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
