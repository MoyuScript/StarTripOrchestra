
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MoyuMidi;

public partial class MidiTrack : Godot.GodotObject, IEnumerable<MidiEvent>
{
    public List<MidiEvent> Events { get; private set; } = new();
    public string TrackName { get; private set; } = null;
    public MidiFile MidiFile { get; set; } = null;

    public MidiTrack(IEnumerable<NAudio.Midi.MidiEvent> events)
    {
        Events = new List<MidiEvent>(
            from ev in events
            select new MidiEvent(ev)
            {
                MidiTrack = this
            }
        );

        

        foreach (var ev in events)
        {
            if (ev is NAudio.Midi.TextEvent textEvent && textEvent.MetaEventType == NAudio.Midi.MetaEventType.SequenceTrackName)
            {
                TrackName = textEvent.Text;
            }
        }
    }

    public IEnumerator<MidiEvent> GetEnumerator()
    {
        return Events.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public MidiEvent this[int index] => Events[index];
}