using Godot;
using System;

public partial class TrackList : Node3D
{
	[Export] public PackedScene TrackScene;

	float _trackGap = 1.0f;
	[Export] public float TrackGap
	{
		get => _trackGap;
		set
		{
			_trackGap = value;
		}
	}
	public override void _Ready()
	{
		GlobalState.Singleton.MidiTrackToMenuTrackConfigNodeChanged += OnMidiTrackToMenuTrackConfigNodeChanged;
	}

	void OnMidiTrackToMenuTrackConfigNodeChanged()
	{
		// TODO: 优化
		var midiFile = GlobalState.Singleton.MidiFile;

		foreach (var child in GetChildren())
		{
			child.QueueFree();
		}

		int index = 0;
		foreach (var track in midiFile)
		{
			Track trackNode = TrackScene.Instantiate<Track>();
			AddChild(trackNode);
			trackNode.MidiTrack = track;
			trackNode.Position = new Vector3(0, TrackGap, 0) * index++;
		}
	}

}
