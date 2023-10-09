using Godot;
using MoyuMidi;
using System;

public partial class Note : CharacterBody3D
{
	Color _noteColor = new(1.0f, 0.99f, 0.6f);
	[Export(PropertyHint.ColorNoAlpha)]
	public Color NoteColor
	{
		get => _noteColor;
		set
		{
			_noteColor = value;
			if (IsNodeReady())
			{
				SetColor(_noteColor);
			}
		}
	}

	float _emissionEnergy = 1.0f;
	[Export] public float EmissionEnergy
	{
		get => _emissionEnergy;
		set
		{
			_emissionEnergy = value;
			if (IsNodeReady())
			{
				SetEmissionEnergy(value);
			}
		}
	}
	[Export] public PackedScene NoteBrokenParticlesScene;
	[Export] public PackedScene HitLightScene;

	public MidiEvent NoteOnEvent;
	public MidiEvent NoteOffEvent;
	NoteBrokenParticles _brokenParticles;
	HitLight _hitLight;

	bool _isDisappearing = false;

	void SetColor(Color color)
	{
		ShaderMaterial material = (GetNode<MeshInstance3D>("MeshInstance3D").Mesh as BoxMesh).Material as ShaderMaterial;
		Vector4 vector4Color = new Vector4(color.R, color.G, color.B, 1.0f);
		material.SetShaderParameter("albedo", vector4Color);
		material.SetShaderParameter("emission", vector4Color);
	}

	void SetEmissionEnergy(float emissionEnergy)
	{
		ShaderMaterial material = (GetNode<MeshInstance3D>("MeshInstance3D")?.Mesh as BoxMesh)?.Material as ShaderMaterial;
		material?.SetShaderParameter("emission_energy", emissionEnergy);
	}

	public override void _Ready()
	{
		// 复制一份材质，用于更换颜色
		var meshInstance3D = GetNode<MeshInstance3D>("MeshInstance3D");
		BoxMesh mesh = (BoxMesh)meshInstance3D.Mesh.Duplicate(true);
		meshInstance3D.Mesh = mesh;
		SetColor(NoteColor);
		SetEmissionEnergy(EmissionEnergy);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!GlobalState.Singleton.IsPlaying)
		{
			return;
		}
		if (_isDisappearing)
		{
			return;
		}
		float moveDelta = GlobalState.Singleton.NoteMetersPerSeconds * (float)delta;
		KinematicCollision3D collision = MoveAndCollide(new Vector3(-moveDelta, 0, 0));

		if (collision is not null)
		{
			for (int i = 0; i < collision.GetCollisionCount(); i++)
			{
				if (collision.GetCollider(i) is HitFace)
				{
					StartDisappear();
					break;
				}
			}
		}
	}

	public override void _Process(double delta)
	{
		if (!GlobalState.Singleton.IsPlaying)
		{
			return;
		}
		
		if (_isDisappearing)
		{
			if (Scale.X <= 0.1f)
			{
				StopDisappear();
				SetProcess(false);
				SetPhysicsProcess(false);
				// QueueFree();
				Visible = false;
				return;
			}
			Scale = Scale with
			{
				X = Scale.X - GlobalState.Singleton.NoteMetersPerSeconds * (float)delta
			};
		}
	}
	public void StartDisappear()
	{
		if (_isDisappearing)
		{
			return;
		}
		_isDisappearing = true;

		_brokenParticles = NoteBrokenParticlesScene.Instantiate<NoteBrokenParticles>();
		_brokenParticles.Position = Position;
		_brokenParticles.ParticleColor = NoteColor;
		_brokenParticles.Velocity = GlobalState.Singleton.NoteMetersPerSeconds;
		AddSibling(_brokenParticles);

		_hitLight = HitLightScene.Instantiate<HitLight>();
		_hitLight.Position = Position + Vector3.Right * 3.0f;
		_hitLight.LightColor = NoteColor;
		AddSibling(_hitLight);

		var animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("glow");
	}

	public void StopDisappear()
	{
		if (!_isDisappearing)
		{
			return;
		}
		_isDisappearing = false;
		_brokenParticles.Quit();

		_hitLight.FadeOut();
	}
}
