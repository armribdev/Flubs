using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject pont;
    public Sprite pressed, released;
    private BridgeController bc;
    private SpriteRenderer sr;

    void Start()
    {
        bc = pont.GetComponent<BridgeController>();
        sr = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D()
    {
        Debug.Log("Bouton " + transform.name + " enfoncé");
        sr.sprite = pressed;
        bc.open();
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
