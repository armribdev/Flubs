using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class FlubSelector : MonoBehaviour
{
    [SerializeField] private Transform selectionAreaTransform;

    private Camera cam;
    private Vector3 startPos;
    private List<Flub> selectedFlubs;
    
    void Awake()
    {
        selectedFlubs = new List<Flub>();
        selectionAreaTransform.gameObject.SetActive(false);
    }


    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            
            if(EventSystem.current.IsPointerOverGameObject())
                return;

            startPos = cam.ScreenToWorldPoint(Input.mousePosition);
            selectionAreaTransform.gameObject.SetActive(true);
        }

        if (Input.GetMouseButton(0)) {

            if(EventSystem.current.IsPointerOverGameObject())
                return;

            Vector3 currentPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(startPos.x, currentPos.x),
                Mathf.Min(startPos.y, currentPos.y),
                -1
            );
            Vector3 upperRight = new Vector3(
                Mathf.Max(startPos.x, currentPos.x),
                Mathf.Max(startPos.y, currentPos.y),
                -1
            );

            selectionAreaTransform.position = lowerLeft;
            selectionAreaTransform.localScale = upperRight - lowerLeft;
        }

        if (Input.GetMouseButtonUp(0)) {
            
            selectionAreaTransform.gameObject.SetActive(false);

            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPos, cam.ScreenToWorldPoint(Input.mousePosition));

            foreach(Flub flub in selectedFlubs) {
                flub.select(false);
            }
            selectedFlubs.Clear();

            foreach (Collider2D collider2D in collider2DArray) {
                Flub flub = collider2D.GetComponent<Flub>();
                if (flub != null) {
                    flub.select(true);
                    selectedFlubs.Add(flub);
                    Debug.Log(flub);
                }
            }
        }
    }

    public void givePowerUp(int powerUp) {
        foreach(Flub flub in selectedFlubs) {
            flub.setPowerUp((Flub.PowerUp)powerUp);
        }
    }

}
