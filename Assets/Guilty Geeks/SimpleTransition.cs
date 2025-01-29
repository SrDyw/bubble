using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleTransition : MonoBehaviour
{
    public string sceneName;
    private void Start() {
        DontDestroyOnLoad(gameObject);
    }
    public void OnFadeIn()
    {
        AsyncOperation sceneLoader = null;
        if (sceneName == "")
        {
            sceneLoader = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else sceneLoader = SceneManager.LoadSceneAsync(sceneName);

        sceneLoader.completed += handle => Open();
    }

    public void Open()
    {
        GetComponent<Animator>().SetTrigger("open");
    }
    public void End() => Destroy(gameObject);
}
