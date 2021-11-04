using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMask : MonoBehaviour
{
    public float TimeForMasked { private get; set; }

    [SerializeField] private Image IconMask;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCoolDown(TimeForMasked);
    }
    private void UpdateCoolDown(float timeCoolDown)
    {
        IconMask.fillAmount = Mathf.MoveTowards(IconMask.fillAmount, 1f, Time.deltaTime/timeCoolDown);
    }
}
