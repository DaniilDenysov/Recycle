using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool Paused;
    public GameObject[] PauseMenu;
    MapManager mapManager;

    void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
    }

    private void OnMouseDown()
    {
        Pause();
    }
    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }
    public void Pause ()
    {
        if (!isPlaying(Camera.main.gameObject.GetComponent<Animator>(), "DefeatAnim") && !isPlaying(Camera.main.gameObject.GetComponent<Animator>(), "StartAnim"))
        {
            if (!Paused)
            {
                Camera.main.gameObject.GetComponent<Animator>().Play("DefeatAnim");
                Paused = true;
            }
            else
            {
                Camera.main.gameObject.GetComponent<Animator>().Play("StartAnim");
                Paused = false;
            }
            PauseMenu[mapManager.Map].SetActive(Paused);
        }
    }


}
