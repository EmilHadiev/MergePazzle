using System;

public interface IRobotDeleter
{
    event Action Deleting;

    void Delete();
}