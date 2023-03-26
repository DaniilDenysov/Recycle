using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    private int Acceptable = 0;
    private Animator tick_anim;

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
        tick_anim = GetComponentInChildren<Animator>();
    }


    public void Check(int Garbage_ID)
    {
       if (Garbage_ID == Acceptable) tick_anim.Play("Accepted");
       else tick_anim.Play("NotAccepted");
    }
}
