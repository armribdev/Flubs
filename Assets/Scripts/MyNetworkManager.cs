using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public NetworkConnection player1Conn, player2Conn;
    
    public GameObject player1go, player2go;

    public string[] levelScenes;


    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        Debug.Log("Client connecté");
        Debug.Log(NetworkServer.connections.Count);
        if (NetworkServer.connections.Count == 1) {
            Debug.Log("Le premier joueur est présent !");
            player1Conn = conn;
        }
        
        if (NetworkServer.connections.Count == 2) {
            Debug.Log("Les deux joueurs sont présents !");
            player2Conn = conn;
            ServerChangeScene("Assets/Scenes/" + levelScenes[0] + ".unity");
        }
    }


    public override void OnServerSceneChanged(string newSceneName) {
        Debug.Log("Changement de scène : " + newSceneName);
        if ("Assets/Scenes/" + levelScenes[0] + ".unity" == newSceneName) {
            Debug.Log("Chargement d'un niveau : spawn des joueurs");
            GameObject player1 = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(player1Conn, player1);
            player1.GetComponent<FlubSpawner>().numPlayer = 1;
            GameObject player2 = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(player2Conn, player2);
            player2.GetComponent<FlubSpawner>().numPlayer = 2;
        }
    }
}
