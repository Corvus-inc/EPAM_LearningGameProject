using System;

public interface ISpawningWithHealth
{
    event Action<IHaveHealth> spawnedObject;
}