using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float Score, Level, CurrentLevelScore,Increase = 1;
    [SerializeField] private TextMeshProUGUI Display;
    public bool Protection;

    public void AddScore(float score)
    {
        if (score > 0)
        {
            _Score(score);
        }else if (!Protection)
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
