using Godot;
using System;

public partial class WindowController : Node
{
	public override void _Ready()
	{
		GlobalState.Singleton.IsFullScreenChanged += OnFullScreenChanged;
	}

	void OnFullScreenChanged()
	{
		var window = GetWindow();

		if (GlobalState.Singleton.IsFullScreen)
		{
			window.Mode = Window.ModeEnum.ExclusiveFullscreen;
		}
		else
		{
			window.Mode = Window.ModeEnum.Windowed;
		}
	}
}
