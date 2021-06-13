using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    bool opening, closing, opened, closed;
    public float turnSpeed = 12.0f;
    Bounds bounds;

    private float initialRotation, targetRotation;
    public float flipAngle;

    private Vector3 rotationPoint;


    void Start() {
        bounds = GetComponent<SpriteRenderer>().bounds;
        initialRotation = transform.rotation.eulerAngles.z;
        targetRotation = initialRotation + flipAngle;
        if (targetRotation < 0) targetRotation += 360;

        rotationPoint = transform.Find("RotationPoint").transform.position;
    }


    void Update()
    {
        if (opening && !opened) {
            Debug.Log(transform.rotation.eulerAngles.z);
            if (flipAngle > 0) {
                transform.RotateAround(rotationPoint, Vector3.forward, turnSpeed * Time.deltaTime);
                if (transform.rotation.eulerAngles.z > targetRotation) opened = true;
            }
            else {
                transform.RotateAround(rotationPoint, -Vector3.forward, turnSpeed * Time.deltaTime);
                if (transform.rotation.eulerAngles.z < targetRotation) opened = true;
            }
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
