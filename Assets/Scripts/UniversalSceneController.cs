using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UniversalSceneController : MonoBehaviour
{
    [SerializeField] private Image Bar;
    [SerializeField] GameObject UI;
    public GameObject[] Buckets;
    [SerializeField] private Color[] TextColors; 
    [SerializeField] private Text Results, Record;
    //public float [] BucketPositionX;
    public int[] Score;
    public GameObject PauseBtn;
    public GameObject bottle;
    public GameObject Panel;
    public Text RabText;
    Animator anim;
    float Count;
    public int NowRab;
    public int needRab = 10;
    public int Lvl = 1;
    public GameObject Cam;
    public GameObject PanelCont;
    public bool isPaused;
    [SerializeField] private Spawn spawn;
    public GameObject []  PauseScreen, DefeatScreen;
    bool [] OtherChecked;
    int ReCounted = 0, SavedCount;
    // int[] PosX;
    Rigidbody2D rb;
    void Start()
    {
        anim = Cam.GetComponent<Animator>();
        Time.timeScale = 1;
        StartCoroutine(StartAn());
        RabText.text = NowRab + "";
        
        Record.text = "Record:" + PlayerPrefs.GetInt("Record");
    }


    public void ReCount ()
    {
        
        if (ReCounted < NowRab)
        {
            ReCounted += 1;
            Results.text = "Your score:" + ReCounted;

        }
        if (PlayerPrefs.GetInt("Record") < NowRab)
        {
            PlayerPrefs.SetInt("Record", NowRab);
            Record.text = "Record:" + NowRab;
        }
        else
        {
            Record.text = "Record:" + PlayerPrefs.GetInt("Record");
        }
    }

    public void Check ()
    {
       RabText.text = NowRab + "";
        if (NowRab < 0)
        {
            Defeat();
            SavedCount = NowRab;
           
            NowRab = 0;
            Bar.fillAmount = 0;
            Debug.Log(needRab);
        }
        if (NowRab > needRab)
        {
            Lvl += 1;
            Count += 1;
            Bar.fillAmount = 1 / needRab;
            needRab = (10 * Lvl);
        }
        if (PlayerPrefs.GetInt("Record") < NowRab)
        {
            PlayerPrefs.SetInt("Record", NowRab);
            Record.text = "Record:" + NowRab;
        }
        else
        {
            Record.text = "Record:" + PlayerPrefs.GetInt("Record");
        }
        PlayerPrefs.Save();
    }
    public void PointBar (int Sign) 
    {
        if (Sign > 0)
        {
            Bar.fillAmount = Bar.fillAmount + Sign * 1 / needRab;
        }
        else
        {
            if (Bar.fillAmount <= 0.2f && Count != 0)
            {
                Bar.fillAmount = 1 - 1/ needRab;
                Count -= 1;
            }else if (Bar.fillAmount <= 0.2f && Count != 0)
            {
                Bar.fillAmount = 0f;
                Check();
            }
            else Bar.fillAmount = Bar.fillAmount - Sign * (3*(1 / needRab));
        }
        Check();
    }
    public void Defeat ()
    {
        UI.SetActive(false);
        Record.enabled = true;
        RabText.text = NowRab + "";
        isPaused = true;
        PanelCont.SetActive(false);
        PauseScreen[spawn.Map].SetActive(false);
        DefeatScreen[spawn.Map].SetActive(true);
        Cam.GetComponent<Animator>().Play("DefeatAnim");
        InvokeRepeating("ReCount",0.02f, 0.02f);
    }
    void FixedUpdate()
    {
   

    }
    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;

    } 
    IEnumerator StartAn()
    {
        yield return new WaitForSeconds(1);
        Buckets[14].SetActive(true);

    }
    public void Reload ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
    public void OnChangeDifficulty ()
    {
        if (PlayerPrefs.GetInt("Difficulty") < 2) PlayerPrefs.SetInt("Difficulty", PlayerPrefs.GetInt("Difficulty") + 1);
        else PlayerPrefs.SetInt("Difficulty", 0);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("Difficulty"));
    }
    private void OnMouseUp()
    {
        if (isPaused == false)
        {
            PanelCont.SetActive(false);
            PauseScreen[spawn.Map].SetActive(true);
            DefeatScreen[spawn.Map].SetActive(false);
            Cam.GetComponent<Animator>().Play("DefeatAnim");
            isPaused = true;
        }
    }
    public void Continue2()
    {
       if (bottle) Destroy(bottle);
        isPaused = false;
        PauseScreen[spawn.Map].SetActive(false);
        DefeatScreen[spawn.Map].SetActive(true);

    }
    public void Continue()
    {
        PanelCont.SetActive(true);
        Debug.Log("Cont");
       
        Cam.GetComponent<Animator>().Play("StartAnim");
        Invoke("Continue2", 1);
    }
   

}
