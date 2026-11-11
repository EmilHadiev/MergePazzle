using System;

public interface IRobotUpgrader
{
    event Action Upgraded;

    void LevelUp();
}