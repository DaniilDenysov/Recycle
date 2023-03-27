using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;

public class DefeatManager : MonoBehaviour
{
    public static DefeatManager instance { get; set; }

    public bool Lost;
    [SerializeField] private GameObject[] DefeatMenu;

    private void Awake()
    {
        instance = this;
    }
        
    void Start()
    {
        StartCoroutine(ScoreCheck());
    }

    IEnumerator ScoreCheck()
    {
        yield return new WaitUntil(predicate: () => ScoreManager.instance.Score < 0);
        Defeat();
    }

    public void Defeat ()
    {
        Debug.Log("U lost!");
        Lost = true;
        ScoreManager.instance.SetScore(0);
        DefeatMenu[MapManager.instance.MapID].SetActive(true);
        FindObjectOfType<CinemachineVirtualCamera>().gameObject.GetComponent<Animator>().Play("DefeatAnim");
    }

}
