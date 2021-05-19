using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private float spawningDeltaTime;

    public int stock;
    public Flub.Type type;

    [SerializeField]
    private FlubMovement.Direction orientation;

    [SerializeField]
    private GameObject prefab;
    
    // Start is called before the first frame update
    void Start()
    {
        spawningDeltaTime = 2.0f;
        InvokeRepeating("SpawnFlub", .0f, spawningDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFlub()
    {
        if (stock > 0)
        {
            GameObject go = Instantiate(prefab, transform.position, transform.rotation);
            go.GetComponent<Flub>().type = type;
            if (orientation == FlubMovement.Direction.Left)
                go.GetComponent<FlubMovement>().Flip();
            stock--;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "portal.png", true);
    }
}
