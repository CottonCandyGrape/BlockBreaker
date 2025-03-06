using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject[] PopPrefabs;
    public GameObject[] ItemPrefabs;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void BreakBrick()
    {
        SoundMgr.Inst.PlaySfxSound("break");

        if (PopPrefabs != null)
            Instantiate(PopPrefabs[Random.Range(0, PopPrefabs.Length)], transform.position, Quaternion.identity);

        if (ItemPrefabs != null)
        {
            int val = Random.Range(0, 10);
            if (val < 2)
                Instantiate(ItemPrefabs[Random.Range(0, ItemPrefabs.Length)], transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
