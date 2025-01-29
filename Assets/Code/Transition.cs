using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Transition : MonoBehaviour
{
    public static Transition Current;
    [SerializeField] private float _autoTime = 1f;
    [SerializeField] private string sceneName;

    private void Awake() {
        Current = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if (_autoTime >= 0)
        {
            Invoke(nameof(GoTo), _autoTime);
        }
    }

    public void GoTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoTo()
    {
        GoTo(sceneName);
    }
}
