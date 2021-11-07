using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtils
{
    public class Utils : MonoBehaviour
    {
        public static bool CheckLayerMask(GameObject obj, LayerMask layers)
        {
            if (((1 << obj.layer) & layers) != 0)
            {
                return true;
            }

            return false;
        }
        public static Vector3 GetRandomDir()
        {
            return new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized;
        }
    }
}