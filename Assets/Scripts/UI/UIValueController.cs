using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIValueController : MonoBehaviour
{
    public Text text;
    public string title;
    public int max;
    public int min;
    public int value;
    public InputField inputValue;
    public Scrollbar scrollValue;

    public UnityEngine.Events.UnityAction<float> onValueChanged;

    void Start()
    {
        text.text = title;
        inputValue.text = value.ToString();
        scrollValue.value = (float)(value - min) / (max - min);
        inputValue.onValueChanged.AddListener(changeInput);
        scrollValue.onValueChanged.AddListener(changeScroll);
    }

    public void setValueListener(UnityEngine.Events.UnityAction<float> cal)
    {
        onValueChanged = cal;
    }

    void changeInput(string stringValue)
    {
        int val = int.Parse(stringValue);
        if (val < min) val = min;
        if (val > max) val = max;
        value = val;
        float scroll = value;
        scrollValue.value = (scroll - min) / (max - min);
        onValueChanged(value);
    }

    void changeScroll(float val)
    {
        val = val * (max - min) + min;
        value = (int)(val);
        inputValue.text = value.ToString();
        onValueChanged(value);
    }


}
