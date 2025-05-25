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
    public SpriteRenderer spriteRenderer;
    private bool facingRight = true;

    [Header("Collision Check")]
    [SerializeField] private LayerMask whatIsGround; // ground
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private bool isGrounded;

    [Header("Movement")]
    [SerializeField] private bool isAlive;
    [SerializeField] public float moveSpeed = 2.0f;
    [SerializeField] public int health = 3;
    private bool Alive;
    private Transform playerTransform;
    private int directionMultiplier = 1; // Baþlangýç deðeri
    private float baseMoveSpeed; // Orijinal hýz deðerini saklamak için

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //anim
        Alive = true;
        baseMoveSpeed = moveSpeed; // Orijinal hýz deðerini sakla
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
            EnemySpawner.Instance.EnemyDied();
        }
    }

    private void Die()
    {
        EnemySpawner.Instance.SpawnEntchantMaterial(this.transform.position);
        EnemyDie();
    }

    private void UpdateMoveSpeed()
    {
        playerTransform = EnemySpawner.Instance.GetPlayerTransform();
        float xPlayer = playerTransform.position.x;
        float xEnemy = this.transform.position.x;

        if (xPlayer > xEnemy)
        {
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
            directionMultiplier = 1;
        }
        else
        {

            if (spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
            directionMultiplier = -1;
        }

        moveSpeed = baseMoveSpeed * directionMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Alive)
        {
            UpdateMoveSpeed();
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
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
}