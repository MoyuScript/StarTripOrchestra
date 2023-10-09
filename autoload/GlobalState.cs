using Godot;
using System;
using System.Collections.Generic;
using MoyuMidi;

public partial class GlobalState : Node
{
	public static GlobalState Singleton;

	public override void _Ready()
	{
		Singleton = this;
	}

	bool _hasStartedPlay = false;
	[Signal] public delegate void HasStartedPlayChangedEventHandler();
	[Export] public bool HasStartedPlay
	{
		get => _hasStartedPlay;
		set
		{
			_hasStartedPlay = value;
			EmitSignal(SignalName.HasStartedPlayChanged);
		}
	}

	bool _isPlaying = false;
	[Signal] public delegate void IsPlayingChangedEventHandler();
	[Export] public bool IsPlaying
	{
		get => _isPlaying;
		set
		{
			_isPlaying = value;
			EmitSignal(SignalName.IsPlayingChanged);
		}
	}

	bool _isDebugInfoVisible = false;
	[Signal] public delegate void IsDebugInfoVisibleChangedEventHandler();
	public bool IsDebugInfoVisible
	{
		get => _isDebugInfoVisible;
		set
		{
			_isDebugInfoVisible = value;
			EmitSignal(SignalName.IsDebugInfoVisibleChanged);
		}
	}

	bool _isMenuVisible = false;
	[Signal] public delegate void IsMenuVisibleChangedEventHandler();
	public bool IsMenuVisible
	{
		get => _isMenuVisible;
		set
		{
			_isMenuVisible = value;
			EmitSignal(SignalName.IsMenuVisibleChanged);
		}
	}

	bool _isFullScreen = false;
	[Signal] public delegate void IsFullScreenChangedEventHandler();
	public bool IsFullScreen
	{
		get => _isFullScreen;
		set
		{
			_isFullScreen = value;
			EmitSignal(SignalName.IsFullScreenChanged);
		}
	}

	string _audioPath = null;
	[Signal] public delegate void AudioPathChangedEventHandler();
	public string AudioPath
	{
		get => _audioPath;
		set
		{
			_audioPath = value;
			EmitSignal(SignalName.AudioPathChanged);
		}
	}

	float _noteMetersPerSeconds = 20.0f;
	[Signal] public delegate void NoteMetersPerSecondsChangedEventHandler();
	public float NoteMetersPerSeconds
	{
		get => _noteMetersPerSeconds;
		set
		{
			_noteMetersPerSeconds = value;
			EmitSignal(SignalName.NoteMetersPerSecondsChanged);
		}
	}

	MidiFile _midiFile;
	[Signal] public delegate void MidiFileChangedEventHandler();
	public MidiFile MidiFile
	{
		get => _midiFile;
		set
		{
			_midiFile = value;
			EmitSignal(SignalName.MidiFileChanged);
		}
	}

	Dictionary<MidiTrack, GameMenu.TrackItem> _midiTrackToMenuTrackConfigNode = new();
	[Signal] public delegate void MidiTrackToMenuTrackConfigNodeChangedEventHandler();
	public Dictionary<MidiTrack, GameMenu.TrackItem> MidiTrackToMenuTrackConfigNode
	{
		get => _midiTrackToMenuTrackConfigNode;
		set
	{
			_midiTrackToMenuTrackConfigNode = value;
			EmitSignal(SignalName.MidiTrackToMenuTrackConfigNodeChanged);
		}
	}

	float _notePitchGap = 1.0f;
	[Signal] public delegate void NotePitchGapChangedEventHandler();
	public float NotePitchGap
	{
		get => _notePitchGap;
		set
		{
			_notePitchGap = value;
			EmitSignal(SignalName.NotePitchGapChanged);
		}
	}

	float _trackMovement = 0;
	[Signal] public delegate void TrackMovementChangedEventHandler();
	public float TrackMovement
	{
		get => _trackMovement;
		set
		{
			_trackMovement = value;
			EmitSignal(SignalName.TrackMovementChanged);
		}
	}

	float _trackGap = 1.0f;
	[Signal] public delegate void TrackGapChangedEventHandler();
	[Export] public float TrackGap
	{
		get => _trackGap;
		set
		{
			_trackGap = value;
			EmitSignal(SignalName.TrackGapChanged);
		}
	}
}
