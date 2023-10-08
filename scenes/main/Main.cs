using Godot;
using System;

public partial class Main : Node3D
{
	public static Main Singleton;

	public override void _Ready()
	{
		Singleton = this;
		GlobalSignal.Singleton.StartPlay += OnStartPlay;
	}
	public static void Alert(string message)
	{
		var dialog = Singleton.GetNode<AcceptDialog>("AlertDialog");
		dialog.DialogText = message;
		dialog.Visible = true;
	}

	void OnStartPlay()
	{
		if (GlobalState.Singleton.MidiFile is null || GlobalState.Singleton.AudioPath is null)
		{
			return;
		}
		GlobalState.Singleton.NoteMoveSpeed = GlobalState.Singleton.NoteMetersPerSeconds;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("play"))
		{
			GlobalSignal.Singleton.EmitSignal(GlobalSignal.SignalName.StartPlay);
		}
	}
}
