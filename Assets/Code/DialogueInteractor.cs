using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueInteractor : MonoBehaviour
{
    [SerializeField, TextArea(2, 5)] private string[] lines;
    [SerializeField] private bool _initOnStart;

    [SerializeField] private UnityEvent _OnDialogFinishes;
    [SerializeField] private UnityEvent _OnDialogStarts;


    private DialogueBehavior _dialogue;

    public UnityEvent OnDialogFinishes { get => _OnDialogFinishes; set => _OnDialogFinishes = value; }

    private void OnEnable()
    {
        if (_initOnStart)
        {
            _dialogue = FindObjectOfType<DialogueBehavior>();
            ShowDialog();
        }
    }

    private void Start()
    {
        _dialogue = FindObjectOfType<DialogueBehavior>();
    }

    public void ShowDialog()
    {
        _OnDialogStarts?.Invoke();
        UIDialogModule.Current.StartDialogue(lines, this);
    }
}
