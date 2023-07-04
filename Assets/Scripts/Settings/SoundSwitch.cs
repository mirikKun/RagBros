using UnityEngine;
using UnityEngine.UI;

public class SoundSwitch : MonoBehaviour
{
    [SerializeField] private Button soundSwitchButton;

    private AudioSource _audioSource;
    private bool _soundOn = true;
    private Text _buttonText;

    private void Start()
    {
        SoundSwitchButtonSetup();
        _audioSource = GetComponent<AudioSource>();
        LoadSound();
    }

    /// <summary>
    /// Load data about sound state
    /// </summary>
    private void LoadSound()
    {
        //Load data about sound state
        if (PlayerPrefs.HasKey("SoundOn"))
        {
            _soundOn = GetSoundActive();
            if (!_soundOn)
            {
                SoundOff();
            }
        }
        else
        {
            PlayerPrefs.SetInt("SoundOn", 1);
        }

        SwitchButtonText();
    }

    private void SoundSwitchButtonSetup()
    {
        if (soundSwitchButton)
        {
            soundSwitchButton.onClick.AddListener(Switch);
            _buttonText = soundSwitchButton.GetComponentInChildren<Text>();
        }
    }

    private bool GetSoundActive()
    {
        return PlayerPrefs.GetInt("SoundOn") == 1;
    }

    /// <summary>
    /// Change state of sound
    /// </summary>
    public void Switch()
    {
        _soundOn = !_soundOn;
        if (_soundOn)
        {
            SoundOn();
        }
        else
        {
            SoundOff();
        }

        SwitchButtonText();
    }

    /// <summary>
    /// Change text on button of changing state of sound
    /// </summary>
    private void SwitchButtonText()
    {
        if (!_buttonText)
            return;
        _buttonText.text = _soundOn ? "ON" : "OFF";
    }

    private void SoundOn()
    {
        PlayerPrefs.SetInt("SoundOn", 1);
        _audioSource.Play();
    }

    private void SoundOff()
    {
        PlayerPrefs.SetInt("SoundOn", 0);
        _audioSource.Pause();
    }
}