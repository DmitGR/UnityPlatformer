using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{


    private const float collRadius = 0.16f;


    [SerializeField]
    private float speed = 3.0f;

    [SerializeField]
    private int lives;
    private int coins;

    public int Coins
    {
        get { return coins; }
        set { coins = value; coinCounter.Refresh(); }
    }

    private int stars;
    public int Stars
    {
        get
        { return stars; }

        set
        {
            stars = value;
            starCounter.Refresh();
        }
    }
    public int Lives
    {
        get { return lives; }
        set { if (value <= 5) lives = value; healthBar.Refresh(); }
    }

    private int score;
    public int Score
    {
        get {return score;}
        set
        {
            score = value;
            if(scoreInfo != null)
            scoreInfo.Refresh();
        }
    }

    public Vector3 Checkpoint { get; set; }

    HealthBar healthBar;
    ManaBar manaBar;
    CoinCounter coinCounter;
    StarCounter starCounter;
    Score scoreInfo;

    [SerializeField]
    [Range(1, 15)]
    private float jumpForce;

    [SerializeField]
    private float fallMultiplier = 2.5f;

    [SerializeField]
    private float lowJumpMultiplier = 2f;

    [SerializeField]
    private float bulletValue = 0.25f;

    [SerializeField]
    private float maxFallSpeed = 2f;

    [SerializeField]
    private AudioClip jump;

    [SerializeField]
    private AudioClip shoot;

    [SerializeField]
    private AudioClip damage;

    [SerializeField]
    private AudioClip fall;

    private bool isGrounded = false;

    private Bullet bullet;

    private float damageTimer;
    private const float timeForDamage = 3.6f;
    private const float jumpForceUp = 5f;
    private const float jumpForceOut = 12f;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
    protected override void Awake()
    {
        lives = PlayerPrefs.GetInt("HP");

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        healthBar = FindObjectOfType<HealthBar>();
        manaBar = FindObjectOfType<ManaBar>();
        starCounter = FindObjectOfType<StarCounter>();
        bullet = Resources.Load<Bullet>("Bullet");
        coinCounter = FindObjectOfType<CoinCounter>();
        score = 0;
        scoreInfo = FindObjectOfType<Score>();
        damageTimer = 0;
        stars = 0;
    }

    private void FixedUpdate()
    {
        CheckGround();
        if (transform.position.y < -jumpForceOut)  // under LEVEL
            FallUnderground();

        if (rigidbody.velocity.y < 0)
            rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (rigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
            rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        if (rigidbody.velocity.y > maxFallSpeed * jumpForce)
            rigidbody.velocity -= Vector2.up * maxFallSpeed*jumpForce;
        if(damageTimer > 0)
        damageTimer -= 0.1f;
    }

    private void FallUnderground()
    {
        lives--;
        rigidbody.velocity = Vector2.zero;
        transform.position = Checkpoint;

        GetComponent<AudioSource>().clip = fall;
        PlaySound();
    }

    protected override void Update()
    {
        if (damageTimer > 0)
            State = CharState.Damage;
        else
        {
            if (!isGrounded) State = CharState.Fall;
            else State = CharState.Idle;
            if (Input.GetButton("Horizontal")) Run();
            if (isGrounded && Input.GetButtonDown("Jump")) Jump();
            if (Input.GetButtonDown("Fire1")) Shoot();

        }

        if (damageTimer == timeForDamage && lives > 0)
        {
            GetComponent<AudioSource>().clip = damage;
            PlaySound();
        }

    }

    private void Shoot()
    {
        if (manaBar.CurrentValue - bulletValue > 0)
        {
            manaBar.CurrentValue -= bulletValue;
            Vector3 position = transform.position;
            position.x += (sprite.flipX ? -1.0f : 1.0f);
            Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation);

            newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0f : 1.0f);
            newBullet.Parent = gameObject;

            GetComponent<AudioSource>().clip = shoot;
            PlaySound();
        }
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0;

        if (isGrounded)
            State = CharState.Run;
    }

    private void Jump()
    {
        GetComponent<AudioSource>().clip = jump;
        PlaySound();
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        State = CharState.Jump;

    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position - transform.up / 2, collRadius);

        isGrounded = colliders.Length > 1;
    }

    void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }

    public override void ReceiveDamage()
    {
        if (damageTimer <= 0)
        {
            Lives--;
            ForceJumpOut();
            Debug.Log("Clount of Lives: " + lives);
            damageTimer = timeForDamage;
        }
    }

    public void ForceJumpOut()
    {
        ForceJumpUp();
        rigidbody.AddForce((sprite.flipX ? Vector2.right : Vector2.left) * jumpForceOut, ForceMode2D.Impulse);
    }

    public void ForceJumpUp()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(transform.up * jumpForceOut, ForceMode2D.Impulse);
    }

    public void ForceJumpOut(Vector3 direction)
    {
        ForceJumpUp();
        rigidbody.AddForce((direction) * jumpForceOut, ForceMode2D.Impulse);
    }
}

public enum CharState
{
    Idle, Run, Jump, Fall, Damage
}
