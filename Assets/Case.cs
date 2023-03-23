using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] private ParticleSystem confety,pieces,trophey;
    [SerializeField] private Animator trophey_animator,case_animator;

    private enum State
    {
        Open_1,
        Open_2,
        Open_3,
        Open_finish,
        Take,
        Restart
    }

    private State state;


    public void PlayConfety ()
    {
        confety.Play();
        pieces.Play();
    }

    public void openCase ()
    {
        switch (state)
        {
            case State.Open_1:
                state = State.Open_2;
               case_animator.SetBool("Open", true);
                break;
            case State.Open_2:
                state = State.Open_3;
                case_animator.SetTrigger("Next");
                break;
            case State.Open_3:
                state = State.Open_finish;
                case_animator.SetTrigger("Next");
                break;
            case State.Open_finish:
                state = State.Take;
                PlayTrophey("Open");
                case_animator.SetTrigger("Next");
                break;
            case State.Take:
                state = State.Restart;
                takeTrophey();
                case_animator.SetTrigger("Take");
                case_animator.SetBool("Open", false);
                break;
            case State.Restart:
                state = State.Open_1;
    
                break;
        }
    }

    public void takeTrophey ()
    {
        trophey_animator.SetTrigger("Take");
    }

    public void PlayTrophey (string name)
    {
        trophey_animator.SetTrigger(name);
    }
}
