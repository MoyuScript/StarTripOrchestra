
using System;

namespace MoyuMidi;

public partial class MidiEvent : Godot.GodotObject
{
    public MidiTrack MidiTrack { get; set; }
    public TimeSpan AbsoluteTimeSpan { get; set; }
    public NAudio.Midi.MidiEvent OriginalMidiEvent
    {
        get;
        private set;
    }

    public MidiEvent(NAudio.Midi.MidiEvent naudioMidiEvent)
    {
        OriginalMidiEvent = naudioMidiEvent;
    }
}