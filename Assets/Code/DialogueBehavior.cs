using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DialogueBehavior : MonoBehaviour, IPointerClickHandler
{
   
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int pointer;

    void OnEnable() {
        textComponent.text = string.Empty;
        StartDialogue();
        Player.Current.SetInputState(false);
    }

    void OnDisable() {
        pointer = 0;
        Player.Current.SetInputState(true);
    }

    void StartDialogue() {
        pointer = 0;
        StartCoroutine("TypeLine");
    }

    IEnumerator TypeLine() {
        foreach (char c in lines[pointer].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine() {
        pointer = Mathf.Min(++pointer, lines.Length);
        textComponent.text = string.Empty;
        if (pointer < lines.Length) 
            StartCoroutine("TypeLine");
        else {
            gameObject.SetActive(false);
        }
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (textComponent.text == lines[pointer])
            NextLine();
        else {
            StopAllCoroutines();
            textComponent.text = lines[pointer];
        }
    }
}
