using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    public void BackHome()
    {
        SceneManager.LoadScene(0);
    }
}
