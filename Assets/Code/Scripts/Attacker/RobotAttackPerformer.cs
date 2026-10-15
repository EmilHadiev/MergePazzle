using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class RobotAttackPerformer : MonoBehaviour, IAttackPerformer
{
    private RobotData _data;
    private ICharacterAnimator _animator;

    private bool _isLocked;
    private float _attackTimer = 0f;

    private void Start()
    {
        IRobot robot = GetComponent<IRobot>();
        _data = robot.Data;
        _animator = robot.Animator;
        StopAttacking();
    }

    public void StartAttacking()
    {
        Debug.Log("Start attacking");
        _isLocked = false;
        _attackTimer = _data.StartAttackSpeed;
    }

    public void StopAttacking()
    {
        Debug.Log("Stop attacking");
        _isLocked = true;

        StopAttackPerform();
        ResetTimer();
    }

    private void Update()
    {
        if (_isLocked)
            return;

        _attackTimer += Time.deltaTime;

        if (_attackTimer >= _data.StartAttackSpeed)
        {
            PerformAttack();
            ResetTimer();
        }
        else
        {
            StopAttackPerform();
        }
    }

    private void ResetTimer()
    {
        _attackTimer = 0;
    }

    private void PerformAttack()
    {
        _animator.StartAttacking();
    }

    private void StopAttackPerform()
    {
        _animator.StopAttacking();
        Debug.Log("Stop attacking");
    }
}