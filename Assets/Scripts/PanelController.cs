using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PanelController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject Cameramain;
    public GameObject Trash;
    public Spawn spawn;
    [SerializeField] private Vector2 State; 

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Trash && Trash.GetComponent<TrashController>().isTaken)
        {
            Trash.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Trash && Trash.GetComponent<TrashController>().isTaken && Input.GetButtonUp("Fire1"))
        {
            Trash.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            Trash = null;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (Trash && Trash.GetComponent<TrashController>().isTaken)
        {          
            Trash.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Trash.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            Trash.transform.position = new Vector3(Trash.transform.position.x, Trash.transform.position.y, 0);
        }
    }
}
