using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;

    public static event EventHandler<int> OnLanguageChanged;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeLanguage (int ID)
    {
        OnLanguageChanged?.Invoke(this,ID);
    }
}
