using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlubMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        Move(horizontalMovement);
    }


    private void Move(float horizontalMovement) {
        Vector3 target = new Vector2 (horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, target, ref velocity, .05f);
    }
}
