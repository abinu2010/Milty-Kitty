using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private Animator anim;
    private SpriteRenderer sp;
    private Vector2 inputVector, moveVector;
    private Vector3 groundCheckA, groundCheckB, ceilingCheckA, ceilingCheckB;
    private float yVel;
    public float gravity = 9.81f;
    public float jumpVel = 9.81f;
    public float climbVel = 9.81f;
    public float speed = 5f;
    public float groundcheckRadius = 0.1f;
    public LayerMask groundLayers, enemyLayer, ceilingLayer;
    bool grounded, jumpPressed, jumping, squishEnemy, extraJump, ceilinged, climbing; 
    public bool laddered, wasLaddered;
    public GameObject audioObject;
    public AudioManager am;
    float sinceLastFootstep;
    float timeBetweenFootsteps = 0.3f;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        am= FindObjectOfType<AudioManager>();
        col = GetComponent<CapsuleCollider2D>();
        CalculateScales();
        Manager.LastCheckPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        CalculateMovement();
        ControlAnimation();


        //print("the input vector is" + inputVector);
    }

    void GetInputs()
    {
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        jumpPressed = Input.GetButtonDown("Jump");
    }

    void ControlAnimation()
    {
        if(!Manager.gamePaused)
        {
            if (inputVector.x != 0f)
            {
                anim.SetBool("Walk", true);
                if (inputVector.x > 0f)
                {
                    sp.flipX = false;
                }
                else
                {
                    sp.flipX = true;
                }

            }
            else
            {
                anim.SetBool("Walk", false);
            }
            anim.SetBool("Jump", jumping);
            anim.SetBool("Climb", climbing);
        }


    }

    void CalculateMovement()
    {
        if (!Manager.gamePaused) 
        {
            grounded = CheckCollision(groundCheckA, groundCheckB, groundLayers);
            ceilinged = CheckCollision(ceilingCheckA, ceilingCheckB, ceilingLayer);

            //print("Grounded = " + grounded);

            if (jumpPressed)
            {
                jumpPressed = false;
                if (grounded)
                {
                    jumping = true;
                    yVel = jumpVel;
                    am.AudioTrigger(AudioManager.SoundFXCat.Jump, transform.position, 1f);

                }
                if (extraJump)
                {
                    extraJump = false;
                    jumping = true;
                    yVel = jumpVel;
                }
                if (ceilinged)
                {
                    if (squishEnemy)
                    {
                        extraJump = true;
                        jumping = true;
                        yVel = jumpVel * 0.5f;
                    }
                }
            }
            if (!grounded && yVel < 0f)
            {
                squishEnemy = CheckCollision(groundCheckA, groundCheckB, enemyLayer);
                if (squishEnemy)
                {
                    extraJump = true;
                    jumping = true;
                    yVel = jumpVel * 0.5f;
                }
            }
            if (grounded && yVel <= 0f || ceilinged && yVel > 0f)
            {
                if (grounded && jumping)
                    am.AudioTrigger(AudioManager.SoundFXCat.HitGround, transform.position, 1f);
                if (grounded && jumping)
                    am.AudioTrigger(AudioManager.SoundFXCat.HitCeiling, transform.position, 1f);
                yVel = 0f;
                jumping = false;
            }
            else
            {
                yVel -= gravity * Time.deltaTime;
            }
            if (laddered && !wasLaddered)
            {
                if (inputVector.y != 0f)
                {
                    climbing = true;
                    wasLaddered = true;

                }
            }

            if (wasLaddered && !laddered)
            {
                climbing = false;
                wasLaddered = false;
            }
            if (climbing)
            {
                yVel = climbVel * inputVector.y;
            }


            moveVector.y = yVel;
            moveVector.x = inputVector.x * speed;

            sinceLastFootstep += Time.deltaTime;
            if (moveVector.x != 0f && grounded)
            {
                if(sinceLastFootstep > timeBetweenFootsteps)
                {
                    sinceLastFootstep = 0f;
                    am.AudioTrigger(AudioManager.SoundFXCat.FootStepConcrete, transform.position, 1f);
                }
            }
            if (moveVector.y != 0f && laddered)
            {
                if (sinceLastFootstep > timeBetweenFootsteps)
                {
                    sinceLastFootstep = 0f;
                    am.AudioTrigger(AudioManager.SoundFXCat.FootStepWood, transform.position, 1f);
                }

        } 
            }
        


    }
    bool CheckCollision(Vector3 a, Vector3 b, LayerMask l)
    {
        Collider2D colA = Physics2D.OverlapCircle(transform.position - a, groundcheckRadius, l);
        Collider2D colB = Physics2D.OverlapCircle(transform.position - b, groundcheckRadius, l);
        if (colA)
        {
            if (l == enemyLayer && yVel < 0f)
            {
                Debug.Log("enemy");
                colA.gameObject.GetComponent<EnemyHealthSystem>().RecieveHit(1);
             
            }
            return true;
        }else if (colB)
        {
            if(l == enemyLayer && yVel < 0f)
            {
                colB.gameObject.GetComponent<EnemyHealthSystem>().RecieveHit(1);

            }
            return true;
        }
        else
        {
            return false;
        }
    }
    void CalculateScales()
    {
        groundCheckA = -col.offset - new Vector2(col.size.x/2f - (groundcheckRadius * 1.2f), - col.size.y/2.1f);
        groundCheckB = -col.offset - new Vector2(- col.size.x / 2f + (groundcheckRadius * 1.2f), - col.size.y/2.1f);

        ceilingCheckA = -col.offset - new Vector2(col.size.x / 2f - (groundcheckRadius * 1.2f), col.size.y / 2.1f);
        ceilingCheckB = -col.offset - new Vector2(-col.size.x / 2f + (groundcheckRadius * 1.2f), col.size.y / 2.1f);

    }
    private void FixedUpdate()
    {
        rb.velocity = moveVector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position - groundCheckA, groundcheckRadius);
        Gizmos.DrawWireSphere(transform.position - groundCheckB, groundcheckRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position - ceilingCheckA, groundcheckRadius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position - ceilingCheckB, groundcheckRadius);

    }
}
  