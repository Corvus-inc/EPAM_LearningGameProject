using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private RectTransform _bar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
            SetSize( 2);
    }
    public void SetSize(float sizeNormalized)
    {
        _bar.localScale = new Vector3(sizeNormalized, 1f);
    }

}
