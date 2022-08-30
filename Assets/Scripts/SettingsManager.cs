using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private string Music_PlayerPrefsID, Help_PlayerPrefsID;
    [SerializeField] private Toggle Music, Help;
    [SerializeField] private AudioMixer mixer;

    void Start()
    {
        if (PlayerPrefs.HasKey(Music_PlayerPrefsID))
        {
            if (PlayerPrefs.GetInt(Music_PlayerPrefsID) != 1)
            {
                mixer.SetFloat("Master", -10);
                Music.isOn = true;
            }
            else
            {
                mixer.SetFloat("Master", -80);
                Music.isOn = false;
            }
        }
        else Music.isOn = true;
        if (PlayerPrefs.HasKey(Help_PlayerPrefsID))
        {
            if (PlayerPrefs.GetInt(Help_PlayerPrefsID) != 1) Help.isOn = true;
            else Help.isOn = false;
        }
        else Help.isOn = true;
    }

    public void MusicStateChange (bool state)
    {
        if (state)
        {
            PlayerPrefs.SetInt(Music_PlayerPrefsID, 0);
            mixer.SetFloat("Master",-10);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt(Music_PlayerPrefsID, 1);
            mixer.SetFloat("Master", -80);
            PlayerPrefs.Save();
        }
    }

    public void HelpStateChange(bool state)
    {
        if (state) PlayerPrefs.SetInt(Help_PlayerPrefsID, 0);
        else PlayerPrefs.SetInt(Help_PlayerPrefsID, 1);
    }
}
