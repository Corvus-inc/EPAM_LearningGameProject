using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public abstract class BaseHaveHealth : MonoBehaviour, IHaveHealth 
 {
     public IHealthSystem MyHealthSystem { get; protected set; }
     public GameObject GetHealthOwner()
     {
         return gameObject;
     }
 }
