using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
   
    [SerializeField] private float _score = 0, _level = 10,_increase = 1;
    [SerializeField] private TextMeshProUGUI Display;
    [SerializeField] private bool _protection;

    private Image _effectDisplay;


    private void Start()
    {
        EventManager.OnScoreChanges += EventManager_OnScoreChanges;
        EventManager.OnEffectStarted += EventManager_OnEffectStarted;
        EventManager.OnEffectStopped += EventManager_OnEffectStopped;
        _effectDisplay = GetComponent<Image>();
    }

    private void EventManager_OnEffectStarted(Effect obj)
    {
        if (obj.TryGetComponent<Protect>(out Protect protect))
        {
            _protection = true;
            return;
        }
        if (obj.TryGetComponent<DoubleScore>(out DoubleScore doubleScore))
        {
            _increase = (int)doubleScore.GetEffect();
            return;
        }
    }

    private void EventManager_OnEffectStopped(Effect obj)
    {
        if (obj.TryGetComponent<Protect>(out Protect protect))
        {
            _protection = false;
            return;
        }
        if (obj.TryGetComponent<DoubleScore>(out DoubleScore doubleScore))
        {
            _increase = 1;
            return;
        }
    }

    private void EventManager_OnScoreChanges(float obj)
    {
       AddScore(obj);
    }

    public void AddScore(float score)
    {
        if (score > 0)
        {
            SetScore(score);
        }
        else if (!_protection)
        {
            SetScore(score);
        }
    }
    public void SetScore (float score)
    {
        if (_score + (score * _increase) < 0)
        {
            EventManager.FireEvent(EventManager.OnDefeat);
            _score = 0;
            _effectDisplay.fillAmount = 0;
            Display.text = "";
            return;
        }
        if (_score + (score * _increase) > _level) _level *= 10;
        _score += (score * _increase);
        _effectDisplay.fillAmount = _score * (1f / (_level));
        Display.text = _score.ToString();
    }

    private void OnDestroy()
    {
        EventManager.OnScoreChanges -= EventManager_OnScoreChanges;
        EventManager.OnEffectStarted -= EventManager_OnEffectStarted;
        EventManager.OnEffectStopped -= EventManager_OnEffectStopped;
    }
}