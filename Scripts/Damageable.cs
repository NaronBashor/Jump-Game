using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
        Animator anim;
        Rigidbody2D rb;

        [SerializeField] private int maxHealth = 3;
        [SerializeField] private int health;
        [SerializeField] private float spikeKnockbackForce;

        [SerializeField] private AudioSource hpBoost;
        [SerializeField] private AudioSource hitTrap;

        public bool IsAlive { get => anim.GetBool("isAlive"); set => anim.SetBool("isAlive" , value); }

        private void Start()
        {
                anim = GetComponent<Animator>();
                rb = GetComponent<Rigidbody2D>();

                health = maxHealth;
        }

        private void Update()
        {
                if (health <= 0)
                {
                        anim.SetBool("isAlive" , false);
                }
        }

        public void OnHit(int damage)
        {
                if (health > 0)
                {
                        hitTrap.Play();
                        anim.SetTrigger("hit");
                        health -= damage;
                        rb.velocity = new Vector2(-1 * spikeKnockbackForce , 2 * spikeKnockbackForce);
                }
        }

        public bool OnHeal(int restore)
        {
                if (health < 2)
                {
                        hpBoost.Play();
                        health += restore;
                        return true;
                }
                else
                {
                        return false;
                }
        }
}
