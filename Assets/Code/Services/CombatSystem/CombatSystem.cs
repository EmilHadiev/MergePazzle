using System.Collections;
using UnityEngine;

public class CombatSystem : ICombatSystem
{
    private readonly IRobotContainer _robotContainer;

    public CombatSystem(IRobotContainer robotContainer)
    {
        _robotContainer = robotContainer;
    }

    public void StartCombat()
    {
        foreach (var robot in _robotContainer.Robots)
        {
            robot.StateMachine.SwitchTo<RobotMoveState>();
        }
    }
}