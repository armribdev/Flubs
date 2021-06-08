using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FlubSpawner : NetworkBehaviour
{
    SpawnerController sc;
    GameManager gm;
    MyNetworkManager nm;
    NetworkConnection conn;
    public int numPlayer = 2;
    
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        nm = GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>();
        conn = GetComponent<NetworkIdentity>().connectionToClient;
        if (conn == nm.player1Conn) numPlayer = 1;
        if (conn == nm.player2Conn) numPlayer = 2;
        if (isLocalPlayer) {
            Debug.Log("Connexion : " + conn);
            sc = GameObject.Find("Spawner" + numPlayer).GetComponent<SpawnerController>();
            InvokeRepeating("SpawnFlub", .0f, 2.0f);
        }
    }

    
    public void SpawnFlub()
    {
        if (!isLocalPlayer)
            return;

        Debug.Log("Connexion client : " + conn);
        if (sc.stock > 0) {
            GameObject go = sc.SpawnFlub();
            NetworkServer.Spawn(go);
            go.GetComponent<NetworkIdentity>().AssignClientAuthority(conn);
        }
    }
    
}
