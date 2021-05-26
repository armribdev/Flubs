using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Flub))]
public class FlubMovement : NetworkBehaviour
{
    public enum Direction {Left , Right};
    
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float slopeCheckDistance;
    private Animator animator;

    private bool grounded;
    private float lastGroudedHeight;
    public float maxFallHeightWithoutDying;

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
        grounded = false;
        lastGroudedHeight = transform.position.y;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        flub = GetComponent<Flub>();
        animator = GetComponent<Animator>();

        rb.bodyType = isServer ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
    }

    void FixedUpdate()
    {
        SlopeCheck();
        SideCheck();
        FloorCheck();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environnement")) {
            float horizontalMovement = moveSpeed * Time.deltaTime;
            if (direction == Direction.Left) horizontalMovement *= -1;
            if (flub.powerUp == Flub.PowerUp.None)
                Move(horizontalMovement);
        }
    }

    private void Move(float horizontalMovement) {
        Vector3 target = new Vector2 (horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, target, ref velocity, .05f);
    }

    public void Flip()
    {
        direction = direction == Direction.Right ? Direction.Left : Direction.Right;
        Vector3 newLocalScale = transform.localScale;
        newLocalScale.x *= -1;
        transform.localScale= newLocalScale;
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

    private void SideCheck()
    {
        float x = direction == Direction.Right ? cc.bounds.max.x : cc.bounds.min.x;
        Vector2 dir = direction == Direction.Right ? Vector2.right : Vector2.left;
        Vector2 origin = new Vector2(x, cc.bounds.center.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, .05f,  groundLayerMask);
        Debug.DrawRay(origin, dir * .05f, Color.red);
        
        if (hit)
            Flip();
    }

    private void FloorCheck()
    {
        Vector2 origin = new Vector2(cc.bounds.center.x, cc.bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, .1f,  groundLayerMask);
        Debug.DrawRay(origin, Vector2.down * .1f, Color.red);
        
        if (!hit && grounded) {
            grounded = false;
            lastGroudedHeight = transform.position.y;
        }

        if (hit && !grounded) {
            grounded = true;
            float fallHeight = lastGroudedHeight - transform.position.y;
            if (fallHeight >= maxFallHeightWithoutDying) {
                flub.die();
            }
        }
    }
}
