using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rigidbodies;
    Animator animator;
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();

        DeactivateRagdoll();

    }

    // Update is called once per frame


    public void DeactivateRagdoll()
    {
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        animator.enabled = true;
    }


    public void ActivateRagdoll()
    {
        foreach(var rigidbody in rigidbodies) 
        {
            rigidbody.isKinematic = false;
        }
        animator.enabled = false;
    }
}
