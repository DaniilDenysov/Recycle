using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlider : MonoBehaviour
{
    [SerializeField] private int Default;
    [SerializeField] private string PlayerPrefsID;
    [SerializeField] private Image output;
    [SerializeField] private Sprite [] sprite;



    void Start()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsID)) output.sprite = sprite[PlayerPrefs.GetInt(PlayerPrefsID)];
        else output.sprite = sprite[Default];

        
    }

    public void Button ()
    {
        for (int i = 0;i< sprite.Length; i++)
        {
            Debug.Log(i);
            if (output.sprite == sprite[i])
            {
                if (i+1 < sprite.Length)
                {
                    output.sprite = sprite[i+1];
                    PlayerPrefs.SetInt(PlayerPrefsID,i+1);
                    PlayerPrefs.Save();
                    break;
                }
                else
                {
                    output.sprite = sprite[0];
                    PlayerPrefs.SetInt(PlayerPrefsID, 0);
                    PlayerPrefs.Save();
                    break;
                }
            }
        }
    }

}
