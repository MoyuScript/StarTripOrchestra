using Godot;
using System;

namespace GameMenu;

public partial class MidiPathLabel : Label
{
	public override void _Ready()
	{
		GlobalState.Singleton.MidiFileChanged+= OnMidiFileChanged;
		OnMidiFileChanged();
	}

	void OnMidiFileChanged()
	{
		Text = GlobalState.Singleton.MidiFile?.Path ?? "请选择 MIDI 文件...";
	}
}
