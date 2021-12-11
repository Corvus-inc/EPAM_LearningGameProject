using UnityEngine;

public interface IHaveHealth
{
    IHealthSystem MyHealthSystem { get; }
    GameObject GetHealthOwner();

}