using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DialogueBehavior : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public float textSpeed;
    private int pointer;
    private string[] lines;

    public string[] Lines { get => lines; set => lines = value; }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == Lines[pointer])
                NextLine();
            else
            {
                StopAllCoroutines();
                textComponent.text = Lines[pointer];
            }
        }
    }

    void OnEnable()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        Player.Current.SetInputState(false);

        var dir = Mathf.Sign(Player.Current.transform.position.x - UIDialogModule.Current.CurrentInteractor.transform.position.x);
        Player.Current.Direction = dir;
    }

    void OnDisable()
    {
        pointer = 0;
        Player.Current.SetInputState(true);
    }

    public void StartDialogue()
    {
        pointer = 0;
        StartCoroutine("TypeLine");
    }

    IEnumerator TypeLine()
    {
        foreach (char c in Lines[pointer].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        pointer = Mathf.Min(++pointer, Lines.Length);
        textComponent.text = string.Empty;
        if (pointer < Lines.Length)
            StartCoroutine("TypeLine");
        else
        {
            UIDialogModule.Current.Hide();
        }
    }
    // void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    // {
    //     if (textComponent.text == Lines[pointer])
    //         NextLine();
    //     else {
    //         StopAllCoroutines();
    //         textComponent.text = Lines[pointer];
    //     }
    // }
}
