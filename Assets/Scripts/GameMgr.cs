using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    None,
    Ready,
    Play,
    GameOver,
}

public class GameMgr : MonoBehaviour
{
    [HideInInspector]
    public GameState gameState = GameState.None;

    public GameObject brick;
    public Sprite[] brickSprites;
    Transform bricks;
    Vector2 startPos = new Vector2(0, 4);

    public GameObject paddle;

    //GameUI
    public Text GameOver_Txt;
    //GameUI

    public static GameMgr Inst = null;

    void Awake()
    {
        Inst = this;
        bricks = GameObject.Find("Bricks").transform;

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    void Start()
    {
        SetBricks();
    }

    void SetBricks()
    {
        for (int y = 0; y < 5; y++)
        {
            for (int x = -2; x < 3; x++)
            {
                GameObject brickObj = Instantiate(brick, bricks);
                Vector2 brickSize = brickObj.GetComponent<SpriteRenderer>().bounds.size;
                brickObj.transform.position
                = new Vector2(startPos.x + x * (brickSize.x + 0.1f), startPos.y - y * (brickSize.y + 0.1f));
                Brick brickSc = brickObj.GetComponent<Brick>();
                if (brickSc != null)
                {
                    brickSc.SetSprite(brickSprites[y]);
                }
            }
        }
    }

    public IEnumerator GameOver(bool isClear)
    {
        GameOver_Txt.gameObject.SetActive(true);
        if (isClear)
        {
            SoundMgr.Inst.PlaySfxSound("clear");
            GameOver_Txt.text = "Clear!";
        }
        else
        {
            SoundMgr.Inst.PlaySfxSound("gameover");
            GameOver_Txt.text = "Game Over";
        }

        gameState = GameState.GameOver;
        Time.timeScale = 0.0f;

        yield return new WaitForSecondsRealtime(1f);

        paddle.GetComponent<Paddle>().SetStartPos();

        yield return new WaitForSecondsRealtime(1f);

        SceneManager.LoadScene("Menu");
    }
}
