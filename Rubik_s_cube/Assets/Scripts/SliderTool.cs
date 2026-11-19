using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public class SliderTool : MonoBehaviour
{
    public UnityEvent ExecuteOnEnable;
    private void OnEnable()
    {
        ExecuteOnEnable.Invoke();
    }

    public void RoundValue(int digits)
    {
        GetComponent<Slider>().value = (float)Math.Round(GetComponent<Slider>().value, digits);
    }
    public void SetSliderText(TextMeshProUGUI text)
    {
        text.text = this.GetComponent<Slider>().value.ToString();
    }

}
