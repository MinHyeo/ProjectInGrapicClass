using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("플레이어 정보")]
    public float speed;
    public float jumpPower;
    Vector2 inputVec;
    Vector2 jumpVec;

    [Header("충돌 처리")]
    [SerializeField] LayerMask groundLayer;
    RaycastHit2D hit;

    [Header("플레이어 Component")]
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Animator anim;

    bool isJump = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        // 이동 구현
        Move();
        // 점프 구현
        Jump();
    }
    private void Move()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        transform.Translate(nextVec, Space.World);
        anim.SetFloat("Speed", nextVec.magnitude);
    }
    private void Jump()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer)) {
            Debug.Log("땅 접촉");
            isJump = false;
        }
    }
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
        sprite.flipX = inputVec.x < 0;
    }
    void OnJump(InputValue value)
    {
        if (isJump)
            return;

        rigid.velocity = Vector2.up * jumpPower;
        isJump = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEatAble eatAble = collision.GetComponent<IEatAble>();
        if (eatAble != null)
        {
            eatAble.Eat();
            collision.gameObject.SetActive(false);
            GameManager.instance.CoinSpawn();
        }
    }
}
