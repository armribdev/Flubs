using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ChargerNiveau(int num)
    {
        SceneManager.LoadScene(num);  // S'assurer que TutoStop est bien en position 1 dans le build
    }

    public void QuitGame()
    {
        Debug.Log("QUIT !");
        Application.Quit();
    }
}
