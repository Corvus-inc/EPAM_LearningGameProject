using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HealthSystem HealthSystem { private get; set; }
    
    [SerializeField] private RectTransform _bar;
    
    private float _startHealthRectWidth; 

    void Awake()
    {
        _startHealthRectWidth = _bar.rect.width;
        //if(_bar)  _bar = transform.Find("Bar");
    }

    private void Start()
    {
        HealthSystem_OnOnHealthChanged(HealthSystem, EventArgs.Empty);
        HealthSystem.OnHealthChanged += HealthSystem_OnOnHealthChanged;
    }

    private void HealthSystem_OnOnHealthChanged(object sender, System.EventArgs e)
    {
        SetSize(HealthSystem.GetHealthPercent());
    }

    public void SetSize(float sizeNormalized)
    {
        _bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeNormalized* _startHealthRectWidth);

        //_bar.localScale = new Vector3(sizeNormalized, _bar.localScale.y, _bar.localScale.z); // Why Bar on Canvas state invisible when Scale Z=0 !!!
    }
    
    public void SetColour(Color color)
    {
        _bar.GetComponent<Image>().color = color;
    }
}
