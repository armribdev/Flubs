using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    public void Restart()   // Bouton Restart
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
