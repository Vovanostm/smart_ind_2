using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMineController : MonoBehaviour
{
    public Image img;

    public UIValueController powerController;
    public RectTransform percentImage;

    public Text capacityText;
    public Text prodSpeedText;

    int _maxCapacity;
    int _currentCapacity;

    public float prodPercent
    {
        set
        {
            float width = value * 160;
            percentImage.sizeDelta = new Vector2(width, 0.5f);
        }
    }

    public float prodPower
    {
        set
        {

        }
    }
    public int prodCurrentCapacity
    {
        get
        {
            return _currentCapacity;
        }
        set
        {
            _currentCapacity = value;
            capacityText.text = _currentCapacity.ToString() + " / " + _maxCapacity;
        }
    }
    public int prodMaxCapacity
    {
        get
        {
            return _maxCapacity;
        }
        set
        {
            _maxCapacity = value;
            capacityText.text = _currentCapacity.ToString() + " / " + _maxCapacity;
        }
    }
    public float prodCurrentSpeed
    {
        set
        {
            prodSpeedText.text = value.ToString() + " / min";
        }
    }
    private void Start()
    {
    }
    public void setValueListener(UnityEngine.Events.UnityAction<float> cal)
    {
        powerController.setValueListener(cal);
    }
}
