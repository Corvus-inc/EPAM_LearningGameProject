using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] private GameObject Player;//!warning

    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private RawImage IconWeapon;
    [SerializeField] private TMP_Text Clip;

    private void Start()
    {
        _healthBar.SetColour(Color.green);
    }
}
