using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinChanger : MonoBehaviour
{

    public static BinChanger instance { get; set; }

    [SerializeField] private List <GameObject> Bin,bin_position;
    private int Copacity;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("Difficulty") && PlayerPrefs.GetInt("Difficulty") > 0) ChoseRandomly(PlayerPrefs.GetInt("Difficulty"));
        else ChoseRandomly(2);
    }

    public int GetCopacity() => Copacity;

    public void ChoseRandomly (int copacity)
    {
        Copacity = copacity;

        for (int i = 0;i<4;i++)
        {
            if (copacity > 0)
            {
                copacity--;
                int r = Random.Range(0, bin_position.Count-1);
               // Debug.Log("Bin position:" + r + " Bin:" + m);
                Bin[copacity].transform.position = bin_position[r].transform.position;
                bin_position.Remove(bin_position[r]);
            }
            else { Destroy(Bin[i]); }
         
        }
        
    }
}
