using System.Collections.Generic;
using UnityEngine;

public class RobotContainer : IRobotContainer
{
    private readonly List<IRobot> _robots;

    public IReadOnlyList<IRobot> Robots => _robots;

    public RobotContainer()
    {
        _robots = new List<IRobot>(10);
    }

    public void Add(IRobot robot)
    {
        _robots.Add(robot);
    }

    public void Remove(IRobot robot)
    {
        _robots.Remove(robot);
    }
}