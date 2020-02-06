using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    
    private Character character;

    private void Awake()
    {
        character = FindObjectOfType<Character>();

        character.Checkpoint = character.transform.position = transform.position;
    }
}
