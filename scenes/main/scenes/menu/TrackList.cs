using System.Collections.Generic;
using Godot;
using System.Linq;
using MoyuMidi;

namespace GameMenu;

public partial class TrackList : VBoxContainer
{
	[Export] public PackedScene TrackScene;
	public override void _Ready()
	{
		GlobalState.Singleton.MidiFileChanged += OnMidiFileChanged;
		GlobalState.Singleton.MidiTrackToMenuTrackConfigNodeChanged += OnMidiTrackToMenuTrackConfigNodeChanged;
	}

	void OnMidiTrackToMenuTrackConfigNodeChanged()
	{
		foreach (var child in GetChildren())
		{
			child.QueueFree();
		}

		foreach (var child in GlobalState.Singleton.MidiTrackToMenuTrackConfigNode.Values)
		{
			AddChild(child);
		}
	}

	static IEnumerable<Color> GenerateTrackColor(float hueStep = 0.1f)
	{
		float hue = 0.0f;
		while (true)
		{
			yield return Color.FromHsv(hue, 0.7f, 1.0f);
			hue += hueStep;
		}
	}

	void OnMidiFileChanged()
	{
		MidiFile midiFile = GlobalState.Singleton.MidiFile;
		var trackColorGenerator = GenerateTrackColor().GetEnumerator();
		trackColorGenerator.MoveNext();
		int trackIndex = 0;

		Dictionary<MidiTrack, TrackItem> map = new();

		foreach (var track in midiFile)
		{
			if (track.TrackName is null)
			{
				continue;
			}
			var trackItem = TrackScene.Instantiate<TrackItem>();
			trackItem.TrackIndex = trackIndex;
			trackItem.TrackColor = trackColorGenerator.Current;
			trackItem.TrackName = track.TrackName;

			trackIndex++;
			trackColorGenerator.MoveNext();
			map.Add(track, trackItem);
		}

		GlobalState.Singleton.MidiTrackToMenuTrackConfigNode = map;
	}
}
