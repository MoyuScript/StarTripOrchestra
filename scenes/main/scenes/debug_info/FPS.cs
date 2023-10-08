using Godot;
using System;

public partial class FPS : Label
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var fps = 1.0 / delta;
		Text = $"FPS: {fps:00.0}";
	}
}
