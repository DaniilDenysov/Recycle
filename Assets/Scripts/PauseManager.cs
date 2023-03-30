using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance { get; set; }

    public bool Paused;
    public GameObject[] PauseMenu;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private GameObject pauseBtn;
    private GameObject camera;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        camera = Camera.main.gameObject;
    }

    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    public void DisableOrEnablePauseButton ()
    {
        Paused = Paused ? false : true;
        pauseBtn.SetActive(pauseBtn.active ? false : true);
    }

    public void DisableOrEnablePauseMenu()
    {
        PauseMenu[mapManager.GetMapID()].SetActive(PauseMenu[mapManager.GetMapID()].active ? false : true);
    }

    public void Pause ()
    {
        FindObjectOfType<CinemachineVirtualCamera>().gameObject.GetComponent<Animator>().SetBool("Pause",Paused ? false : true);
    }


}
