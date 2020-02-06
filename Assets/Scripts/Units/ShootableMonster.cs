using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonster : MoveableMonster {

    [SerializeField]
    [Range (2,4)]
    private float rate;

    [SerializeField]
    private AudioClip shoot;

    private Missile missile;

    private const float missileDistance = 1.6f;

    protected override void Awake()
    {
        rate = Random.Range(2f, 5f);
        missile = Resources.Load<Missile>("Missile");
        base.Awake();
    }

    protected override void Start()
    {
        InvokeRepeating("Shoot", rate, rate);
        gettingScore = 60;
    }

    private void Shoot()
    {
        animator.Play("shoot", 0, 1f);

        Vector3 position = transform.position + (sprite.flipX ? transform.right* missileDistance : transform.right*(-missileDistance));

        Missile newMissile = Instantiate(missile, position, missile.transform.rotation) as Missile;

        newMissile.Parent = gameObject;
        newMissile.Direction = newMissile.transform.right*(sprite.flipX?1f:-1f);

        GetComponent<AudioSource>().clip = shoot;
        GetComponent<AudioSource>().Play();
    }

}
