using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    bool opening, closing, opened, closed;
    public float turnSpeed = 12.0f;
    Bounds bounds;

    private float initialRotation;
    public float endAngle = 80.0f;
    public bool reversed;

    private Vector3 rotationPoint;


    void Start() {
        bounds = GetComponent<SpriteRenderer>().bounds;
        initialRotation = transform.rotation.eulerAngles.z;
        rotationPoint = transform.Find("RotationPoint").transform.position;
    }


    void Update()
    {
        Vector3 dir = (reversed) ? -Vector3.forward  : Vector3.forward;
        if (opening && !opened) {
            Debug.Log(transform.rotation.eulerAngles.z);
            transform.RotateAround(rotationPoint, dir, turnSpeed * Time.deltaTime);
            if (!reversed && transform.rotation.eulerAngles.z > endAngle) opened = true;
            if (reversed && transform.rotation.eulerAngles.z - 360 < endAngle) opened = true;
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
