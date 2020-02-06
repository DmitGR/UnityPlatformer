using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Bonus {

    CompleteLevel level;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        EndLevel(collision.GetComponent<Character>());
        base.OnTriggerEnter2D(collision);
    }

    private void EndLevel(Character unit)
    {
        level = FindObjectOfType<CompleteLevel>();
        level.Completed();
    }
}
