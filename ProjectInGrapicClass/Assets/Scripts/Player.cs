using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    Vector2 inputVec;
    Vector2 jumpVec;

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
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        transform.Translate(nextVec, Space.World);
        sprite.flipX = nextVec.x < 0;
        anim.SetFloat("Speed", nextVec.magnitude);

        Jump();
    }
    void Jump()
    {
        if (anim.GetBool("isJump"))
            return;

        rigid.AddForce(jumpVec * jumpPower);
        anim.SetFloat("Jump", rigid.velocity.y);
    }
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        //jumpVec = value.Get<Vector2>();
        jumpVec = Vector2.up;
        anim.SetBool("isJump", true);
    }
}
