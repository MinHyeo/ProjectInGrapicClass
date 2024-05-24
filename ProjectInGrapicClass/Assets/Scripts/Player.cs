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
        Debug.DrawRay(rigid.position, Vector2.down,new Color(1, 0, 0), 0.5f);
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayer))
        {
            Debug.Log("���� �浹");
            anim.SetBool("isJump", false);
        }
        // �̵� ����
        Move();
        // ���� ����
        Jump();
    }
    private void Move()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        transform.Translate(nextVec, Space.World);
        anim.SetFloat("Speed", nextVec.magnitude);
    }
    void Jump()
    {
        if (anim.GetBool("isJump"))
            return;

        rigid.AddForce(jumpVec * jumpPower);
        anim.SetBool("isJump", true);
        anim.SetFloat("Jump", rigid.velocity.y);
    }
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
        sprite.flipX = inputVec.x < 0;
    }
    void OnJump(InputValue value)
    {
        jumpVec = Vector2.up;
    }
}
