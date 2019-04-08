using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimator : MonoBehaviour
{
    Animator animator;
    Vector3 lastPositon;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        lastPositon = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(lastPositon != transform.position)
        {
            Vector3 dir = transform.position - lastPositon;
            animator.SetFloat("Horizontal", dir.x * 10);
            animator.SetFloat("Vertical", dir.z * 10    );
            lastPositon = transform.position;
        }

    }
}
