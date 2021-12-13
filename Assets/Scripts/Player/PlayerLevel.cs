using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    public event Action Unlock2lvl;
    public float Experience
    {
        get => _currentExp;
        set
        {
            _currentExp += value;
            experience.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Experience+ _startHealthRectWidth);
            if (_currentExp >= _needExp)
            {
                Unlock2lvl?.Invoke();
                _currentExp = 0;
                experience.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,  0);
                _currentLvl += 1;
                level.text = _currentLvl.ToString();
            }
        }
    }
    
    [SerializeField] private TMP_Text level;
    [SerializeField] private RectTransform experience;

    private int _currentLvl = 1;
    private float _currentExp;
    private float _needExp = 300;
    private float _startHealthRectWidth;
    
    private void Awake()
    {
        _startHealthRectWidth = experience.rect.width;
    }
}
