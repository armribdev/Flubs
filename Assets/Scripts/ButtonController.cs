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

    void OnCollisionEnter()
    {
        Debug.Log("Bouton " + transform.name + " enfoncé");
        sr.sprite = pressed;
        bc.open();
    }
}
