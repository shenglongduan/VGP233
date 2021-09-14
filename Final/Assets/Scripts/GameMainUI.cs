using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainUI : MonoBehaviour
{
    public Text GameTimesTex;

    public Text WinTimesTex;

    void Start()
    {
        int gameTimes = PlayerPrefs.GetInt("GameTimes", 0);
        int winTimes = PlayerPrefs.GetInt("WinTimes", 0);
        GameTimesTex.text = $"Game Times: {gameTimes}";
        WinTimesTex.text = $"Win Times: {winTimes}";
    }
}