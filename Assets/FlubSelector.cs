using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FlubSelector : MonoBehaviour
{
    private Camera cam;

    private Vector3 startPos;
    
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            startPos = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0)) {
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPos, cam.ScreenToWorldPoint(Input.mousePosition));
            foreach (Collider2D collider2D in collider2DArray) {
                Debug.Log(collider2D);
            }
        }
    }
}
