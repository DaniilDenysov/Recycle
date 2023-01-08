using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EffectsManager : MonoBehaviour
{
    ScoreManager score;
    public float slowdownFactor = 0.02f; // чем меньше значаение тем медленее
    public float slowdownLengh = 50f; //продолжительность в секундах
    [SerializeField] private AudioMixer mixer;
    bool timeStop;

    void Start()
    {
        score = FindObjectOfType<ScoreManager>();
    }

 
    void Update()   
    {
      
        if (timeStop)
        {
            Time.timeScale += (1f / slowdownLengh) * Time.unscaledDeltaTime;
            //Debug.Log((1f / slowdownLengh) * Time.unscaledDeltaTime);
            //    Time.fixedDeltaTime += (0.01f / slowdownLengh) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            if (Time.timeScale >= 1)
            {
                timeStop = false;
                Debug.Log("Time:" + Time.timeScale);
                Time.timeScale = 1;
            }
            // Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0f, 0.01f);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            GetComponent<Animator>().Play("Tic-Tac");
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            GetComponent<Animator>().Play("ScoreIncrease");
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            GetComponent<Animator>().Play("Protection");
        }
    }

    public void ScoreIncrease ()
    {
        score.Increase = 2;
    }

    public void Protection_On ()
    {
        score.Protection = true;
    }

    public void Protection_Off() 
    {
        score.Protection = false;
    }

    public void ScoreDecrease ()
    {
        score.Increase = 1;
    }

    

    public void TimeBack ()
    {
        mixer.SetFloat("Pitch",1f);
        //Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<ChromaticAberration>().intensity.value = 0;

        timeStop = true;
    }

    public void TimeStop ()
    {
     //   Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<ChromaticAberration>().intensity.value = 0.3f;
        mixer.SetFloat("Pitch", 0.1f);
        SpawnGarbage spawn = FindObjectOfType<SpawnGarbage>();
       for (int i = 6;i>0;i--) spawn.InstantiateGarbage();
        Time.timeScale = slowdownFactor;    
        Time.fixedDeltaTime = Time.timeScale * .02f;
     
    }

    IEnumerator ScoreBuff (float time,int Increase)
    {
        score.Increase = Increase;
        yield return new WaitForSeconds(time);
        score.Increase = 1;
    }



}
