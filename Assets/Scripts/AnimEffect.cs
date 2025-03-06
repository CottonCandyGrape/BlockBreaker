using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEffect : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            Destroy(gameObject);
    }
}
