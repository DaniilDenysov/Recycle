using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private float delayTime;
    public Sprite[] Backgraund;
    [SerializeField] private string[] Ru, En;
    [SerializeField] private Text[] toTranslate;
    [SerializeField] private Color [] LowGraundColors;
    [SerializeField] private SpriteRenderer[] HighGraund, LowGraund;
    [SerializeField] private GameObject [] Menus, UI, DifficultyUI, Settings,SettingsBack;
    [SerializeField] private Switcher [] switchAdvices;
    public int Rand;

    private void Start()
    {
        Rand = Random.Range(0, Backgraund.Length - 1);       
        LowGraund[0].color = LowGraundColors[Rand];
        UI[Rand].SetActive(true);
        foreach (SpriteRenderer SP in HighGraund)
        {
            SP.sprite = Backgraund[Rand];
        }
        //ChangerLanguage();
    }
    public void ChangerLanguage( )
    {
        if (PlayerPrefs.GetInt("Language") > 0)//en
        {
            for (int i = 0; i < toTranslate.Length - 1; i++)
            {
                toTranslate[i].text = "" + En[i];
            }
        }
        else if (PlayerPrefs.GetInt("Language") == 0)//ru
        {
            for (int i = 0; i < toTranslate.Length - 1; i++)
            {
                toTranslate[i].text = "" + Ru[i];
            }
        }
    }
    public void Play(int Difficulty)
    {
        PlayerPrefs.SetInt("Difficulty",Difficulty);
        SceneManager.LoadScene(1);
    }
    public void Switch (string Name)
    {
        if (PlayerPrefs.GetInt(Name) == 0) PlayerPrefs.SetInt(Name, 1);
        else if (PlayerPrefs.GetInt(Name) == 1) PlayerPrefs.SetInt(Name, 0);
        Debug.Log("Switched:" + Name);
    }
    public void settings ()
    {
        if (UI[Rand].active == true)
        {
            Settings[0].SetActive(true);
            SettingsBack[Rand].SetActive(true);
            UI[Rand].SetActive(false);
        }
        else
        {
            Settings[0].SetActive(false);
            SettingsBack[Rand].SetActive(false);
            UI[Rand].SetActive(true);
        }
    }
    public void StartOrQuitGame ()
    {
        if (UI[Rand].active == true)
        {
            DifficultyUI[Rand].SetActive(true);
            //if (Win == 0) DifficultyUI[Rand].SetActive(true);
           // else Settings[0].SetActive(true); SettingsBack[Rand].SetActive(true); 
            UI[Rand].SetActive(false);
            /*foreach (Switcher Sw in switchAdvices)
            {
                Sw.OnStart();
            }*/
        }
        else
        {
            DifficultyUI[Rand].SetActive(false);
            // if (Win == 0) DifficultyUI[Rand].SetActive(false);
            // else Settings[0].SetActive(false); SettingsBack[Rand].SetActive(false); 
            UI[Rand].SetActive(true);
        }
    }
    public void Delay (string void_Name)
    {
        Invoke(void_Name, delayTime);
    }
    public void Quit()
    {
        Application.Quit();
    }
    

}
