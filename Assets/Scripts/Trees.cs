using UnityEngine;

public class Trees : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator anim;

    [Header("AnimControl")]
    public float animSpeed = 1.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("okay");
        }
    }
}
