using Godot;
using System;

public partial class ResetButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += OnPressed;
	}

	void OnPressed()
	{
		GlobalState.Singleton.IsPlaying = false;
		GlobalState.Singleton.HasStartedPlay = false;
		GlobalSignal.Singleton.EmitSignal(GlobalSignal.SignalName.OnReset);
	}
}
