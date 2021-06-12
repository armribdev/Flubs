using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ChargerNiveau(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT !");
        Application.Quit();
    }
}
