using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float baseSpeed = 2f;
    public float speed;
    float[] speedUp = { 1.1f, 1.2f, 1.4f, 1.6f, 1.8f, 2.0f };

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
    public void LevelUp() {
        speed = baseSpeed * speedUp[GameManager.instance.level];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
        }
    }
}
