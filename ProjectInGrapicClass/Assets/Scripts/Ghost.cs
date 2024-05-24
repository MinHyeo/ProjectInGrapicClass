using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float speed;

    public Rigidbody2D playerRigid;
    Rigidbody2D rigid;
    SpriteRenderer sprite;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        // 플레이어 추적 및 이동
        Vector2 playerVec = playerRigid.position - rigid.position;
        rigid.velocity = playerVec.normalized * speed;

        sprite.flipX = playerVec.x < 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
        }
    }
}
