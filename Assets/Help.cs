using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Help : MonoBehaviour
{
    private int Acceptable = 0;

    [SerializeField] private UnityEvent OnCorrect, OnIncorrect;

    void Start()
    {
        if (PlayerPrefs.HasKey("Help"))
        {
            if (PlayerPrefs.GetInt("Help") != 0)
            {
                Destroy(this);
            }
        }
        Acceptable = gameObject.layer;
    }


    public void Check(int Garbage_ID)
    {
       if (Garbage_ID == Acceptable) OnCorrect?.Invoke();
       else OnIncorrect?.Invoke();
    }
}
