using Godot;
using System;

public partial class TrackList : Node3D
{
	[Export] public PackedScene TrackScene;

	
	public override void _Ready()
	{
		GlobalState.Singleton.MidiTrackToMenuTrackConfigNodeChanged += OnMidiTrackToMenuTrackConfigNodeChanged;
		GlobalState.Singleton.TrackMovementChanged += OnTrackMovementChanged;
		GlobalState.Singleton.TrackGapChanged += OnTrackGapChanged;
		GlobalSignal.Singleton.OnReset += GenerateTracks;
	}

	void OnReset()
	{
		GenerateTracks();
	}

	void OnTrackGapChanged()
	{
		int i = 0;
		foreach (Node node in GetChildren())
		{
			if (node is Track track)
			{
				track.Position = track.Position with {
					Y = i * GlobalState.Singleton.TrackGap,
				};
				i++;
			}
		}
	}
	
	void OnTrackMovementChanged()
	{
		Position = Position with
		{
			Z = GlobalState.Singleton.TrackMovement
		};
	}

	void GenerateTracks()
	{
		var midiFile = GlobalState.Singleton.MidiFile;

		if (midiFile is null)
		{
			return;
		}

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
			trackNode.Position = new Vector3(0, GlobalState.Singleton.TrackGap, 0) * index++;
		}
	}

	void OnMidiTrackToMenuTrackConfigNodeChanged()
	{
		GenerateTracks();
	}

}
