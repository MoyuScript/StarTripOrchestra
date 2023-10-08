using Godot;
using System;

public partial class HitLight : SpotLight3D
{
	public void FadeOut()
	{
		GetNode<AnimationPlayer>("AnimationPlayer").Play("fade_out");
	}
}
