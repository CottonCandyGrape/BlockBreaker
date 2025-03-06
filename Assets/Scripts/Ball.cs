using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Markup;
using Unity.Mathematics;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameObject paddle;
    Vector2 paddleSize;
    Vector2 paddleCollVec;

    Vector2 currentDir;
    public Vector2 CurrentDir
    {
        get { return currentDir; }
        set { currentDir = value; }
    }
    float speed = 10;

    float radius = 0.125f;

    void Awake()
    {
        paddle = GameObject.Find("Paddle");
        paddleSize = paddle.GetComponent<SpriteRenderer>().bounds.size;
    }

    void Start()
    {

    }

    void Update()
    {
        if (GameMgr.Inst.gameState == GameState.Play)
            Move();
    }

    void Move()
    {
        transform.position =
        (Vector2)transform.position + currentDir * speed * Time.deltaTime;

        CollisionCheck();

        //Ball이 아래로 떨어지면 Destroy() 
        if (transform.position.y < paddle.transform.position.y - 0.5f)
            Destroy(gameObject);
    }

    void CollisionCheck()
    {
        Vector2 nextPos = (Vector2)transform.position + currentDir * speed * Time.deltaTime;
        float dist = Vector2.Distance(transform.position, nextPos);

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, currentDir, dist);
        if (hit.collider)
        {
            if (hit.collider.CompareTag("Item"))
            {
                return;
            }
            else if (hit.collider.CompareTag("Paddle"))
            {
                float x = (hit.point.x - paddle.transform.position.x) / (paddleSize.x / 2);
                paddleCollVec.x = x;
                paddleCollVec.y = 1;
                currentDir = paddleCollVec.normalized;
            }
            else //Brick, Wall일 경우
            {
                if (hit.collider.CompareTag("Brick"))
                {
                    Brick brickSc = hit.collider.gameObject.GetComponent<Brick>();
                    if (brickSc != null) brickSc.BreakBrick();
                }
                currentDir = Vector2.Reflect(currentDir, hit.normal).normalized;
            }

            SoundMgr.Inst.PlaySfxSound("ball");
        }
    }
}
