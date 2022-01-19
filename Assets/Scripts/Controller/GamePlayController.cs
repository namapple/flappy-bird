using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;
    [SerializeField]
    private Button instructionButton; // need UnityEngine.UI
    [SerializeField]
    private Text scoreText, endScoreText, bestScoreText;

    [SerializeField]
    private GameObject gameOverPanel, pausePanel;
    private void Awake()
    {
        MakeInstance();
        Time.timeScale = 0; // 0: freeze time, 1: continue
    }

    public void InstructionButton()
    {
        Time.timeScale = 1;
        //hide panel
        instructionButton.gameObject.SetActive(false); // button/text type: .gameOject
    }

    public void SetScore(int score)
    {
        scoreText.text = "" + score; // cast type, ex: score.ToString()
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // => Remember to init this instance in Awake function

    public void BirdDiedShowPanel(int score)
    {
        //show panel when bird died
        gameOverPanel.SetActive(true); // gameOverPanal is type of gameObject => no need '.gameOject'
        endScoreText.text = "" + score;
        if (score > GameManager.instance.GetHightScore())
        {
            GameManager.instance.SetHighScore(score);
        }
        bestScoreText.text = "" + GameManager.instance.GetHightScore();
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGameButton()
    {
        // Back to GamePlay
        //In case there're a lot of levels in games:
        // Use functions SceneManager.GetActiveScene();
        // to back to current level, player died at level 12, back to level 12
        SceneManager.LoadScene("GamePlay"); 

    }

    public void PauseButton()
    {
        Time.timeScale = 0; // freeze game when hit pause
        pausePanel.SetActive(true);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
