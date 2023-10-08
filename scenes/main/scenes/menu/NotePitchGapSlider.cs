using Godot;
using System;

public partial class NotePitchGapSlider : HSlider
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ValueChanged += OnValueChanged;
		GlobalState.Singleton.NotePitchGapChanged += OnGlobalStateChanged;
		
		OnGlobalStateChanged();
	}

	void OnGlobalStateChanged()
	{
		Value = GlobalState.Singleton.NotePitchGap;
		GetNode<Label>("../ValueDisplayer").Text = Value.ToString("0.0");
	}

	void OnValueChanged(double value)
	{
		GlobalState.Singleton.NotePitchGap = (float)value;
		GetNode<Label>("../ValueDisplayer").Text = value.ToString("0.0");
	}
}
