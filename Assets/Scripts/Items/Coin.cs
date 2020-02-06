using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{

    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Character>())
        {
            collision.GetComponent<Character>().Score += 10;
            collision.GetComponent<Character>().Coins += (PlayerPrefs.GetInt("CoinUp"));
            GetComponent<AudioSource>().Play();
        }
            base.OnTriggerEnter2D(collision);   
    }
}
