using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    bool opening, closing, opened, closed;
    public float turnSpeed = 12.0f;
    Bounds bounds;

    private float initialRotation;
    public float rotation = 80.0f;

    private Vector3 rotationPoint;


    void Start() {
        bounds = GetComponent<SpriteRenderer>().bounds;
        initialRotation = transform.rotation.eulerAngles.z;
        rotationPoint = transform.Find("RotationPoint").transform.position;
    }


    void Update()
    {
        if (opening && !opened) {
            transform.RotateAround(rotationPoint, Vector3.forward, turnSpeed * Time.deltaTime);
            if (transform.rotation.eulerAngles.z - initialRotation > rotation) opened = true;
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
