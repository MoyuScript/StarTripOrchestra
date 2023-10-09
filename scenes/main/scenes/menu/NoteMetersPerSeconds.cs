using Godot;
using System;

namespace GameMenu;

public partial class NoteMetersPerSeconds : HSlider
{
	public override void _Ready()
	{
		ValueChanged += OnValueChanged;
		GlobalState.Singleton.HasStartedPlayChanged += OnHasStartedPlayChanged;
		OnGlobalStateChanged();
	}

	void OnHasStartedPlayChanged()
	{
		Editable = !GlobalState.Singleton.HasStartedPlay;
	}

	void OnGlobalStateChanged()
	{
		Value = GlobalState.Singleton.NoteMetersPerSeconds;
		GetNode<Label>("../ValueDisplayer").Text = Value.ToString("0.0");
	}

	void OnValueChanged(double value)
	{
		GlobalState.Singleton.NoteMetersPerSeconds = (float)value;
		GetNode<Label>("../ValueDisplayer").Text = value.ToString("0.0");
	}
}
