using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private const string HIGH_SCORE = "High Score"; // create a key name "High Score" in registry (Windows)
    private void Awake()
    {
        MakeSingleInstance();
        IsGameStaredForTheFirstTime();
    }
    void MakeSingleInstance()
    {
        // 'if' to destroy clone of instance, if not, the instance could be created everytime
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //When save somethting in the game, create this kind of function
    void IsGameStaredForTheFirstTime()
    {
        if (!PlayerPrefs.HasKey("IsGameStaredForTheFirstTime"))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
            PlayerPrefs.SetInt("IsGameStaredForTheFirstTime", 0);
        }
    }

    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }

    public int GetHightScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }
}
