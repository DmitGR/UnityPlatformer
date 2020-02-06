using UnityEngine;

public class LandMonster : Unit
{
    [Range(1, 3)]
    private int level;

    [SerializeField]
    private GameObject[] fires;

    [SerializeField]
    private AudioClip[] hits;
    private int hit;

    private int hp;


    private float timer;

    private const float angle = 1280f;
    protected const float damageTime = 0.4f;

    private const int min = 1;
    private const int max = 4;
    protected int gettingScore;

    public int Hp
    {
        get
        {
            if (hp > 0)
                return hp;
            else
                return 0;
        }

        set
        {
            if(value >= 0)
            hp = value;
        }
    }

    protected override void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        var chance = UnityEngine.Random.value;
        if (chance <= 0.5f)
            level = 1;
        else if (chance > 0.5f && chance <= 0.75f)
            level = 2;
        else level = 3;

        Hp = level;
        hit = 0;

        gettingScore = 25;

        foreach (var item in fires)
            item.SetActive(false);

        for (int i = 0; i < level; i++)
            fires[i].SetActive(true);
        animator.SetBool("Hit", false);
        animator.SetInteger("Level", level);
    }

    protected override void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            animator.SetBool("Hit", true);
        }
        else
        {
            animator.SetBool("Hit", false);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Character unit = collision.GetComponent<Character>();

        if (unit)
            unit.ReceiveDamage();
    }

    protected override void Die()
    {
         if(animator.GetBool("Hit"))
        rigidbody.MoveRotation(-Time.deltaTime * angle);
        gameObject.GetComponent<BoxCollider2D>().size = Vector2.zero;
        Destroy(gameObject, damageTime);

        GetScore();
    }

    protected virtual void GetScore()
    {
        FindObjectOfType<Character>().Score += gettingScore * level;
    }

    public override void ReceiveDamage()
    {
        animator.SetBool("Hit", true);
        Hp--;
        timer = damageTime;
        
        fires[Hp].SetActive(false);
        PlaySound();
        if (Hp <= 0)
            Die();
    }

    private void PlaySound()
    {
        if (hit < hits.Length)
        {
            GetComponent<AudioSource>().clip = hits[hit];
            GetComponent<AudioSource>().Play();
            hit++;
        }
    }
}
