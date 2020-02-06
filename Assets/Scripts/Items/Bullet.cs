using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float speed = 10f;
    protected float timer = 1.5f;
    protected Vector3 direction;

    protected GameObject parent;
    public GameObject Parent { set { parent = value; } }

    public Vector3 Direction { set { direction = value; } }

    protected SpriteRenderer spriteRender;

    protected virtual void Awake()
    {
        spriteRender = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    protected  void Start()
    {
        Destroy(gameObject, timer);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        Character ch = collision.GetComponent<Character>();

        if (unit && unit.gameObject != parent)
        {
            Destroy(gameObject);
            unit.ReceiveDamage();
            if (ch)
                ch.ForceJumpOut(direction);
        }           

    }   
}
