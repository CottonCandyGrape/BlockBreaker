using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public GameObject Ball;

    Vector2 startPos = new Vector2(0, -4);
    Vector2 readyBallPos;
    float offsetX;
    float wallDist = 2.1f;
    float ballY;
    float paddleY;

    bool movable = false;

    void Start()
    {
        SetStartPos();
    }

    void Update()
    {
        if ((GameMgr.Inst.gameState == GameState.Ready ||
        GameMgr.Inst.gameState == GameState.Play) && movable) //끝나고 안움직이게
        {
            Move();
        }
    }

    public void SetStartPos()
    {
        transform.position = startPos;

        if (Ball != null)
        {
            ballY = Ball.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            paddleY = GetComponent<SpriteRenderer>().bounds.size.y / 2;
            Ball.transform.position = (Vector2)transform.position + new Vector2(0, paddleY + ballY);
        }
    }

    void OnMouseDown()
    {
        movable = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offsetX = mousePos.x - transform.position.x;
    }

    void OnMouseUp()
    {
        movable = false;
    }

    void Move()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x -= offsetX;
        mousePos.x = Mathf.Clamp(mousePos.x, -wallDist, wallDist);
        mousePos.y = startPos.y;
        mousePos.z = 0;

        transform.position = mousePos;

        if (GameMgr.Inst.gameState == GameState.Ready)
        {
            readyBallPos.x = transform.position.x;
            readyBallPos.y = transform.position.y + ballY + paddleY;
            Ball.transform.position = readyBallPos;
        }
    }
}
