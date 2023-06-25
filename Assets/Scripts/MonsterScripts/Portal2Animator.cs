using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal2Animator : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        animator.Play("portal2");
    }
}
