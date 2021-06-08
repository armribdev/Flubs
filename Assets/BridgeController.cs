using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    bool opening, closing, opened, closed;

    void Update()
    {
        if (opening && !opened) {
            // On tourne
            // si position -> opened
        }

        else if (closing && !closed) {
            // On tourne
            // si position -> closed
        }
    }

    public void open() {
        Debug.Log("Ouverture du pont " + transform.name);
        closed = false;
        opening = true;
    }

    public void close() {
        Debug.Log("Fermeture du pont " + transform.name);
        open = false;
        closing = true;
    }
}
