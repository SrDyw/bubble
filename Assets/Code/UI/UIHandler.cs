using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private UIModule[] _modules;

    public static UIHandler Current;

    private void Awake()
    {
        Current = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShowOrHide<UIInventoryModule>();
        }
    }

    public T GetModule<T>() where T : UIModule
    {
        var target = _modules.First(module => module.GetType() == typeof(T));

        if (target == null)
        {
            Debug.LogError($"The module {typeof(T)} doesnt exists in main module");
            return null;
        }
        return target as T;
    }

    public T Show<T>() where T : UIModule
    {
        var target = GetModule<T>();

        if (target != null)
        {
            target.Show();
            return target as T;
        }
        return null;

    }

    public T ShowOrHide<T>() where T : UIModule
    {
        var target = GetModule<T>();

        if (target != null)
        {
            if (!target.IsActive) target.Show();
            else target.Hide();
            return target as T;
        }
        return null;

    }
}
