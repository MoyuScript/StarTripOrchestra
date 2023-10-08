
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace MoyuMidi;

public partial class MidiFile : Godot.GodotObject, IEnumerable<MidiTrack>
{
    public NAudio.Midi.MidiFile OriginalMidiFile
    {
        get;
        private set;
    } = null;

    public List<MidiTrack> Tracks { get; private set; }
    public int DeltaTicksPerQuarterNote
    {
        get => OriginalMidiFile.DeltaTicksPerQuarterNote;
    }

    public string Path { get; private set; }

    public TimeSpan Duration { get; private set; }

    public List<MidiEvent> TempoEvents { get; private set; }

    public MidiFile(string path)
    {
        Path = path;
        var nMidiFile = new NAudio.Midi.MidiFile(path);
        OriginalMidiFile = nMidiFile;
        ParseTracks();
        ExtractTempoEvents();

        // 必须放在 ParseTrack 和 ExtractTempoEvents 后
        CalculateDurationAndEventAbsoluteTimeInSeconds();
    }

    public IEnumerator<MidiTrack> GetEnumerator() => Tracks.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public MidiTrack this[int index] => Tracks[index];

    void ParseTracks()
    {
        Tracks = new List<MidiTrack>(
            from track in OriginalMidiFile.Events
            select new MidiTrack(track)
            {
                MidiFile = this
            }
        );
    }

    void ExtractTempoEvents()
    {
        TempoEvents = new(
            from track in this
            from ev in track
            where ev.OriginalMidiEvent is NAudio.Midi.TempoEvent
            orderby ev.OriginalMidiEvent.AbsoluteTime ascending
            select ev
        );
    }

    void CalculateDurationAndEventAbsoluteTimeInSeconds()
    {
        float totalMicroseconds = 0;
        int microsecondsPerTick = 0;
        List<MidiEvent> allEvents = new List<MidiEvent>(
            from track in Tracks
            from ev in track
            orderby ev.OriginalMidiEvent.AbsoluteTime ascending
            select ev
        );
        MidiEvent lastEvent = null;
        foreach (var ev in allEvents)
        {
            if (ev.OriginalMidiEvent is NAudio.Midi.TempoEvent tempo)
            {
                microsecondsPerTick = tempo.MicrosecondsPerQuarterNote / DeltaTicksPerQuarterNote;
            }
            totalMicroseconds += microsecondsPerTick * (lastEvent is null ? 0 : ev.OriginalMidiEvent.AbsoluteTime - lastEvent.OriginalMidiEvent.AbsoluteTime);
            ev.AbsoluteTimeSpan = new TimeSpan((long)(totalMicroseconds * 10));
            lastEvent = ev;
        }
        Duration = new((long)(totalMicroseconds * 10));
    }
}