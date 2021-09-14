using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameSetting Setting;

    public static GameManager Instance { get; private set; }
    public int currentLevel;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        currentLevel = 1;
        Instance = this;
        // GameSetting setting = new GameSetting();
        // setting.RunSpeed = 10;
        // setting.RunTime = 60;
        // var l = JsonUtility.ToJson(setting);
        // Debug.Log(l);
        // var streamWriter = File.CreateText(Application.streamingAssetsPath + "/set");
        // streamWriter.Write(l);
        // streamWriter.Flush();
    }

    private IEnumerator Start()
    {
        string setPath;
#if UNITY_EDITOR_OSX
        setPath = "file://" + Application.streamingAssetsPath + "/set";
#else
        setPath = Application.streamingAssetsPath + "/set";
#endif
        UnityWebRequest request = UnityWebRequest.Get(setPath);
        yield return request.SendWebRequest();
        try
        {
            Setting = JsonUtility.FromJson<GameSetting>(request.downloadHandler.text);
        }
        catch (Exception e)
        {
            // ignored
        }

        Setting ??= new GameSetting {RunSpeed = 10, RunTime = 60};
    }

    private void OnDestroy()
    {
        Instance = null;
    }


    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Level1");
        IncreaseGameTimes();
    }

    public bool HasNextLevel()
    {
        return currentLevel < 3;
    }

    public void StartNextLevel()
    {
        if (HasNextLevel())
        {
            currentLevel++;
            SceneManager.LoadSceneAsync("Level" + currentLevel);
            IncreaseGameTimes();
        }
    }

    public void IncreaseGameTimes()
    {
        int gameTimes = PlayerPrefs.GetInt("GameTimes", 0);
        PlayerPrefs.SetInt("GameTimes", gameTimes + 1);
        PlayerPrefs.Save();
    }

    public void IncreaseWinTimes()
    {
        int winTimes = PlayerPrefs.GetInt("WinTimes", 0);
        PlayerPrefs.SetInt("WinTimes", winTimes + 1);
        PlayerPrefs.Save();
    }

    public void Retry()
    {
        SceneManager.LoadSceneAsync("Level" + currentLevel);
        IncreaseGameTimes();
    }
}

public class GameSetting
{
    public int RunTime;
    public float RunSpeed;
}