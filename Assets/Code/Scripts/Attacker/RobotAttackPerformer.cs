using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class RobotAttackPerformer : MonoBehaviour, IAttackPerformer
{
    private RobotData _data;
    private ICharacterAnimator _animator;

    private bool _isLock;
    private bool _isAttackReady;
    private float _currentTime;

    private void Start()
    {
        IRobot robot = GetComponent<IRobot>();
        _data = robot.Data;
        _animator = robot.Animator;
        StopAttacking();
    }

    public void StartAttacking()
    {
        _isLock = false;
        _isAttackReady = true;
        ResetTime();
    }

    public void StopAttacking()
    {
        _isLock = true;
        _isAttackReady = false;

        _animator.StopAttacking();
    }

    private void Update()
    {
        if (_isLock)
            return;

        if (_isAttackReady)
        {
            PerformAttack();
            _isAttackReady = false;
        }

        _currentTime += Time.deltaTime;

        if (_currentTime >= _data.StartAttackSpeed)
        {
            _currentTime = 0;
            _isAttackReady = true;
        }
    }

    private void ResetTime()
    {
        _currentTime = 0;
    }

    private void PerformAttack()
    {
        _animator.StartAttacking();
        Debug.Log("Атакую - атака готова!");
    }
}