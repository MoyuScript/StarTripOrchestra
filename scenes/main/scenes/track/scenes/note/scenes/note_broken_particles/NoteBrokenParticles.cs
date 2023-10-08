using Godot;
using System;

public partial class NoteBrokenParticles : GpuParticles3D
{
	Color _particleColor = new Color(1.0f, 1.0f, 0.61f);
	[Export(PropertyHint.ColorNoAlpha)] public Color ParticleColor
	{
		get => _particleColor;
		set
		{
			_particleColor = value;
			if (IsNodeReady())
			{
				SetColor(_particleColor);
			}
		}
	}

	float _velocity = 4.0f;
	[Export] public float Velocity
	{
		get => _velocity;
		set
		{
			_velocity = value;
			if (IsNodeReady())
			{
				SetVelocity(_velocity);
			}
		}
	}

	public override void _Ready()
	{
		var material = (ParticleProcessMaterial)ProcessMaterial.Duplicate();
		ProcessMaterial = material;
		SetColor(ParticleColor);
		SetVelocity(Velocity);
	}

	void SetColor(Color color)
	{
		var material = (ParticleProcessMaterial)ProcessMaterial;
		material.Color = color;
	}

	void SetVelocity(float velocity)
	{
		var material = (ParticleProcessMaterial)ProcessMaterial;
		material.InitialVelocityMin = material.InitialVelocityMax = velocity;
	}
	public void Quit()
	{
		GetNode<AnimationPlayer>("AnimationPlayer").Play("quit");
	}
}
