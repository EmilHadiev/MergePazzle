using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerInputView : MonoBehaviour
{
    [SerializeField] private Button _robotStartMoveBtn;

    [Inject] private readonly ICombatSystem _combatSystem;

    private void OnEnable()
    {
        _robotStartMoveBtn.onClick.AddListener(StartMove);
    }

    private void OnDisable()
    {
        _robotStartMoveBtn.onClick.RemoveListener(StartMove);
    }

    private void StartMove()
    {
        _combatSystem.StartCombat();
    }
}