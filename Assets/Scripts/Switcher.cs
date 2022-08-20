using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;

public class Switcher : MonoBehaviour
{
    [SerializeField] MainMenuController MainCont;
    [SerializeField] RectTransform UIHandleRectTransform;
    [SerializeField] Color[] BackgroundActive, BackgroundDisable, HandleActive, HandleDisable;
    [SerializeField] Image BackgroundIMG, HandleIMG;
    Toggle toggle;
    Vector2 HandlePosition;
    [SerializeField] string Pref;
    [SerializeField] private UnityEvent Method;

    void Start ()
    {
        BackgroundIMG = UIHandleRectTransform.parent.GetComponent<Image>();
        HandleIMG = UIHandleRectTransform.GetComponent<Image>();
        toggle = GetComponent<Toggle>();
        BackgroundIMG.color = BackgroundActive[MainCont.Rand];
        HandleIMG.color = HandleActive[MainCont.Rand];
        if (PlayerPrefs.GetInt(Pref) == 0)
        {
            toggle.isOn = false;
           
        }
        else
        {
            toggle.isOn = true;

        }
        OnSwitch(toggle.isOn);
        //GetComponent<Toggle>().onValueChanged.AddListener(OnSwitch);

        // UIHandleRectTransform.anchoredPosition = UIHandleRectTransform.anchoredPosition * -1;
        //  UIHandleRectTransform.DOAnchorPos(toggle.isOn ? HandlePosition * -1 : HandlePosition, .4f).SetEase(Ease.InOutBack);
    }
    public void OnStart ()
    {
      
        
    }


    void OnSwitch (bool on)
    {
        if (PlayerPrefs.GetInt(Pref) == 0)
        {
            PlayerPrefs.SetInt(Pref, 1);
            UIHandleRectTransform.DOAnchorPos(on ? HandlePosition * -1 : HandlePosition, .4f).SetEase(Ease.InOutBack);
            UIHandleRectTransform.anchoredPosition = on ? HandlePosition * -1 : HandlePosition;
            BackgroundIMG.color = on ? BackgroundDisable[MainCont.Rand] : BackgroundActive[MainCont.Rand];
            HandleIMG.color = on ? HandleDisable[MainCont.Rand] : HandleActive[MainCont.Rand];
            Method.Invoke();
            PlayerPrefs.Save();
        }
        else 
        {
            PlayerPrefs.SetInt(Pref, 0);
            UIHandleRectTransform.DOAnchorPos(on ? HandlePosition : HandlePosition * -1, .4f).SetEase(Ease.InOutBack);
            UIHandleRectTransform.anchoredPosition = on ? HandlePosition : HandlePosition * -1;
            BackgroundIMG.color = on ? BackgroundDisable[MainCont.Rand] : BackgroundActive[MainCont.Rand];
            HandleIMG.color = on ? HandleDisable[MainCont.Rand] : HandleActive[MainCont.Rand];
            Method.Invoke();
            PlayerPrefs.Save();
        }
    }

    private void OnDestroy()
    {
        GetComponent<Toggle>().onValueChanged.RemoveListener(OnSwitch);
    }

    void Update()
    {
        
    }
}
