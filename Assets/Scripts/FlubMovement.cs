using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class FlubMovement : MonoBehaviour
{
    enum Direction {Left , Right};
    
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float slopeCheckDistance;
    private Animator animator;


    [SerializeField]
    float maxFlipAngle;

    [SerializeField]
    private LayerMask groundLayerMask;
    
    private Direction direction;

    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private Vector3 velocity = Vector3.zero;

    private Flub flub;
    
    void Awake()
    {
        direction = Direction.Right;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        flub = GetComponent<Flub>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        SlopeCheck();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        float horizontalMovement = moveSpeed * Time.deltaTime;
        if (direction == Direction.Left) horizontalMovement *= -1;
        if (flub.powerUp == Flub.PowerUp.None)
            Move(horizontalMovement);
    }

    private void Move(float horizontalMovement) {
        Vector3 target = new Vector2 (horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, target, ref velocity, .05f);
    }

    private void Flip()
    {
        direction = direction == Direction.Right ? Direction.Left : Direction.Right;
        Vector3 newLocalScale = transform.localScale;
        newLocalScale.x *= -1;
        transform.localScale= newLocalScale;
        Debug.Log("Fliped !");
    }

    private void SlopeCheck()
    {
        float x = direction == Direction.Right ? cc.bounds.max.x : cc.bounds.min.x;
        Vector2 origin = new Vector2(x, cc.bounds.center.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, cc.bounds.extents.y + .01f, groundLayerMask);
        Debug.DrawRay(origin, Vector2.down * (cc.bounds.extents.y + .01f), Color.red);
        
        if (hit)
        {
            Debug.DrawRay(hit.point, hit.normal, Color.green);
            float angle = Vector2.SignedAngle(hit.normal, Vector2.up);
            if ((direction == Direction.Right && angle < -maxFlipAngle) || (direction == Direction.Left && angle > maxFlipAngle)) Flip();
        }
    }
}
