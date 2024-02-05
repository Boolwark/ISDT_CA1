using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AudioSettings : MonoBehaviour
    {
        public Slider musicSlider, sfxSlider;
        public void OnSFXSliderChanged()
        {
            AudioManager.Instance.SetSFXVolume(sfxSlider.value);
        }
        public void OnMusicSliderChanged()
        {
            AudioManager.Instance.SetMusicVolume(musicSlider.value);
        }
    }
}