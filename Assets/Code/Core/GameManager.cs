using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameState GameState;

    public static GameManager Current;

    public SharedDelegate OnGameOver;
    public SharedDelegate OnRestart;
    public ReleasePlantDelegate OnReleasePlant;
    public SharedDelegate OnQuestComplete;


    /// <summary>
    /// True - Playing | False - Paused
    /// </summary>
    public BooleanDelegate OnGameStateChanged;

    private void Awake()
    {
        if (GameManager.Current != null)
        {
            Destroy(gameObject);
        }
        Current = this;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (GameState == GameState.Paused)
        {
            Play();
        }
        else
        {
            Pause();
        }
    }

    public void Freeze() => Time.timeScale = 0;

    public void Unfreeze() => Time.timeScale = 1;

    public void Pause()
    {
        GameState = GameState.Paused;
        OnGameStateChanged?.Invoke(false);
    }

    public void Play()
    {
        GameState = GameState.Playing;
        OnGameStateChanged?.Invoke(true);
    }

    public void GameOver()
    {
        if (GameState != GameState.GameOver)
        {
            GameState = GameState.GameOver;
            OnGameOver?.Invoke();
        }
    }

    public void RestartGame()
    {
        OnRestart?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
