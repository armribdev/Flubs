using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    private Camera cam;

    private GameManager gm;

    [SerializeField]
    private float zoomStep, minCamSize;
    private float maxCamSize;

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private Vector3 dragOrigin;

    private float targetCamSize;

    private void Awake()
    {
        
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        mapMinX = - gm.levelHeight / 2;
        mapMaxX =   gm.levelHeight / 2;

        mapMinY = - gm.levelWidth / 2;
        mapMaxY =   gm.levelWidth / 2;

    }

    void Start()
    {
        transform.position = new Vector3(0f, 0f, -10f);
        cam = GetComponent<Camera>();
        
        targetCamSize = cam.orthographicSize = gm.levelHeight / 2;
        maxCamSize = cam.orthographicSize;
    }

    void Update()
    {
        PanCamera();
        ZoomCamera();
    }

    private void PanCamera()
    {
        // if (cam.orthographicSize == maxCamSize) return;

        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            targetCamSize = cam.orthographicSize;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 diff = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            // cam.transform.position = ClampCamera(cam.transform.position + diff);
            cam.transform.position += diff;
        }
    }

    private void ZoomCamera()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            ZoomIn();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            ZoomOut();
        }

        if (Mathf.Abs(cam.orthographicSize - targetCamSize) > .001f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetCamSize, Time.deltaTime * 2.0f);
            //cam.transform.position = ClampCamera(cam.transform.position);
        }
        else
        {
            cam.orthographicSize = targetCamSize;
        }
    }

    public void ZoomIn()
    {
        targetCamSize = Mathf.Clamp(targetCamSize - zoomStep, minCamSize, maxCamSize);
    }

    public void ZoomOut()
    {
        targetCamSize = Mathf.Clamp(cam.orthographicSize + zoomStep, minCamSize, maxCamSize);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
