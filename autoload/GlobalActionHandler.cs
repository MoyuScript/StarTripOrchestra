using Godot;
using System;

public partial class GlobalActionHandler : Node
{
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("switch_menu"))
		{
			GlobalState.Singleton.IsMenuVisible = !GlobalState.Singleton.IsMenuVisible;
		}
		else if (Input.IsActionJustPressed("switch_fullscreen"))
		{
			GlobalState.Singleton.IsFullScreen = !GlobalState.Singleton.IsFullScreen;
		}
	}
}
