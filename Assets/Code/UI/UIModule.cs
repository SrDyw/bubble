using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModule : MonoBehaviour
{
    [SerializeField] private RectTransform _wrapper;
    private bool _isActive;

    public RectTransform Wrapper { get => _wrapper; set => _wrapper = value; }
    public bool IsActive { get => _isActive; set => _isActive = value; }

    public virtual void Show()
    {
        Wrapper.gameObject.SetActive(true);
        IsActive = true;
    }

    public virtual void Hide()
    {
        Wrapper.gameObject.SetActive(false);
        IsActive = false;
    }
}
