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
        if (!isPlaying(FindObjectOfType<CinemachineVirtualCamera>().gameObject.GetComponent<Animator>(), "DefeatAnim") && !isPlaying(FindObjectOfType<CinemachineVirtualCamera>().gameObject.GetComponent<Animator>(), "StartAnim"))
        {
            if (!Paused)
            {
                FindObjectOfType<CinemachineVirtualCamera>().gameObject.GetComponent<Animator>().Play("DefeatAnim");
                pauseBtn.SetActive(false);
                Paused = true;
            }
            else
            {
                FindObjectOfType<CinemachineVirtualCamera>().gameObject.GetComponent<Animator>().Play("StartAnim");
                pauseBtn.SetActive(true);
                Paused = false;
            }
            PauseMenu[mapManager.GetMapID()].SetActive(Paused);
        }
    }


}
