using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rb;
    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = new Vector2(0, -speed);
    }
}
