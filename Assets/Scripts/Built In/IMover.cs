using System;

public interface IMover
{
    Speed MoveSpeed { get; set; }
    Speed SprintSpeed { get; set; }
}
public delegate float Speed();