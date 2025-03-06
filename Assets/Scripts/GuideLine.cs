using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLine : MonoBehaviour
{
    public GameObject Ball;
    public Paddle Paddle;
    public LayerMask CollisionLayer;
    LineRenderer lineRenderer;

    Vector2 firstDir;

    bool drawable = false;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        GameMgr.Inst.gameState = GameState.Ready;
    }

    void Update()
    {
        if (drawable) DrawGuideLine();
    }

    void OnMouseDown()
    {
        drawable = true;
        lineRenderer.enabled = true;
    }

    void OnMouseUp()
    {
        drawable = false;
        Ball.GetComponent<Ball>().CurrentDir = firstDir;
        GameMgr.Inst.gameState = GameState.Play;
        gameObject.SetActive(false);
    }

    void DrawGuideLine()
    {
        lineRenderer.positionCount = 3;

        Vector2 currentMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMouse.y = Mathf.Clamp(currentMouse.y, Ball.transform.position.y + 0.1f, currentMouse.y);

        Vector2 currentDir = (currentMouse - (Vector2)Ball.transform.position).normalized;
        Vector2 currentPos = currentMouse;
        firstDir = currentDir;

        lineRenderer.SetPosition(0, (Vector2)Ball.transform.position + Vector2.one * currentDir * 0.25f);

        RaycastHit2D hit = Physics2D.Raycast(currentPos, currentDir, Mathf.Infinity, CollisionLayer);
        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
            currentDir = Vector2.Reflect(currentDir, hit.normal);
            currentPos = hit.point;

            Vector2 lastPos = currentPos + currentDir * Vector2.one;
            lineRenderer.SetPosition(2, lastPos);
        }
    }
}
