    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private LayerMask jumpGround;

    private float dirX = 0f;
    [SerializeField] private float jumpHeight = 10.0f;
    [SerializeField] private int jumps = 0;
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private AudioSource jumpingSound;

    private enum AnimationState { idle, running, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        
         dirX = Input.GetAxisRaw("Horizontal");
         rb.velocity = new Vector2(dirX * playerSpeed, rb.velocity.y);
         
    
        
        
        //Jump
        if (Input.GetButtonDown("Jump") && jumps < 1)
        {
            jumpingSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            jumps++;
        }

        //Dash
        

        IsGrounded();
        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {
        AnimationState state;

        if (dirX > 0f)
        {
            state = AnimationState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = AnimationState.running;
            sprite.flipX = true;
        }
        else
        {
             state = AnimationState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = AnimationState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = AnimationState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private void IsGrounded()
    {
        if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpGround))
        {
            jumps = 0;
        }
    }
}
