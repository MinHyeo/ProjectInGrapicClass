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

        rigid.AddForce(jumpVec * jumpPower);
    }
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        jumpVec = value.Get<Vector2>();
    }
}
