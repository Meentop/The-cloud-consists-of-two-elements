using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool active;

    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator anim;
    [SerializeField] float speed, jumpHeight;
    [SerializeField] Transform groundCheck;
    [SerializeField] string deadTag;
    [SerializeField] GameObject leftMB, deathPar;
    bool isGrounded;
    Sounds sound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Camera.main.GetComponent<Main>().SetActiveFire();
        sound = Sounds.Instante;
    }

    private void Update()
    {
        if (active)
        {
            GroundCheck();
            if (Input.GetAxis("Horizontal") == 0 && isGrounded)
            {
                anim.SetInteger("State", 0);
            }
            else
            {
                Flip();
                if (isGrounded)
                    anim.SetInteger("State", 1);
            }
        }
        else
        {
            leftMB.SetActive(false);
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && active)
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            sound.PlayJump();
        }
    }

    private void FixedUpdate()
    {
        if (active)
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        else
            rb.velocity = new Vector2(-0.0055f, rb.velocity.y);
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            sprite.flipX = false;
        else if (Input.GetAxis("Horizontal") < 0)
            sprite.flipX = true;
    }

    void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.15f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded)
        {
            anim.SetInteger("State", 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == deadTag)
        {
            Camera.main.GetComponent<Main>().Lose();
            Vector3 splitPos = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z - 2f);
            Instantiate(deathPar, splitPos, deathPar.transform.rotation);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == deadTag + "Zone" && active)
        {
            leftMB.SetActive(true);
            if (Input.GetMouseButtonDown(0))
                Camera.main.GetComponent<Main>().SpawnMainPlayer(transform.position);
        }
        if (collision.gameObject.tag == "Arm")
        {
            if (active)
                collision.gameObject.GetComponent<Arm>().Activation();
            else
                collision.gameObject.GetComponent<Arm>().Deactivation();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == deadTag + "Zone")
        {
            leftMB.SetActive(false);
        }
        if (collision.gameObject.tag == "Arm")
        {
            collision.gameObject.GetComponent<Arm>().Deactivation();
        }
    }
}
