public class RobotStatsCalculator
{
    private const float Coefficient = 0.5f;
    private readonly RobotData _data;

    public RobotStatsCalculator(RobotData data)
    {
        _data = data;
    }

    public float GetHealth() => GetStatsValue(_data.StartHealth);

    public float GetDamage() => GetStatsValue(_data.StartDamage);

    public float GetSellingPrice() => GetStatsValue(_data.StartPrice);

    private float GetStatsValue(float stat) => stat * Coefficient * _data.RobotLevel;
}