using Godot;
using System;

public partial class GlobalSignal : Node
{
    public static GlobalSignal Singleton;

    public override void _Ready()
    {
        Singleton = this;
    }

    [Signal] public delegate void StartPlayEventHandler();
}
