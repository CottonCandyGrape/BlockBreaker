using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMgr : MonoBehaviour
{
    public GameObject BallPrefab;

    Transform balls;
    GameObject paddle;

    public static ItemMgr Inst = null;

    void Awake()
    {
        if (Inst == null) Inst = this;

        balls = GameObject.Find("Balls").transform;
        paddle = GameObject.Find("Paddle");
    }

    void Start()
    {

    }

    public void PlusBall()
    {
        float angle = 45;
        for (int i = -1; i < 2; i++)
        {
            Ball ballSc = Instantiate(BallPrefab, balls).GetComponent<Ball>();
            ballSc.transform.position = paddle.transform.position;
            ballSc.CurrentDir = (Quaternion.AngleAxis(i * angle, Vector3.forward) * Vector2.up).normalized;
        }
    }

    public void MultipleBall()
    {
        Ball[] ballChildren = balls.GetComponentsInChildren<Ball>();
        for (int i = 0; i < ballChildren.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Ball ballSc = Instantiate(BallPrefab, balls).GetComponent<Ball>();
                ballSc.transform.position = ballChildren[i].transform.position;
                ballSc.CurrentDir = (Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * ballChildren[i].CurrentDir).normalized;
            }
        }
    }
}
