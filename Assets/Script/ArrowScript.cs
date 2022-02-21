using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private SpriteRenderer spriteR;
    private BoxCollider boxColl;
    private Rigidbody rb;
    private float timer = 0f;
    private float destructTime = 2f;

    public float speed = 100f;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        spriteR = GetComponent<SpriteRenderer>();
        boxColl = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        rb.velocity = -transform.right * speed;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > destructTime)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            spriteR.enabled = false;
            boxColl.enabled = false;
        }
    }
}
