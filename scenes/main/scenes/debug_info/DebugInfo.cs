using Godot;
using System;

public partial class DebugInfo : Control
{
	public override void _Ready()
	{
		GlobalState.Singleton.IsDebugInfoVisibleChanged += OnIsDebugInfoVisibleChanged;
		OnIsDebugInfoVisibleChanged();
	}

	void OnIsDebugInfoVisibleChanged()
	{
		Visible = GlobalState.Singleton.IsDebugInfoVisible;
	}
}
