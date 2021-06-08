using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    bool opening, closing, opened, closed;
    float turnSpeed = 12.0f;
    Bounds bounds;

    void Start() {
        bounds = GetComponent<SpriteRenderer>().bounds;
    }


    void Update()
    {
        if (opening && !opened) {
            transform.RotateAround(new Vector3(bounds.min.x + bounds.size.y / 2, bounds.center.y, 0), Vector3.forward, turnSpeed * Time.deltaTime);
            if (transform.rotation.eulerAngles.z > 80.0f) opened = true;
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
        opened = false;
        closing = true;
    }
}
