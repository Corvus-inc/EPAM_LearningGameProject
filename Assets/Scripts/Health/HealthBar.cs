using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform _bar;
    private HealthSystem _healthSystem;


    void Awake()
    {
        //if(_bar)  _bar = transform.Find("Bar");
    }
    public void Setup(HealthSystem healthSystem)
    {
        _healthSystem = healthSystem;
        healthSystem.OnHealthChanged += HealthSystem_OnOnHealthChanged;
    }

    private void HealthSystem_OnOnHealthChanged(object sender, System.EventArgs e)
    {
        SetSize(_healthSystem.GetHealthPrecent());
    }

    public void SetSize(float sizeNormalized)
    {
        //_bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeNormalized);

        _bar.localScale = new Vector3(sizeNormalized, 1f, 1f); // Why Bar on Canvas state invisible when Scale Z=0 !!!
    }
    
    public void SetColour(Color color)
    {
        _bar.GetComponent<Image>().color = color;
    }
}
