using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameUI : MonoBehaviour
{
    public enum GameState { MainMenu, Paused, Playing, GameOver};
    public GameState CurrentState;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI CoinText;

    public Image redkeyui, bluekeyUI, yellowkeyUI;
    public GameObject allGameUI, mainMenuPanel, pauseMenuPanel, gameOverPanel, titleText;
    // Start is called before the first frame update
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            CheckGameState(GameState.MainMenu);

        }
        else
        {
            CheckGameState(GameState.Playing);
        }
        
    }
    public void CheckGameState(GameState newGamestate)
    {
        CurrentState= newGamestate;
        switch(CurrentState)
        {
            case GameState.MainMenu:
                MainMenuSetup();
                break;
             case GameState.Paused:
                GamePaused();
                Manager.gamePaused = true;
                Time.timeScale = 0f;
                break;
            case GameState.Playing:
                GameActive();
                Manager.gamePaused = false;
                Time.timeScale = 1f;
                break;
            case GameState.GameOver:
                GameOver();
                Manager.gamePaused = true;
                Time.timeScale = 0f;
                break;

        }
    }
    public void MainMenuSetup()
    {
        allGameUI.SetActive(false);
        mainMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        titleText.SetActive(true);
            

    }
    public void GameActive()
    {
        allGameUI.SetActive(true);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        titleText.SetActive(false);


    }
    public void GamePaused()
    {
        allGameUI.SetActive(true);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        titleText.SetActive(true);


    }
    public void GameOver()
    {
        allGameUI.SetActive(false);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        titleText.SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
    }
    void CheckInputs()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(CurrentState == GameState.Playing)
            {
                CheckGameState(GameState.Paused);
            }
            else if(CurrentState == GameState.Paused) 
            {
                CheckGameState(GameState.Playing);
            }
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level01");
        CheckGameState(GameState.Playing);
    }
    public void PauseGame()
    {
        CheckGameState(GameState.Paused);
    }
    public void ResumeGame()
    {
        CheckGameState(GameState.Playing);

    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        CheckGameState(GameState.MainMenu);
    }
    public void QuitGame()
    {
        Application.Quit();

    }

    public void UpdateCoins()
    {
        CoinText.text = Manager.coins.ToString();
    }
    public void UpdateLives() 
    {
        LifeText.text = Manager.Lives.ToString();
    }
    public void UPdateKey(Manager.DoorKeyColours keyColours)
    {
        switch (keyColours)
        {
            case Manager.DoorKeyColours.Red:
                redkeyui.GetComponent<Image>().color = Color.red;
                break;
            case Manager.DoorKeyColours.Blue:
                bluekeyUI.GetComponent<Image>().color = Color.blue;
                break;
            case Manager.DoorKeyColours.Yellow:
                yellowkeyUI.GetComponent<Image>().color = Color.yellow;
                break;
        }
    }

}
