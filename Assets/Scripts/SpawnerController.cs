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
    
    void Start()
    {
        spawningDeltaTime = 2.0f;
        InvokeRepeating("SpawnFlub", 3.0f, spawningDeltaTime);
    }

    public void SpawnFlub()
    {
        if (stock > 0) {
            GameObject go = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, -1f), transform.rotation);
            if (orientation == FlubMovement.Direction.Left)
                go.GetComponent<FlubMovement>().Flip();
            go.GetComponent<Flub>().setType(type);
            stock--;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "portal.png", true);
    }
}
