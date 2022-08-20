using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DefeatManager : MonoBehaviour
{
    public bool Lost;
    [SerializeField] private GameObject[] DefeatMenu;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private MapManager mapManager;

    void Start()
    {
        StartCoroutine(ScoreCheck());
    }

    IEnumerator ScoreCheck()
    {
        yield return new WaitUntil(predicate: () => scoreManager.Score < 0);
        Defeat();
    }

    public void Defeat ()
    {
        Debug.Log("U lost!");
        Lost = true;
        scoreManager.SetScore(0);
        DefeatMenu[mapManager.Map].SetActive(true);
        Camera.main.gameObject.GetComponent<Animator>().Play("DefeatAnim");
    }

}
