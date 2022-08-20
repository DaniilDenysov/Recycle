using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float Score, Level, CurrentLevelScore;
    [SerializeField] private Text Display;  

    public void AddScore(float score)
    {
        if (Score + score > Level * 10) Level += 1;
        Score += score;
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
