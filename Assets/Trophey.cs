using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Trophey : MonoBehaviour
{
    [SerializeField] private int ID;
    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        FindObjectOfType<DataManager>().saveData(ID);
    }
}
