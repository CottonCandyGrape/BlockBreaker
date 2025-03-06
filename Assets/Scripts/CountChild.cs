using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountChild : MonoBehaviour
{
    bool isClear;

    void Awake()
    {
        if (transform.name == "Balls")
            isClear = false;
        else
            isClear = true;
    }

    void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0)
            StartCoroutine(GameMgr.Inst.GameOver(isClear));
    }
}
