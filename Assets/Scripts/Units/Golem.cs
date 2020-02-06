using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Golem : Unit
{
    public enum GolemState
    {
        idle, disappear, appear, attack, rocket, die
    }

    private GolemState State
    {
        get { return (GolemState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    [SerializeField]
    Obstacle[] saws;

    [SerializeField]
    AudioClip damage, hit, teleport, rocket, defeat;

    private Character character;
    private BossHPbar hp;

    private Vector3[] positions;
    private Vector3 mid, left, lup, rup, right;
    private Vector3 nextPos;

    private const float collDist = 0.64f;
    private const float collRad = 0.1f;

    //private bool lookRight;
    private bool disappear;
    private bool attack;
    private bool come;
    private bool die;

    private int canShoot;

    public int Health { get; set; }
    private int health;


    private float transTimer;
    private float disTimer;
    private float attackTimer;
    private float forceAttackTimer;
    private float sawsTimer;


    private Missile missile;

    private const float missileDistance = 1.6f;

    protected override void Awake()
    {
        character = FindObjectOfType<Character>();
        sprite = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hp = FindObjectOfType<BossHPbar>();
        missile = Resources.Load<Missile>("Missile");

        base.Awake();
    }
    protected override void Start()
    {
        mid = new Vector3(7.44f, 0.74f);
        left = new Vector3(-1.48f, 4f);
        lup = new Vector3(4.24f, 6.82f);
        rup = new Vector3(12f, 6.74f);
        right = new Vector3(15.32f, 3.74f);

        positions = new Vector3[] { mid, left, lup, rup, right };

        transTimer = 10f;
        forceAttackTimer = 0.5f;
        sawsTimer = 8f;
        transform.position = nextPos = mid;
        disappear = false;
        attack = false;
        come = true;
        die = false;
        Health = 100;

        InvokeRepeating("PrepareShot", 5f, 5f);

        canShoot = 0;
        State = GolemState.appear;
    }


    protected override void Update()
    {
        if (!die)
        {
            sprite.flipX = character.transform.position.x - transform.position.x > 0;
            if (attackTimer < 0)
                attack = false;
            else attackTimer -= 0.1f;
            if (!attack)
                ChangePos();
        }
        TakeSaws();

    }



    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Character unit = collision.GetComponent<Character>();

        if (unit && !attack && !die)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < collDist && unit.transform.position.y - collRad > transform.position.y)
            {
                ReceiveDamage();
                unit.ForceJumpUp();

                GenRndPos();
                ForceChangePos();
                //              GetComponent<AudioSource>().clip = death;
                //                GetComponent<AudioSource>().Play();
            }
            else
            {
                State = GolemState.attack;
                Invoke("Attack", 0.2f);
                attack = true;
                attackTimer = 1f;

            }
        }
    }

    private void Attack()
    {
        character.ReceiveDamage();
        GetComponent<AudioSource>().clip = hit;
        GetComponent<AudioSource>().Play();
    }

    private void ChangePos()
    {


        if (disTimer > 0 && disappear)
            disTimer -= 0.15f;
        else
            State = GolemState.idle;
        if (transTimer >= 0)
            transTimer -= 0.05f;


        if (transTimer <= 0)
        {
            var temp = Calcpos(character.transform.position);
            if (transform.position != temp)
            {
                State = GolemState.disappear;
                disTimer = 2f;
                disappear = true;
                // transform.position = temp;
                nextPos = temp;
            }
            transTimer = 10f;
            forceAttackTimer -= 0.25f;

        }
        if (disTimer < 0)
        {
            if (transform.position != nextPos)
            {
                transform.position = nextPos;
                GetComponent<AudioSource>().clip = teleport;
                GetComponent<AudioSource>().Play();
            }
            disappear = false;
            State = GolemState.appear;

        }


        if (forceAttackTimer <= 0)
        {
            GenRndPos();
            forceAttackTimer = 0.5f;
            ForceChangePos();

        }
    }

    private void ForceChangePos()
    {
        State = GolemState.disappear;
        disTimer = 2f;
        disappear = true;
        transTimer = 12f;
    }

    private Vector3 Calcpos(Vector3 charPos)
    {

        Vector3 newPos = mid;
        var trans = Math.Abs(charPos.x - mid.x) + Math.Abs(charPos.y - mid.y);
        foreach (var item in positions)
        {
            if (Math.Abs(charPos.x - item.x) + Math.Abs(charPos.y - item.y) < trans)
            {
                newPos = item;
                trans = Math.Abs(charPos.x - item.x) + Math.Abs(charPos.x - item.x) + Math.Abs(charPos.y - item.y);
            }
        }
        return newPos;
    }

    public override void ReceiveDamage()
    {
        GetComponent<AudioSource>().clip = damage;
        GetComponent<AudioSource>().Play();
        Health -= 5;
        hp.CurrentValue = Health / 100f;
        switch ((int)Health)
        {
            case 70: canShoot = 1;
                break;
            case 50:
                canShoot = 2;
                break;
            case 30:
                canShoot = 3;
                break;
            default:
                break;
        }
        if (Health <= 0)
        {
            Die();
        }

    }

    private void TakeSaws()
    {

        if (come && !die)
        {
            saws[1].transform.position = Vector3.LerpUnclamped(saws[1].transform.position, new Vector3(transform.position.x - 1.8f, transform.position.y - 0.5f), 0.024f);
            saws[0].transform.position = Vector3.LerpUnclamped(saws[0].transform.position, new Vector3(transform.position.x + 1.8f, transform.position.y - 0.5f), 0.024f);
            sawsTimer -= 0.05f;
        }
        if (sawsTimer <= 0 || die)
        {
            saws[1].transform.position = Vector3.LerpUnclamped(saws[1].transform.position, new Vector3(20f, 10f), 0.024f);
            saws[0].transform.position = Vector3.LerpUnclamped(saws[0].transform.position, new Vector3(-2f, 10f), 0.024f);

            come = false;
        }
        if (saws[0].transform.position.y >= 9f && !come)
        {
            sawsTimer += 0.05f;

        }

        if ((int)sawsTimer >= Health)
        {
            come = true;
            sawsTimer = 100f - Health * 1.05f;
        }
    }

    private void PrepareShot()
    {
        if (canShoot > 0)
        {
            State = GolemState.rocket;

            Invoke("DoShot", 0.12f);
        }


    }

    private void DoShot()
    {
        switch (canShoot)
        {
            case 0: break;
            case 1:
                animator.Play("shoot", 0, 1f);
                GetComponent<AudioSource>().clip = rocket;
                GetComponent<AudioSource>().Play();
                Vector3 position = transform.position + Vector3.down * 1.2f + (sprite.flipX ? transform.right * missileDistance : transform.right * (-missileDistance));

                Missile newMissile = Instantiate(missile, position, missile.transform.rotation) as Missile;

                newMissile.Parent = gameObject;
                newMissile.Direction = newMissile.transform.right * (sprite.flipX ? 1f : -1f);
                break;
            case 2:
                animator.Play("shoot", 0, 1f);
                position = transform.position + (sprite.flipX ? transform.right * missileDistance : transform.right * (-missileDistance));

                newMissile = Instantiate(missile, position, missile.transform.rotation) as Missile;

                newMissile.Parent = gameObject;
                newMissile.Direction = newMissile.transform.right * (sprite.flipX ? 1f : -1f);
                goto case 1;

            case 3:
                animator.Play("shoot", 0, 1f);
                position = transform.position + Vector3.up * 1.2f + (sprite.flipX ? transform.right * missileDistance : transform.right * (-missileDistance));

                newMissile = Instantiate(missile, position, missile.transform.rotation) as Missile;

                newMissile.Parent = gameObject;
                newMissile.Direction = newMissile.transform.right * (sprite.flipX ? 1f : -1f);
                goto case 2;
            default:
                break;
        }
    }

    private void GenRndPos()
    {
        while (nextPos == transform.position)
        {
            nextPos = positions[(int)UnityEngine.Random.Range(0, 5)];
        }
    }

    protected override void Die()
    {
        die = true;
        State = GolemState.attack;
        GetComponent<AudioSource>().clip = defeat;
        GetComponent<AudioSource>().Play();
        rigidbody = null;
        Invoke("Destroy", 1f);
    }

    private void Destroy()
    {
        State = GolemState.die;
        Destroy(gameObject, 1.36f);
    }
}
