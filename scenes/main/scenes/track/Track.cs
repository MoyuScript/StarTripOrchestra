using Godot;
using MoyuMidi;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Track : Node3D
{
	[Export] public PackedScene NoteScene;

	MidiTrack _midiTrack;
	public MidiTrack MidiTrack
	{
		get => _midiTrack;
		set
		{
			_midiTrack = value;
			GenerateNotes();
		}
	}

	GameMenu.TrackItem _trackConfigItem;

	void GenerateNotes()
	{
		if (!GlobalState.Singleton.MidiTrackToMenuTrackConfigNode.TryGetValue(MidiTrack, out _trackConfigItem))
		{
			return;
		}
		_trackConfigItem.TrackColorChanged += OnTrackColorChanged;
		// 插入 TempoEvent
		List<MidiEvent> eventsWithTempo = MidiTrack.MidiFile.TempoEvents
			.Concat(MidiTrack.Events)
			.OrderBy(ev => ev.OriginalMidiEvent.AbsoluteTime)
			.ToList();

		double microsecondsPerTick = 0;

		Dictionary<int, MidiEvent> noteOnEventMap = new();

		foreach (var ev in MidiTrack)
		{
			if (ev.OriginalMidiEvent is NAudio.Midi.TempoEvent tempoEvent)
			{
				microsecondsPerTick = tempoEvent.MicrosecondsPerQuarterNote / MidiTrack.MidiFile.DeltaTicksPerQuarterNote;
			} else if (ev.OriginalMidiEvent is NAudio.Midi.NoteOnEvent noteOn)
			{
				if (noteOnEventMap.ContainsKey(noteOn.NoteNumber))
				{
					continue;
				}
				noteOnEventMap.Add(noteOn.NoteNumber, ev);
			} else if (ev.OriginalMidiEvent is NAudio.Midi.NoteEvent noteOff)
			{

				noteOnEventMap.Remove(noteOff.NoteNumber, out MidiEvent noteOnEvent);
				TimeSpan duration = ev.AbsoluteTimeSpan - noteOnEvent.AbsoluteTimeSpan;
				
				Note note = NoteScene.Instantiate<Note>();
				note.NoteOnEvent = noteOnEvent;
				note.NoteOffEvent = ev;
				
				UpdateNoteColor(note);
				UpdateNoteScaleAndPosition(note);
				
				AddChild(note);
			}
		}
	}

	void UpdateAllNotesScaleAndPosition()
	{
		foreach (Node node in GetChildren())
		{
			if (node is Note note)
			{
				UpdateNoteScaleAndPosition(note);
			}
		}
	}

	void OnNoteMetersPerSecondsChanged()
	{
		UpdateAllNotesScaleAndPosition();
	}

	void OnTrackColorChanged()
	{
		UpdateAllNotesColor();
	}

	void UpdateAllNotesColor()
	{
		foreach (Node node in GetChildren())
		{
			if (node is Note note)
			{
				UpdateNoteColor(note);
			}
		}
	}

	void UpdateNoteScaleAndPosition(Note note)
	{
		TimeSpan duration = note.NoteOffEvent.AbsoluteTimeSpan - note.NoteOnEvent.AbsoluteTimeSpan;
		float PitchGap = GlobalState.Singleton.NotePitchGap;
		note.Scale = new Vector3((float)(GlobalState.Singleton.NoteMetersPerSeconds * duration.TotalSeconds), 1.0f, 1.0f);
		note.Position = new Vector3((float)note.NoteOnEvent.AbsoluteTimeSpan.TotalSeconds * GlobalState.Singleton.NoteMetersPerSeconds, 0, -PitchGap * ((NAudio.Midi.NoteOnEvent)note.NoteOnEvent.OriginalMidiEvent).NoteNumber);
	}

	void UpdateNoteColor(Note note)
	{
		note.NoteColor = _trackConfigItem.TrackColor;
	}

	void OnNotePitchGapChanged()
	{
		UpdateAllNotesScaleAndPosition();
	}

	public override void _Ready()
	{
		GlobalState.Singleton.NoteMetersPerSecondsChanged += OnNoteMetersPerSecondsChanged;
		GlobalState.Singleton.NotePitchGapChanged += OnNotePitchGapChanged;
	}
}
