using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveableMonster : LandMonster
{

    [SerializeField]
    private float speed = 2.0f;


    [SerializeField]
    protected AudioClip death;

    private Bullet bullet;

    private const float collDist = 0.72f;
    private const float collRad = 0.16f;

    private Vector3 direction;

    protected override void Update()
    {
        Move();
    }

    protected override void Start()
    {
        direction = transform.right;
        gettingScore = 40;
    }

    new protected void OnTriggerEnter2D(Collider2D collision)
    {
        Character unit = collision.GetComponent<Character>();

        if (unit)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < collDist && unit.transform.position.y - collRad > transform.position.y)
            {
                ReceiveDamage();
                unit.ForceJumpUp();
                GetComponent<AudioSource>().clip = death;
                GetComponent<AudioSource>().Play();
            }
            else unit.ReceiveDamage();
        }

    }

    public override void ReceiveDamage()
    {
        sprite.color = Color.red;
        gameObject.GetComponent<BoxCollider2D>().size = Vector2.zero;
        speed = 0f;
        Die();
      //  base.ReceiveDamage();
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.right * direction.x * collDist, collRad);
        Collider2D[] jams = Physics2D.OverlapCircleAll(transform.position - transform.up + transform.right * direction.x * collDist, collRad);
        if ((colliders.All(x => !x.GetComponent<Character>() && !x.GetComponent<Coin>())) && colliders.Length > 0 || jams.Length == 0)
        {
            Flip();
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }



    private void Flip()
    {
        direction = -direction;
        sprite.flipX = direction.x < 0;
    }

    protected override void GetScore()
    {
        FindObjectOfType<Character>().Score += gettingScore;
    }
}
