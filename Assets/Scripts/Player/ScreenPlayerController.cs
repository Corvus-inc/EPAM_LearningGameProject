﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject Player;//!warning

    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private RawImage IconWeapon;
    [SerializeField] private TMP_Text Clip;

    private void Start()
    {
        HealthSystem _healthSystem = Player.GetComponent<MyCharacterController>().GetHealthSystem();//!warning

        _healthBar.Setup(_healthSystem);//how to link system ref //!warning
        _healthBar.SetColour(Color.green);
    }
}
