using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool levelOver;
    public int flubToInvoke;
    public int stoppedFlubs;
    public int deadFlubs;
    public int exitedFlubs;

    public SpawnerController[] spawnerControllers;

    void Start()
    {
        flubToInvoke = 0;
        levelOver = false;
        foreach(SpawnerController spawnerController in spawnerControllers) {
            flubToInvoke += spawnerController.stock;
        }
    }

    void Update()
    {
        if (levelCompleted())
            levelOver = true;
    }

    public bool levelCompleted() {
        return stoppedFlubs + deadFlubs + exitedFlubs == flubToInvoke;
    }

}
