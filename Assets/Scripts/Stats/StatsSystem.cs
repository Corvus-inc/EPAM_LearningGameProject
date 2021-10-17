
public class StatsSystem
{
    private readonly int _health;
    private readonly int _damage;
    private readonly float _speed;
    private readonly float _timeBoostSpeed;

    public int Health => _health;
    public int Damage => _damage;
    public float Speed => _damage;
    public float TimeBoostSpeedState => _damage;

    public StatsSystem(int health, int damage, float speed, float timeBoostSpeed)
    {
        _health = health;
        _damage = damage;
        _speed = speed;
        _timeBoostSpeed = timeBoostSpeed;
    }
/// <summary>
/// Health Damage Speed TimeBoostSpeedState
/// </summary>
/// <returns></returns>
    public override string ToString()
{
    var lineStats = $"{_health} {_damage} {_speed} {_timeBoostSpeed}";
    return lineStats;
}
}
