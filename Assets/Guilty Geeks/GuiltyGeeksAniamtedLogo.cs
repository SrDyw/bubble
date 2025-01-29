using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GuiltyGeeksAniamtedLogo : MonoBehaviour
{
    public UnityEvent[] events;
    public GameObject transition;

    private void Start() {
        // PlayIntroAudio();
        Invoke(nameof(StartAnimation), 0.5f);
    }

    public void ExecuteAction(int index) {
        events[index]?.Invoke();
    }

    public void StartAnimation() {
        GetComponent<Animator>().enabled = true;
    }

    public void NextScene() {
        Invoke(nameof(NextSceneController), 1);
    }
    void NextSceneController() => Instantiate(transition);
    public void PlayIntroAudio() {
        var audio = FindObjectOfType<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("Sfx", 0.75f);
        audio.Play();
    }
}
