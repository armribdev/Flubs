using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Flub : MonoBehaviour
{
    [SerializeField] private bool selected;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        selected = false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void select(bool s) {
        selected = s;
        animator.SetBool("selected", s);
    }
}
