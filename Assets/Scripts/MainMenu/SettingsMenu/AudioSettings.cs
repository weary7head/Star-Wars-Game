using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _generalSlider;
    [SerializeField] private Slider _effectsSlider;
    [SerializeField] private Slider _musicSlider;

    private void Start()
    {
        float temporaryValue;
        _audioMixer.GetFloat("General", out temporaryValue);
        _generalSlider.value = temporaryValue;
        _audioMixer.GetFloat("Effects", out temporaryValue);
        _effectsSlider.value = temporaryValue;
        _audioMixer.GetFloat("Music", out temporaryValue);
        _musicSlider.value = temporaryValue;
    }

    public void SetGeneralVolume(float volume)
    {
        _audioMixer.SetFloat("General", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        _audioMixer.SetFloat("Effects", volume);
    }

    public void SetMusicVolume(float volume)
    {
        _audioMixer.SetFloat("Music", volume);
    }
}
