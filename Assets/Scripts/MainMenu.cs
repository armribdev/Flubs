using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Niveau1()
    {
		SceneManager.LoadScene(1);  // S'assurer que le niveau 1 est bien en position 1 dans le build
	}

	public void QuitGame()
    {
        Debug.Log("QUIT !");
        Application.Quit();
    }
}
