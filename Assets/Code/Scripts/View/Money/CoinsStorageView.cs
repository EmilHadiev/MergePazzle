using Zenject;

public class CoinsStorageView : CurrencyStorageView
{
    [Inject]
    public void Construct(ICoinsStorage coinsStorage)
    {
        Currency = coinsStorage;
    }
}