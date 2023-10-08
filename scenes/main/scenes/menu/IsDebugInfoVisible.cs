using Godot;
using System;

namespace GameMenu;
public partial class IsDebugInfoVisible : CheckButton
{
	public override void _Ready()
	{
		GlobalState.Singleton.IsDebugInfoVisibleChanged += OnIsDebugInfoVisibleChanged;
		Toggled += OnToggled;
		OnIsDebugInfoVisibleChanged();
	}

	void OnToggled(bool buttonPressed)
	{
		GlobalState.Singleton.IsDebugInfoVisibleChanged -= OnIsDebugInfoVisibleChanged;
		GlobalState.Singleton.IsDebugInfoVisible = buttonPressed;
		GlobalState.Singleton.IsDebugInfoVisibleChanged += OnIsDebugInfoVisibleChanged;
	}

	void OnIsDebugInfoVisibleChanged()
	{
		ButtonPressed = GlobalState.Singleton.IsDebugInfoVisible;
	}
}
