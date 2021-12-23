using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 Movement = Vector2.zero;

    private SpriteRenderer spr;
    private bool facingLeft = false;


    private Animator anim;


    public Transform CheckGround;
    public LayerMask GroundMask;
    public bool isGrounded = false;
    private bool wasGrounded=false;
    private bool isJumping = false;
    public float JumpSpeed = 10f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("Walking", false);

    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        PlayerAnimation();
        CheckisGrounded();
        PlayerMove();
    }

    private void CheckInput()
    {

        Movement = new Vector2(Input.GetAxis("Horizontal"), 0f).normalized;

        if (Input.GetButtonDown("Jump") && !isJumping)
            isJumping = true;

        FlipSprite();

    }

    private void FlipSprite()
    {

        if (Movement.x < 0)
            facingLeft = true;
        else if (Movement.x > 0)
            facingLeft = false;

        spr.flipX = facingLeft;


    }

    private void CheckisGrounded()
    {

        Collider2D col = null;
        wasGrounded = isGrounded;
        isGrounded = false;

        col = Physics2D.OverlapCircle(CheckGround.position, 0.1f, GroundMask);

        if (col != null)
            isGrounded = true;
        else
            isGrounded = false;



    }

    private void PlayerAnimation()
    {

        if (Movement.x != 0f)
            anim.SetBool("Walking", true);
        else
            anim.SetBool("Walking", false);



    }

    private void PlayerJump()
    {

        if (isJumping && isGrounded)
            Movement.y = JumpSpeed;

        if (!wasGrounded && isGrounded)
            isJumping = false;

    }

    private void PlayerMove()
    {
        Movement *= speed;

        PlayerJump();

        Movement.y += rb.velocity.y;

        rb.velocity = Movement;

    }
}
