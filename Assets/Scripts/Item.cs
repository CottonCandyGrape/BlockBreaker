using UnityEngine;
public enum ItemType
{
    Plus3,
    Multiple3,
}

public class Item : MonoBehaviour
{
    public ItemType Type;

    float speed = 2f;
    float outLine = -5f;
    float radius = 0.18f;

    void Start()
    {

    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        CollisionPaddle();

        if (transform.position.y < outLine)
        {
            Destroy(gameObject);
        }
    }

    void CollisionPaddle()
    {
        Vector2 nextPos = (Vector2)transform.position + Vector2.down * speed * Time.deltaTime;
        float dist = Vector2.Distance(transform.position, nextPos);

        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, radius, Vector2.down, dist);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("Paddle"))
            {
                SoundMgr.Inst.PlaySfxSound("item");

                if (Type == ItemType.Plus3)
                {
                    ItemMgr.Inst.PlusBall();
                }
                else if (Type == ItemType.Multiple3)
                {
                    ItemMgr.Inst.MultipleBall();
                }

                Destroy(gameObject);
            }
        }
    }
}
