using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DywFunctions;
public class DebugSystem : MonoBehaviour
{
    private Dictionary<string, TextMeshProUGUI> debugTexts;
    private VerticalLayoutGroup verticalGroup;
    private RectTransform verticalGroupRect;
    private static DebugSystem instance;
    public Dictionary<string, Timer> timerList;
    public float textsize = 15;
    public bool _debugging = true;

    public static Vector3 TextContainerLPostion
    {
        get => Instance.verticalGroup.transform.localPosition;
        set => Instance.verticalGroup.transform.localPosition = value;
    }

    public static float TextContainerYPosition
    {
        get => TextContainerLPostion.y;
        set => TextContainerLPostion = TextContainerLPostion.ModifyY(value);
    }
    public static DebugSystem Instance
    {
        get
        {
            InstanceSystemIfNotExists();
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        debugTexts = new Dictionary<string, TextMeshProUGUI>();
        verticalGroup = GetComponentInChildren<VerticalLayoutGroup>();
        verticalGroupRect = verticalGroup.GetComponent<RectTransform>();
    }

    private void Update()
    {
        foreach (var d in debugTexts)
        {
            d.Value.gameObject.SetActive(_debugging);
        }
    }

    /// <summary>
    /// Prints in screen a specified value to a debug text object. If the debug text object doesn't exist, it creates a new one.
    /// </summary>
    /// <param name="value">The value to be printed on screen.</param>
    /// <param name="tag">The tag of the debug text object. If the tag is not specified or is an empty string, "test1" will be used as the default tag.</param>
    public static GameObject Print(object value, string tag = "")
    {
        InstanceSystemIfNotExists();
        string parsedValue = value?.ToString();
        var verticalRect = instance.verticalGroupRect;
        tag = tag == "" ? "test1" : tag;

        GameObject textGO = null;

        if (instance.debugTexts.TryGetValue(tag, out TextMeshProUGUI debugText))
        {
            debugText.text = $"{tag}: {parsedValue}";
            textGO = debugText.gameObject;
        }
        else
        {
            textGO = new GameObject(tag);

            textGO.transform.SetParent(instance.verticalGroup.transform, false);
            var tmp = textGO.AddComponent<TextMeshProUGUI>();
            tmp.text = $"{tag}: {parsedValue}";
            tmp.fontSize = instance.textsize;
            var rect = tmp.GetComponent<RectTransform>();

            rect.sizeDelta = new Vector2(verticalRect.sizeDelta.x, 25);

            instance.debugTexts.Add(tag, tmp);
        }

        return textGO;
    }


    public static void TimerPrint(object value, string tag, float time)
    {
        var textGO = Print(value, tag);

        void DestroyText()
        {
            Destroy(textGO);
            instance.timerList.Remove(tag);
            instance.debugTexts.Remove(tag);
        }
        if (instance.timerList == null) instance.timerList = new();
        if (instance.timerList.TryGetValue(tag, out Timer timer))
        {
            Timer.CreateOrRestartTimer(ref timer, time, instance.gameObject, DestroyText);
        }
        else
        {
            var t = Timer.CreateNewTimer(time, instance.gameObject, DestroyText);
            instance.timerList.Add(tag, t);
        }
    }

    public static void PrintAtParent(object value, string tag, Transform parent, Vector2 offset = default)
    {
        if (!parent)
        {
            Debug.LogError("Cannot assign a debugger text to a null parent");
            return;
        }
        var canvas = parent.ExtractChild("LocalDebugCanvas");
        offset = offset == Vector2.zero ? Vector2.up * 2.5f : offset;

        if (!canvas)
        {
            canvas = Instantiate(Resources.Load<GameObject>("LocalDebugCanvas")).transform;
            canvas.name = "LocalDebugCanvas";
            canvas.SetParent(parent, false);
        }
        canvas.localPosition = offset;
        var verticalGroup = canvas.ExtractChild("VerticalGroup");
        var textInParent = verticalGroup.ExtractChild(tag)?.GetComponent<TextMeshProUGUI>();

        if (!textInParent)
        {
            var textGO = new GameObject(tag);
            textGO.transform.SetParent(verticalGroup.transform, false);

            textInParent = textGO.AddComponent<TextMeshProUGUI>();
            var rect = textInParent.GetComponent<RectTransform>();

            textInParent.enableAutoSizing = true;
            textInParent.fontSizeMin = 0;
            textInParent.fontSize = instance.textsize;
            rect.sizeDelta = new Vector2(verticalGroup.GetComponent<RectTransform>().sizeDelta.x, 1);
        }

        textInParent.text = tag + ": " + value?.ToString();
    }

    public static void InstanceSystemIfNotExists()
    {
        if (!instance)
        {
            var debug = Instantiate(Resources.Load<GameObject>("DebugSystem")).GetComponent<DebugSystem>();
            instance = debug;
        }
    }
}
