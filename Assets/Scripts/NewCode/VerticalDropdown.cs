using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VerticalDropdown : MonoBehaviour
{

    [SerializeField] private Text output;
    [SerializeField] private string[] Option;

    public void Swipe (int Direction)
    {
        if (Direction > 0)
        {
            for (int i = 0; i < Option.Length - 1; i++)
            {
                if (output.text == Option[i])
                {
                    if (i + 1 > Option.Length - 1)
                    {
                        output.text = Option[0];
                        break;
                    } else
                    {
                        output.text = Option[i];
                        break;
                    }
                }
            }
        }
        else
        {
            for (int i = Option.Length - 1; i > 0; i--)
            {
                if (output.text == Option[i])
                {
                    if (i - 1 < 0)
                    {
                        output.text = Option[Option.Length - 1];
                        break;
                    }
                    else
                    {
                        output.text = Option[i-1];
                        break;
                    }
                }
            }
        }
    }

}
