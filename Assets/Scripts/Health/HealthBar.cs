using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IHealthBar
{
    public IHealthSystem HealthSystem { get; set; }
    
    [SerializeField] private RectTransform _bar; 
    private Transform canvasPosition;
    
    private float _startHealthRectWidth;

    private void Awake()
    {
        _startHealthRectWidth = _bar.rect.width;
    }

    private void Start()
    {
        HealthSystem_OnOnHealthChanged(HealthSystem, EventArgs.Empty);
        HealthSystem.OnHealthChanged += HealthSystem_OnOnHealthChanged;
    }

    public void SetSize(float sizeNormalized)
    {
        _bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeNormalized* _startHealthRectWidth);
    }
    
    public void SetColour(Color color)
    {
        _bar.GetComponent<Image>().color = color;
    }

    private void HealthSystem_OnOnHealthChanged(object sender, System.EventArgs e)
    {
        SetSize(HealthSystem.GetHealthPercent());
    }
}