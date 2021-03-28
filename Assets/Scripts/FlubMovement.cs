using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class FlubMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private bool facingRight;

    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    
    void Awake()
    {
        facingRight = true;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    /*
    void Update()
    {
        if (cc.)
        float horizontalMovement = moveSpeed * Time.deltaTime;
        if (!facingRight) horizontalMovement *= -1;
        Move(horizontalMovement);
    }*/

    void OnCollisionStay2D(Collision2D collision)
    {
        float horizontalMovement = moveSpeed * Time.deltaTime;
        if (!facingRight) horizontalMovement *= -1;
        Move(horizontalMovement);
    }

    private void Move(float horizontalMovement) {
        Vector3 target = new Vector2 (horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, target, ref velocity, .05f);
    }
}
