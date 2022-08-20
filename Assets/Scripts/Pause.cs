using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject Cam;
    public bool isPaused;
    public GameObject PauseScreen,DefeatScreen;

    private void OnMouseUp()
    {
        if (isPaused == false)
        {


            PauseScreen.SetActive(true);
            DefeatScreen.SetActive(false);
            Cam.GetComponent<Animator>().Play("DefeatAnim");
            isPaused = true;
        }
    }
    public void Continue2 ()
    {
        isPaused = false;
        PauseScreen.SetActive(false);
        DefeatScreen.SetActive(true);
    }
    public void Continue()
    {
        Debug.Log("Cont");
        Cam.GetComponent<Animator>().Play("StartAnim");
        Invoke("Continue2",1);
    }
    public void Reload()
    {
        Debug.Log("Rell");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
    public void Menu()
    {
        Debug.Log("Menu");
        SceneManager.LoadScene(0);
    }
}
