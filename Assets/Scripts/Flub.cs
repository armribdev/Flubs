using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Flub : MonoBehaviour
{
    public enum Type {Blue, Red};
    public enum PowerUp {None, Dig, Stop};

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
    }

    public void select(bool s) {
        selected = s;
        animator.SetBool("selected", s);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Exit")) {
            exit();
        }
    }

    public void setPowerUp(PowerUp pu) {
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

    public void die() {
        Destroy(gameObject);
        GameObject.Find("GameManager").GetComponent<GameManager>().deadFlubs ++;
    }

    public void exit() {
        Destroy(gameObject);
        GameObject.Find("GameManager").GetComponent<GameManager>().exitedFlubs ++;
    }
}
