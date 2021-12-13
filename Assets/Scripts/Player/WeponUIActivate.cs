using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponUIActivate : MonoBehaviour
{
    
    void Start()
    {
        gameObject.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        if (gameObject.transform.localScale.x >= Vector3.one.x)
        {
            Destroy(gameObject);
        }
    }
}
