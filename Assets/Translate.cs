using TMPro;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class Translate : MonoBehaviour
{
    [Header("Element index must be same as language ID!")]
    [SerializeField] private List<string> _lines;
    private TMP_Text _text;

    private void Start()
    {
        LanguageManager.OnLanguageChanged += LanguageManager_OnLanguageChanged;
        _text = GetComponent<TMP_Text>();
        _text.text = _lines[LanguageManager.Instance.GetCurrentLanguage()];
    }

    private void LanguageManager_OnLanguageChanged(object sender, int e)
    {
        _text.text = _lines[e];
    }
}
