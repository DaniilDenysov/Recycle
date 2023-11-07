using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using Cinemachine;

public class DefeatManager : MonoBehaviour
{
    public static DefeatManager instance { get; set; }

    public static event EventHandler<bool> OnDefeat;

    [SerializeField] private GameObject[] DefeatMenu;

    private void Awake()
    {
        instance = this;
    }


    public void ActivateDefeatMenu ()
    {
        DefeatMenu[MapManager.instance.GetMapID()].SetActive(true);
    }

    public void Defeat()
    {
        OnDefeat?.Invoke(this, true);
        FindObjectOfType<CinemachineVirtualCamera>().gameObject.GetComponent<Animator>().Play("DefeatAnim");
    }
}
