using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPbar : MonoBehaviour {

    private Image hpline;

    //  private Character character;

    private const float maxFill = 1f;

    private float currentValue;

    public float CurrentValue
    {
        get { return currentValue; }
        set { if (value <= maxFill && value >= 0) currentValue = value; }
    }


    private void Awake()
    {
        hpline = GetComponent<Image>();
        currentValue = maxFill;
    }


    private void Update()
    {
        hpline.fillAmount = currentValue;
    }
}
