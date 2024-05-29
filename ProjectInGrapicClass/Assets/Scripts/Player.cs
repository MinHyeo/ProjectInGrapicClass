using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("�÷��̾� ����")]
    public float speed;
    public float jumpPower;
    Vector2 inputVec;
    Vector2 jumpVec;

    [Header("�浹 ó��")]
    [SerializeField] LayerMask groundLayer;
    RaycastHit2D hit;

    [Header("�÷��̾� Component")]
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Animator anim;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        // �̵� ����
        Move();
    }
    private void Move()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        transform.Translate(nextVec, Space.World);
        anim.SetFloat("Speed", nextVec.magnitude);
    }
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
        sprite.flipX = inputVec.x < 0;
    }
    void OnJump(InputValue value)
    {
        if (rigid.velocity.y != 0)
            return;

        rigid.velocity = Vector2.up * jumpPower;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEatAble eatAble = collision.GetComponent<IEatAble>();
        if (eatAble != null)
        {
            eatAble.Eat();
            collision.gameObject.SetActive(false);

            GameManager.instance.EatItem();
        }
    }
}