using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class VerticalDropdown : MonoBehaviour
{
    [SerializeField] private int Default;
    [SerializeField] private string PlayerPrefsName;
    [SerializeField] private TMP_Text output;
    [SerializeField] private string[] Option;

    private void Start()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsName)) output.text = Option[PlayerPrefs.GetInt(PlayerPrefsName)];
        else output.text = Option[Default];
    }

    public void Swipe (int Direction)
    {
        Debug.Log("Pressed!");
        if (Direction > 0)
        {
            for (int i = 0; i <= Option.Length - 1; i++)
            {
                if (Option[i] == output.text.ToString())
                {
                    if (i + 1 > Option.Length - 1)
                    {
                        output.text = Option[0];
                        Save(0);
                        break;
                    }
                    else
                    {
                        output.text = Option[i + 1];
                        Save(i + 1);
                        break;
                    }
                }
            }
        }
        else
        {
            for (int i = Option.Length - 1; i >= 0; i--)
            {
                if (Option[i] == output.text.ToString())
                {
                    if (i - 1 < 0)
                    {
                        output.text = Option[Option.Length - 1];
                        Save(Option.Length - 1);
                        break;
                    }
                    else
                    {
                        output.text = Option[i - 1];
                        Save(i - 1);
                        break;
                    }
                }
            }
        }
        /*if (Direction > 0)
        {
            for (int i = 0; i <= Option.Length - 1; i++)
            {
                Debug.Log("i:" + i);
                if (i == count)
                {
                    if (i + 1 > Option.Length - 1)
                    {
                        Debug.Log(0);
                        count = 0; 
                        output.text = Option[0];
                        Save(0);
                        break;
                    }
                    else
                    {
                        Debug.Log(i + 1);
                        count = i + 1;
                        output.text = Option[i + 1];
                        Save(i + 1);
                        break;
                    }
                }
            }
        }
        else
        {
            for (int i = Option.Length - 1; i>=0; i--)
            {
                Debug.Log("i:" + i);
                if (i == count)
                {
                    if (i - 1 < 0)
                    {
                        Debug.Log(Option.Length - 1);
                        count = Option.Length - 1;
                        output.text = Option[Option.Length - 1];
                        Save(Option.Length - 1);
                        break;
                    }
                    else
                    {
                        Debug.Log(i - 1);
                        count = i - 1;
                        output.text = Option[i - 1];
                        Save(i - 1);
                        break;
                    }
                }
            }
        }*/
    }
    public void Save (int count)
    {
        PlayerPrefs.SetInt(PlayerPrefsName, count);
        PlayerPrefs.Save();
    }

}
