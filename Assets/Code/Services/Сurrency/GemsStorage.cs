using System;

public class GemsStorage : ICurrency
{
    public int Money { get; private set; }

    public event Action<int> Changed;

    public void Add(int value)
    {
        CheckCorrectValue(value);

        Money += value;
        Changed?.Invoke(Money);
    }

    public bool TrySpend(int value)
    {
        CheckCorrectValue(value);

        if (Money - value >= 0)
        {
            Changed?.Invoke(Money);
            return true;
        }

        return false;
    }

    private void CheckCorrectValue(int value)
    {
        if (value < 0)
            throw new System.ArgumentException($"{nameof(value)} less than 0");
    }
}
