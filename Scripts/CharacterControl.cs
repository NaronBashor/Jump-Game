using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
        #region Inspector

        Animator anim;
        Rigidbody2D rb;
        Collider2D coll;

        Damageable damageable;

        [SerializeField] private float jumpForce;
        [SerializeField] private float groundDistance;

        [SerializeField] private float jumpBoostForce;
        [SerializeField] private float flightBoostForce;

        [SerializeField] private LayerMask groundMask;

        [SerializeField] private bool isGrounded;

        [SerializeField] private AudioSource coinCollect;
        [SerializeField] private AudioSource jump;
        [SerializeField] private AudioSource jumpBoost1;
        [SerializeField] private AudioSource jumpBoost2;

        private int coinCount;
        private bool canJump = false;
        private bool jumpSoundPlayed = false;

        #endregion

        public bool IsAlive { get => anim.GetBool("isAlive"); set => anim.SetBool("isAlive" , value); }

        private void Start()
        {
                rb = GetComponent<Rigidbody2D>();
                anim = GetComponent<Animator>();
                coll = GetComponent<Collider2D>();
                damageable = GetComponent<Damageable>();
        }

        private void Update()
        {
                anim.SetFloat("dirY" , rb.velocity.y);
                if (isGrounded)
                {
                        anim.SetBool("grounded" , true);
                        Jump();
                }
                else if (!isGrounded)
                {
                        anim.SetBool("grounded" , false);
                }
        }

        private void FixedUpdate()
        {
                RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center , Vector2.down , groundDistance , groundMask);
                if (hit)
                {
                        Debug.DrawRay(coll.bounds.center , Vector2.down * groundDistance , Color.green);
                        canJump = true;
                        isGrounded = true;
                }
                else if (!hit)
                {
                        Debug.DrawRay(coll.bounds.center , Vector2.down * groundDistance , Color.red);
                        isGrounded= false;
                        jumpSoundPlayed = false;
                }
        }

        public void Jump()
        {
                if (canJump)
                {
                        if (!jumpSoundPlayed)
                        { jumpSoundPlayed = true; jump.Play(); }
                        canJump = false;
                        anim.SetTrigger("jump");
                        rb.velocity = new Vector2(rb.velocity.x , (rb.velocity.y + 1) * jumpForce);
                }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision != null)
                {
                        if (collision.CompareTag("Coin"))
                        {
                                coinCollect.Play();
                                coinCount++;
                                collision.GetComponent<Animator>().SetTrigger("destroy");
                                StartCoroutine(Delay());
                                IEnumerator Delay()
                                { yield return new WaitForSeconds(0.2f); Destroy(collision.gameObject); }
                        }
                        if (collision.CompareTag("JumpBoost"))
                        {
                                jumpBoost1.Play();
                                jumpBoost2.Play();
                                if (rb.velocity.y  > 0)
                                {
                                        rb.velocity = new Vector2(rb.velocity.x , (rb.velocity.y + 1) * jumpBoostForce);
                                }
                                else
                                {
                                        rb.velocity = new Vector2(rb.velocity.x , (rb.velocity.y + 1) * -jumpBoostForce);
                                }
                                anim.SetTrigger("jump");
                                collision.GetComponent<Animator>().SetTrigger("destroy");
                                StartCoroutine(Delay());
                                IEnumerator Delay()
                                { yield return new WaitForSeconds(0.4f); Destroy(collision.gameObject); }
                        }
                        if (collision.CompareTag("FlightBoost"))
                        {
                                jumpBoost1.Play();
                                jumpBoost2.Play();
                                if (rb.velocity.y > 0)
                                {
                                        rb.velocity = new Vector2(rb.velocity.x , (rb.velocity.y + 1) * flightBoostForce);
                                }
                                else
                                {
                                        rb.velocity = new Vector2(rb.velocity.x , (rb.velocity.y + 1) * -flightBoostForce);
                                }
                                anim.SetTrigger("jump");
                                collision.GetComponent<Animator>().SetTrigger("destroy");
                                StartCoroutine(Delay());
                                IEnumerator Delay()
                                { yield return new WaitForSeconds(0.2f); Destroy(collision.gameObject); }
                        }
                        if (collision.CompareTag("HpBoost"))
                        {
                                if (damageable.OnHeal(1))
                                {
                                        collision.GetComponent<Animator>().SetTrigger("destroy");
                                        StartCoroutine(Delay());
                                        IEnumerator Delay()
                                        { yield return new WaitForSeconds(0.2f); Destroy(collision.gameObject); }
                                }
                        }
                }
        }
}
