using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _targetScene = "SampleScene";
    [SerializeField] private GameObject _creditsWrapper;
    public void StartGame()
    {
        SceneManager.LoadScene(_targetScene);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            _creditsWrapper.gameObject.SetActive(false);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Credit()
    {
        _creditsWrapper.gameObject.SetActive(true);
    }
}
