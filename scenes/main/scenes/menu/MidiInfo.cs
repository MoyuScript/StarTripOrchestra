using Godot;
using MoyuMidi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameMenu;

public partial class MidiInfo : Label
{
	public override void _Ready()
	{
		GlobalState.Singleton.MidiFileChanged += OnMidiFileChanged;
	}

	void OnMidiFileChanged()
	{
		MidiFile midiFile = GlobalState.Singleton.MidiFile;

		var midiEvents = from midiEvent in (
			from track in midiFile.Tracks
			from midiEvent in track
			select midiEvent
		)
			orderby midiEvent.OriginalMidiEvent.AbsoluteTime ascending
			select midiEvent;

		int noteCount = (
			from midiEvent in midiEvents
			where midiEvent.OriginalMidiEvent.CommandCode == NAudio.Midi.MidiCommandCode.NoteOn
			select midiEvent
		).Count();

		Text = $"轨道数：{midiFile.Tracks.Count}  音符数：{noteCount}  TPQ：{midiFile.DeltaTicksPerQuarterNote}  总时长：{midiFile.Duration}";
	}
}
