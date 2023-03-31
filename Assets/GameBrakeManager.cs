using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameBrakeManager : MonoBehaviour
{
    public static GameBrakeManager Instance;

    public static event EventHandler<bool> OnBrake;

    private bool _status = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PauseManager.OnPaused += PauseManager_OnPaused;
        DefeatManager.OnDefeat += DefeatManager_OnDefeat;
        ScoreManager.OnDefeat += ScoreManager_OnDefeat;
    }

    private void ScoreManager_OnDefeat(object sender, EventArgs e)
    {
        OnBrake?.Invoke(this, true);
    }

    private void DefeatManager_OnDefeat(object sender, bool e)
    {
        if (!_status) OnBrake?.Invoke(this, e);
    }

    private void PauseManager_OnPaused(object sender, bool e)
    {
        OnBrake?.Invoke(this,e);
        _status = e;
    }

    public bool GetCurrentStatus() => _status;
}
