using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private float spawningDeltaTime;

    [SerializeField]
    private int stock;

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
            Instantiate(prefab, transform.position, transform.rotation);
            stock--;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "portal.png", true);
    }
}
