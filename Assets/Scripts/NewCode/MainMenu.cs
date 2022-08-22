using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider master_volume;
    [SerializeField] private Dropdown seasonSeed;
    [SerializeField] private Toggle help;
    [SerializeField] private GameObject[] Main, Settings, GameMode,BackGrounds;
    [SerializeField] private GameObject [] _Menu;
    int season;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Season") || PlayerPrefs.GetInt("Season") == 4) season = Random.Range(0, Main.Length - 1);
        else season = PlayerPrefs.GetInt("Season");
        ActivateMenu(Main);
        ActivateMenu(BackGrounds);
        ActivateMenu(GameMode);
        ActivateMenu(Settings);
        Debug.Log("Season:" + season);
        if (PlayerPrefs.HasKey("Music"))
        {
            if (PlayerPrefs.GetInt("Music") != 1) mixer.SetFloat("Master", -10);
            else mixer.SetFloat("Master", -80);
        }
        /* for (int i = 0; i < Settings.Length; i++)
         {

             if (i == season)
             {
                 Debug.Log(Settings[i].name);
                 seasonSeed = Settings[i].GetComponentInChildren<Dropdown>();
                 master_volume = Settings[i].GetComponentInChildren<Slider>();
                 help = Settings[i].GetComponentInChildren<Toggle>();
                 break;
             }
         }
         UpdateSettings();*/

    }

    public void Help (bool state)
    {
        if (state) PlayerPrefs.SetInt("Help", 0);
        else PlayerPrefs.SetInt("Help", 1);
    }

    public void UpdateSettings()
    {
        if (PlayerPrefs.HasKey("Master"))
        {
            Debug.Log(PlayerPrefs.GetFloat("Master"));
            master_volume.value = PlayerPrefs.GetFloat("Master");
            mixer.SetFloat("Master", Mathf.Log10(PlayerPrefs.GetFloat("Master")) * 20);
        }
        if (PlayerPrefs.HasKey("Help"))
        {
            if (PlayerPrefs.GetInt("Help") == 0) help.isOn = true;
            else help.isOn = false;
        }
    }

    public void ChangeSeason(int _season)
    {
        PlayerPrefs.SetInt("Season", _season);
        PlayerPrefs.Save();
    }

    public void ActivateMenu (GameObject [] menu)
    {
        for (int i = 0;i<menu.Length;i++)
        {
            if (i == season)
            {
                menu[i].SetActive(true);
                break;
            }
        }
    }

    public void SetVolume(float volume)
    {   
        //mixer.SetFloat("volume",volume);
        Debug.Log(Mathf.Log10(volume) * 20);
        mixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Master", volume);
        PlayerPrefs.Save();
    }
    public void Play (int difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
        SceneManager.LoadScene(1);
    }
    public void random ()
    {
        PlayerPrefs.SetInt("Difficulty",Random.Range(2,4));
        SceneManager.LoadScene(1);
    }
    public void OpenWindow(GameObject window)
    {
        for (int i = 0; i < _Menu.Length; i++)
        {
            if (window != _Menu[i])
            {
                _Menu[i].SetActive(false);
            }
            else
            {
                _Menu[i].SetActive(true);
            }
        }
    }
    public void Quit ()
    {
        Application.Quit();
    }

}
