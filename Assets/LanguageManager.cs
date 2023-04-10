using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;
    private int _currentLanguage;
    public static event EventHandler<int> OnLanguageChanged;

    private void Awake()
    {
        Instance = this;
    }

    public int GetCurrentLanguage() => _currentLanguage;

    public void ChangeLanguage (int ID)
    {
        OnLanguageChanged?.Invoke(this,ID);
        _currentLanguage = ID;
    }
}
