using System;

public interface ICurrency
{
    public event Action<int> Changed;

    public int Money { get; }

    public void Add(int value);
    public bool TrySpend(int value);
}