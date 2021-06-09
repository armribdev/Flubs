using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Niveau1()
    {
		SceneManager.LoadScene(1);  // S'assurer que TutoStop est bien en position 1 dans le build
	}

    public void Niveau2()
    {
        SceneManager.LoadScene(2);  // S'assurer que Niveau1 est bien en position 1 dans le build
    }

    public void QuitGame()
    {
        Debug.Log("QUIT !");
        Application.Quit();
    }
}
