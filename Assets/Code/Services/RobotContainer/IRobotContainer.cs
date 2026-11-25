using System.Collections.Generic;

public interface IRobotContainer
{
    IReadOnlyList<IRobot> Robots { get; }

    public void Add(IRobot robot);
    public void Remove(IRobot robot);
}