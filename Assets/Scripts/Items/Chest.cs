using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chest : Bonus
{
    private bool isOpened = false;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOpened)
        {
            GetStar(collision.GetComponent<Character>());
            GetComponent<AudioSource>().Play();
        }
        base.OnTriggerEnter2D(collision);
    }

    private void GetStar(Character unit)
    {
        unit.Score += 100;
        unit.Stars++;
        isOpened = true;
        unit.Checkpoint = transform.position;
    }
}
