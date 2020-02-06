using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour {

    [SerializeField]
    Slider slider;
   // Use this for initialization
    void Start () {
        slider.value = AudioListener.volume;
    }
	
	// Update is called once per frame
	void Update () {
        AudioListener.volume = slider.value;
	}
}
