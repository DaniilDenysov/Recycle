using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManage : MonoBehaviour
{
    public void Load(int Level)
    {
        SceneManager.LoadScene(Level);
    }
}
