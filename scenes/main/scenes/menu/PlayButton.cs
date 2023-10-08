using Godot;
using System;

public partial class PlayButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += () => {
			GlobalSignal.Singleton.EmitSignal(GlobalSignal.SignalName.StartPlay);
		};
		GlobalState.Singleton.MidiFileChanged += UpdateDisabled;
		GlobalState.Singleton.AudioPathChanged += UpdateDisabled;
		UpdateDisabled();
	}

	void UpdateDisabled()
	{
		Disabled = GlobalState.Singleton.MidiFile is null || GlobalState.Singleton.AudioPath is null;
	}
}
