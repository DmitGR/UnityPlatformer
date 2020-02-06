using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character unit = collider.GetComponent<Character>();

        if(unit)
        {
            unit.ReceiveDamage();
        }
    }
}
