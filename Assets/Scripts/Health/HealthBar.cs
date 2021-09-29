using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Transform _bar;
    private Camera _eventCamera;
    private HealthSystem _healthSystem;

    void Awake()
    {
        _eventCamera = gameObject.GetComponent<Canvas>().worldCamera;//how correctly make  ref
        _bar = transform.Find("Bar");
    }

    private void LateUpdate()
    {
        transform.LookAt(_eventCamera.transform);
        transform.Rotate(0, 180, 0);
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
        _bar.localScale = new Vector3(sizeNormalized, 1f);
    }
    
    public void SetColour(Color color)
    {
        _bar.GetComponent<Image>().color = color;
    }
}
