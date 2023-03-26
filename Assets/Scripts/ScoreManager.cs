using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; set; }
    

    public float Score, Level, CurrentLevelScore,Increase = 1;
    [SerializeField] private TextMeshProUGUI Display;
    public bool Protection;



    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       // Garbage.OnScore += Garbage_OnScore;
        Effect.onEffectStarted += Effect_onEffectStarted;
        Effect.onEffectStopped += Effect_onEffectStopped;
    }

    private void Garbage_OnScore(object sender, int score)
    {
        
    }

    private void Effect_onEffectStopped(object sender, GameObject e)
    {
        if (e.TryGetComponent<Protect>(out Protect protect))
        {
            Protection = false;
            return;
        }
        if (e.TryGetComponent<DoubleScore>(out DoubleScore doubleScore))
        {
            Increase = 1;
            return;
        }
    }

    private void Effect_onEffectStarted(object sender, GameObject e)
    {
        if (e.TryGetComponent<Protect>(out Protect protect))
        {
            Protection = (bool)protect.GetEffect();
            return;
        }
        if (e.TryGetComponent<DoubleScore>(out DoubleScore doubleScore))
        {
            Increase = (int)doubleScore.GetEffect();
            return;
        }
    }


    public void AddScore(float score)
    {
        if (score > 0)
        {
            _Score(score);
        } else if (!Protection)
        {
            _Score(score);
        }
    }
    public void _Score (float score)
    {
        if (Score + (score * Increase) > Level * 10) Level += 1;
        Score += (score * Increase);
        GetComponent<Image>().fillAmount = Score * (1f / (Level * 10f));
        Display.text = Score.ToString();
    }
    public void SetScore(float score)
    {     
        Score = score;
        GetComponent<Image>().fillAmount = score * (1f / (Level * 10f));
        Display.text = Score.ToString(); 
    }

}
