using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Pour manipuler les objets text
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject AffichageFin;
    private Text endText;

    public bool levelOver;
    public int flubToInvoke;
    public int stoppedFlubs;
    public int deadFlubs;
    public int exitedFlubs;

    public float levelWidth, levelHeight;

    public SpawnerController[] spawnerControllers;

    private float endTime;

    private bool playing;

    void Start()
    {
        flubToInvoke = 0;
        levelOver = false;
        foreach(SpawnerController spawnerController in spawnerControllers) {
            flubToInvoke += spawnerController.stock;
        }

        endTime = 0;
        endText = AffichageFin.transform.Find("Panel").Find("Text").GetComponent<Text>();
        levelPlay(false);
    }

    void Update()
    {
        if (levelCompleted()) levelOver = true;
        if (levelOver)
        {
            if (endTime == 0)
            {
                Debug.Log("Terminé");
                endTime = Time.timeSinceLevelLoad;
                endText.text = "Fin de la partie !\nVous avez sauvés " + exitedFlubs.ToString() + " Flubs sur " + flubToInvoke.ToString();
                Instantiate(AffichageFin);
            }
            if (Time.timeSinceLevelLoad - endTime > 5)  // On attend 5s avant de revenir au menu
                SceneManager.LoadScene(0);
        }
    }

    void OnDrawGizmos()
{
    // Green
    Gizmos.color = new Color(0.0f, 1.0f, 0.0f);
    Gizmos.DrawWireCube(new Vector3(0f, 0f / 2, 0.01f), new Vector3(levelWidth, levelHeight, 0.01f));
}

    public bool levelCompleted() {
        return stoppedFlubs + deadFlubs + exitedFlubs == flubToInvoke;
    }

    public void tooglePlay() {
        levelPlay(!playing);
    }
    
    public void levelPlay(bool play) {
        playing = play;
        Time.timeScale = playing ? 1 : 0;
    }

    public void restart() {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
