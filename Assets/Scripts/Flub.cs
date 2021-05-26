using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEditor.Animations;

[RequireComponent(typeof(Animator))]
public class Flub : NetworkBehaviour
{
    public enum Type {Blue, Red};
    public enum PowerUp {None, Dig, Stop};

    public AnimatorController redAnimatorController, blueAnimatorController;

    public bool selected;
    private Animator animator;

    public Type type;
    
    public PowerUp powerUp;

    void Awake()
    {
        powerUp = PowerUp.None;
        selected = false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        //type = Type.Blue;
        //setType(type);
    }

    public void select(bool s) {
        selected = s;
        animator.SetBool("selected", s);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "NoRedDie" && type != Type.Red) die();
        if(collider.tag == "NoBlueDie" && type != Type.Blue) die();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Exit")) {
            exit();
        }
    }

    public void setPowerUp(PowerUp pu) {
        Debug.Log("3");
        powerUp = pu;
        animator.SetInteger("powerUp", (int)pu);

        switch(pu) {
            case PowerUp.None:
                gameObject.layer = LayerMask.NameToLayer("Controllable");
                break;
            case PowerUp.Stop:
                gameObject.layer = LayerMask.NameToLayer("Environnement");
                GameObject.Find("GameManager").GetComponent<GameManager>().stoppedFlubs ++;
                break;
            default:
                break;
        }
    }

    public void setType(Type t) {
        type = t;
        NetworkAnimator na = GetComponent<NetworkAnimator>();
        switch(t) {
            case Type.Red:
                na.animator.runtimeAnimatorController = redAnimatorController;
                break;
            case Type.Blue:
                na.animator.runtimeAnimatorController = blueAnimatorController;
                break;
            default:
                break;
        }
    }


    public void die() {
        Destroy(gameObject);
        GameObject.Find("GameManager").GetComponent<GameManager>().deadFlubs ++;
    }

    public void exit() {
        Destroy(gameObject);
        GameObject.Find("GameManager").GetComponent<GameManager>().exitedFlubs ++;
    }
}
