using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlubSelector : MonoBehaviour
{
    [SerializeField] private GameObject selectionAreaPrefab;
    private Transform selectionAreaTransform;

    private Camera cam;
    private Vector3 startPos;
    private List<Flub> selectedFlubs;

    public int number;
    
    void Awake()
    {
        selectedFlubs = new List<Flub>();
        selectionAreaTransform = Instantiate(selectionAreaPrefab, transform.position, transform.rotation).transform;
    }


    void Start()
    {
        cam = GetComponentInChildren<Camera>();
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

            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            foreach (Collider2D collider2D in collider2DArray) {
                Flub flub = collider2D.GetComponent<Flub>();
                if (flub != null) {
                    flub.select(true);
                    selectedFlubs.Add(flub);
                    Debug.Log("Selection du flub " + flub);
                }
            }
        }
    }

    public void GivePowerUp(int powerUp) {
           
        GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach(GameObject go in gos)
        {
            if(go.layer == LayerMask.NameToLayer("Controllables"))
            {
                Flub flub = go.GetComponent<Flub>();
                if (flub.selected)
                    flub.setPowerUp((Flub.PowerUp)powerUp);
            }
        }
        
    }

}
