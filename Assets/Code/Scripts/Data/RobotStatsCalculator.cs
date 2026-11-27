public class RobotStatsCalculator
{
    private const float HalfCoefficient = 0.5f;
    private const float UpgradePriceCoefficent = 0.85f;

    private readonly RobotData _data;

    public RobotStatsCalculator(RobotData data)
    {
        _data = data;
    }

    public float GetHealth() => GetValue(_data.StartHealth);

    public float GetDamage() => GetValue(_data.StartDamage);

    public float GetSellingPrice() => GetValue(_data.StartPrice);

    public float GetTotalPrice() => GetValue(_data.StartPrice, UpgradePriceCoefficent);

    private float GetValue(float stat, float coefficent = HalfCoefficient) => stat * coefficent * _data.RobotLevel;
}