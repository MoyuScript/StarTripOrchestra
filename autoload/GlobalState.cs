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

	[Export] public float NoteMoveSpeed = 10.0f;

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
}
