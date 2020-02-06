using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected SpriteRenderer sprite;
    protected Animator animator;
    new protected Rigidbody2D rigidbody;


    public virtual void ReceiveDamage()
    {
        Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void Start() { }
    protected virtual void Awake() { }
    protected virtual void Update() { }
}
