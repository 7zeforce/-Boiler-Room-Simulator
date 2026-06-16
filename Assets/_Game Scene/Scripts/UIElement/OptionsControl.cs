using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsControl : MonoBehaviour
{
    [SerializeField] private Slider _sliderSensetivity;
    [SerializeField] private Slider _sliderSounds;
    [SerializeField] private Text _valueSensa;
    [SerializeField] private Text _valueVolume;

    private void Start()
    {
        GameCanvas.SynchronizationOptions += Synchronization;
        Synchronization();
        SliderSensetivityStart();
        SliderSoundsStart();
        TextSensUpdate(_sliderSensetivity.value);
        TextVolumeUpdate(_sliderSounds.value);
    }

    private void OnDestroy() => GameCanvas.SynchronizationOptions -= Synchronization;

    public void SliderSensetivity(float value)
    {
        Settings.Instance.Sensitivity = value;
        TextSensUpdate(value);
    }

    public void SliderSounds(float value) 
    {
        Settings.Instance.VolumeSounds = value;
        TextVolumeUpdate(value);
    }

    private void TextVolumeUpdate(float value) => _valueVolume.text = String.Format("{0:0}", value / 10);

    private void TextSensUpdate(float value) => _valueSensa.text = String.Format("{0:0}", value / 50f);

    private void Synchronization() => _sliderSensetivity.value = Settings.Instance.Sensitivity;

    private void SliderSensetivityStart()
    {
        _sliderSensetivity.value = Settings.Instance.Sensitivity;
        _sliderSensetivity.maxValue = 500;
        _sliderSensetivity.minValue = 500 / 10;
    }

    private void SliderSoundsStart()
    {
        _sliderSounds.value = Settings.Instance.VolumeSounds;
        _sliderSounds.maxValue = 100;
        _sliderSounds.minValue = 0;
    }
}