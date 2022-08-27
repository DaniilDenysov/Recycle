using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinChanger : MonoBehaviour
{
    public List <GameObject> Bin,bin_position;
    public int Copacity;
    // public Vector3[] Origin = new Vector3[4];
    int Rand, RandPos;
        //4 4
    void Start()
    {
        if (PlayerPrefs.HasKey("Difficulty") && PlayerPrefs.GetInt("Difficulty") > 0) ChoseRandomly(PlayerPrefs.GetInt("Difficulty"));
        else ChoseRandomly(2);
    }

    public void ChoseRandomly (int m)
    {
        Copacity = m;

        for (int i = 0;i<4;i++)
        {
            if (m > 0)
            {
                m--;
                int r = Random.Range(0, bin_position.Count-1);
                Debug.Log("Bin position:" + r + " Bin:" + m);
                Bin[m].transform.position = bin_position[r].transform.position;
                bin_position.Remove(bin_position[r]);
            }
            else { Destroy(Bin[i]); }
         
        }
        
    }
}
