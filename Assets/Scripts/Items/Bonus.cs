using UnityEngine;

public class Bonus : MonoBehaviour {

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Open", false);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Character unit = collision.GetComponent<Character>();

        if (unit)
        {
            animator.SetBool("Open", true);
        }
    }
}
