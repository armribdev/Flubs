using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlubMovement : MonoBehaviour
{
    enum Direction {Left , Right};
    
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float maxClimbing;
    
    private Direction direction;

    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private Vector3 velocity = Vector3.zero;
    
    void Awake()
    {
        direction = Direction.Right;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        float horizontalMovement = moveSpeed * Time.deltaTime;
        if (direction == Direction.Left) horizontalMovement *= -1;
        Move(horizontalMovement);
    }

    private void Move(float horizontalMovement) {
        Vector3 target = new Vector2 (horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, target, ref velocity, .05f);
    }

    private void Flip()
    {
        if (direction == Direction.Left) direction = Direction.Right;
        else if (direction == Direction.Right) direction = Direction.Left;
    }
}
