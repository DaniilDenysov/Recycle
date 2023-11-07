using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance { get; set; }

    [SerializeField] private bool _paused;
    public GameObject[] PauseMenu;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private GameObject pauseBtn;
    private GameObject camera;

    public static event EventHandler<bool> OnPaused;

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
        pauseBtn.SetActive(pauseBtn.active ? false : true);
    }

    public void DisableOrEnablePauseMenu()
    {
        PauseMenu[mapManager.GetMapID()].SetActive(PauseMenu[mapManager.GetMapID()].active ? false : true);
    }

    public void Pause ()
    {
        _paused = _paused ? false : true;
        OnPaused?.Invoke(this, _paused);
        FindObjectOfType<CinemachineVirtualCamera>().gameObject.GetComponent<Animator>().SetBool("Pause",_paused);
    }
}
