using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagment : MonoBehaviour
{
    public void Load(int Level)
    {
        SceneManager.LoadScene(Level);
    }
}
