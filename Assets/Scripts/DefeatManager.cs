using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using Cinemachine;
using UnityEngine.Events;

public class DefeatManager : MonoBehaviour
{
    private bool _defeat;

    [SerializeField] private UnityEvent OnDefeat;

    [SerializeField] private GameObject[] DefeatMenu;


    private void Start()
    {
        EventManager.OnDefeat += EventManager_OnDefeat;
    }

    private void EventManager_OnDefeat()
    {
        Defeat();
    }

    public void ChangeDefeatMenuState ()
    {
        DefeatMenu[MapManager.instance.GetMapID()].SetActive(true);
    }

    public void Defeat()
    {
        if (!_defeat)
        {
            OnDefeat?.Invoke();
            EventManager.FireEvent(EventManager.OnGameStateChanged);
            _defeat = !_defeat;
        }
    }
}
