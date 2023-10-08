using Godot;
using System;

public partial class TrackMovementSlider : HSlider
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ValueChanged += OnValueChanged;
		GlobalState.Singleton.TrackMovementChanged += OnGlobalStateChanged;
		
		OnGlobalStateChanged();
	}

	void OnGlobalStateChanged()
	{
		Value = GlobalState.Singleton.TrackMovement;
		GetNode<Label>("../ValueDisplayer").Text = Value.ToString("0.0");
	}

	void OnValueChanged(double value)
	{
		GlobalState.Singleton.TrackMovement = (float)value;
		GetNode<Label>("../ValueDisplayer").Text = value.ToString("0.0");
	}
}
