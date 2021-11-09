
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMask : MonoBehaviour
{
    public float TimeForMasked { private get; set; }

    [SerializeField] private Image IconMask;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateCoolDown(TimeForMasked);
    }
    private void UpdateCoolDown(float timeCoolDown)
    {
        if (Math.Abs(IconMask.fillAmount - 1) < float.Epsilon) return;
        IconMask.fillAmount = Mathf.MoveTowards(IconMask.fillAmount, 1f, Time.deltaTime/timeCoolDown);
    }

    private IEnumerator FillAmountZero()
    {
        yield return new WaitForSeconds(TimeForMasked);
        IconMask.fillAmount = 0;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(FillAmountZero());
    }
}
