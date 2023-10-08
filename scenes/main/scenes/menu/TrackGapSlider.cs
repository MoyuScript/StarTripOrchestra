using Godot;
using System;

public partial class TrackGapSlider : HSlider
{
	public override void _Ready()
	{
		ValueChanged += OnValueChanged;
		GlobalState.Singleton.TrackGapChanged += OnGlobalStateChanged;
		
		OnGlobalStateChanged();
	}

	void OnGlobalStateChanged()
	{
		Value = GlobalState.Singleton.TrackGap;
		GetNode<Label>("../ValueDisplayer").Text = Value.ToString("0.0");
	}

	void OnValueChanged(double value)
	{
		GlobalState.Singleton.TrackGap = (float)value;
		GetNode<Label>("../ValueDisplayer").Text = value.ToString("0.0");
	}
}
