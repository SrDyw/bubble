using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDialogModule : UIModule
{
    [SerializeField] private GameObject dialog;
    [SerializeField] private float maxDistance;
    [SerializeField] private Transform characterPos;
    [SerializeField] private DialogueBehavior _dialogue;

    private DialogueInteractor _currentInteractor;

    public static UIDialogModule Current;

    public DialogueInteractor CurrentInteractor { get => _currentInteractor; set => _currentInteractor = value; }

    private void Awake()
    {
        Current = this;
    }

    public override void Hide()
    {
        base.Hide();
        CurrentInteractor?.OnDialogFinishes?.Invoke();
        CurrentInteractor = null;
    }

    public void StartDialogue(string[] lines, DialogueInteractor interactor)
    {
        _dialogue.Lines = lines;
        CurrentInteractor = interactor;
        Show();
    }
}
