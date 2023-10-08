using Godot;
using System;

namespace GameMenu;

public partial class Menu : Control
{
	GlobalState _globalState;

	public override void _Ready()
	{
		_globalState = GetNode<GlobalState>("/root/GlobalState");
		_globalState.IsMenuVisibleChanged += OnIsMenuVisibleChanged;
	}

	void OnIsMenuVisibleChanged()
	{
		Visible = _globalState.IsMenuVisible;
	}
}
