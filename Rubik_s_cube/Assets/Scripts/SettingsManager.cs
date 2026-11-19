using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public SO_Settings setting;

    public void SetRotation(float value)
    {
        setting.rotationSpeed = value;
    }

    public void SetRotationValue(Slider rotationSpeedSlider)
    {
        rotationSpeedSlider.value = setting.rotationSpeed;
    }
}
