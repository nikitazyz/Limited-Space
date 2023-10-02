using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class SoundSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audio;
        [SerializeField] private Slider _slider;

        private void Start()
        {
            _audio.GetFloat("Volume", out float value);
            _slider.value = Mathf.Pow(10, value / 20);
        }

        private void Update()
        {
            if (_slider.value == 0)
            {
                _audio.SetFloat("Volume", -80);
                return;
            }
            _audio.SetFloat("Volume", 20 * Mathf.Log10(_slider.value));
        }
    }
}
