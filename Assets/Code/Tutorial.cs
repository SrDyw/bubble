using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private DialogueInteractor[] _dialogs;

    [SerializeField] private GameObject[] _hands;

    private int _currentIndex = 0;

    private int _releasedPlants = 0;

    private void Start()
    {
        StartCoroutine(StartTutorialProcess());
        Inventory.Current.OnItemAdd += OnItemAdd;
        Inventory.Current.OnItemUse += OnItemUse;
        GameManager.Current.OnReleasePlant += OnReleasePlant;

    }

    private IEnumerator StartTutorialProcess()
    {
        yield return new WaitForSeconds(3f);
        StartTutorial();
    }

    private void OnDisable()
    {
        Inventory.Current.OnItemAdd -= OnItemAdd;
        Inventory.Current.OnItemUse -= OnItemUse;
        GameManager.Current.OnReleasePlant -= OnReleasePlant;
    }

    public void OnReleasePlant(Plant plant)
    {
        _releasedPlants++;
        if (_currentIndex == 2)
        {
            NextTutorial();
        }

        if (_releasedPlants >= 3)
        {
            NextTutorial();
        }
    }

    public void OnItemAdd(Item item)
    {
        _hands[0].gameObject.SetActive(false);
        if (_currentIndex == 1)
        {
            _hands[1].gameObject.SetActive(true);
        }
    }

    public void OnItemUse(Item item)
    {
        if (_currentIndex != 1) return;
        if (Inventory.Current.Items.Where(x => x is Bubble).Count() < 2)
        {
            NextTutorial();
            _hands[1].gameObject.SetActive(false);
        }
        else
        {
            SkipTutorial();
            _hands[1].gameObject.SetActive(false);
        }
    }

    public GameObject NextTutorial()
    {
        _dialogs[_currentIndex++].gameObject.SetActive(true);
        return _dialogs[_currentIndex - 1].gameObject;
    }

    public void SkipTutorial()
    {
        _currentIndex++;
    }



    public void StartTutorial()
    {
        NextTutorial();
    }
}
