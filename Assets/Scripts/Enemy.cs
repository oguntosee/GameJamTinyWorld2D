using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rb;
    private Animator anim;
    private bool facingRight = true;
    [Header("Collision Check")]
    [SerializeField] private LayerMask whatIsGround; // ground
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private bool isGrounded;
    [Header("Movement")]
    
    [SerializeField] private bool isAlive;
    [SerializeField] public float moveSpeed = 5.0f;
    [SerializeField] public int health = 3;
    private bool Alive;


    private Transform playerTransform;
    private int directionMultiplier;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); //anim
        Alive = true;

    }
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Snowball"))
        {

            TakeDamage(1);
            Destroy(collision.gameObject);

        }
    }

    private void TakeDamage(int damage)
    {
        //isAlive = false;
        health -= damage;
        if (health <= 0)
        {
            
            Die();
        }
    }


    private void Die()
    {
        EnemyDie();
        
    }

    private void UpdateMoveSpeed()
    {
        playerTransform = EnemySpawner.Instance.GetPlayerTransform();
        float xPlayer = playerTransform.position.x;
        float xEnemy = this.transform.position.x;
        if (xPlayer > xEnemy)
            directionMultiplier = 1;
        else
            directionMultiplier = -1;

        moveSpeed *= directionMultiplier;

    }

    // Update is called once per frame
    void Update()
    {

        if (Alive)
        {
            UpdateMoveSpeed();
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y); //rigidbody
        }

    }

    private void EnemyDie()
    {
        StartCoroutine(EnemyDieCoroutine());
    }


    private IEnumerator EnemyDieCoroutine()
    {
        Alive = false;
        anim.SetTrigger("die");
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1.5f); // adjust to your animation time
        
        Destroy(gameObject);
    }
    /* 
     anim.SetBool("isAlive", isAlive);
        
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.5f);
    */


}
