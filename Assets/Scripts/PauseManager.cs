using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{

    [SerializeField] private bool _paused;
    public GameObject[] PauseMenu;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private GameObject pauseBtn;
    [SerializeField] private UnityEvent _onPaused;
    private GameObject camera;

    void Start()
    {
        camera = Camera.main.gameObject;
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
        EventManager.FireEvent(EventManager.OnGameStateChanged);
        _onPaused?.Invoke();
        //OnPaused?.Invoke(this, _paused);
        //FindObjectOfType<CinemachineVirtualCamera>().gameObject.GetComponent<Animator>().SetBool("Pause",_paused);
    }
}
