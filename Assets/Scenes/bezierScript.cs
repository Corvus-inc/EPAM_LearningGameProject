using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bezierScript : MonoBehaviour
{
    float t = 0f;

    Vector2 P1 = new Vector2(0f, 0f);
    Vector2 P2 = new Vector2(0.5f, 1f);
    Vector2 P3 = new Vector2(1f, 0f);


    void Start()
    {
        Debug.DrawLine(P1, P2, Color.black, 1000f);
        Debug.DrawLine(P2, P3, Color.black, 1000f);

        Vector2 P1P2 = new Vector2(P2.x - P1.x, P2.y - P1.y);
        Vector2 P2P3 = new Vector2(P3.x - P2.x, P3.y - P2.y);

        Vector2 LastBezieV = P1;
        Vector2 V1;
        Vector2 V2;
        while ( !(t > 1) )
        {
           V1 = P1 + P1P2 * t;
           V2 = P2 + P2P3 * t;

           Vector2 V1V2 = new Vector2(V2.x - V1.x, V2.y - V1.y);
           Vector2 BezieV = V1 + V1V2 * t;

            Debug.DrawLine(LastBezieV, BezieV, Color.red, 1000f);

            LastBezieV = BezieV;
            t += 0.05f;
        }
        
    }
}
