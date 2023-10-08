using Godot;
using MoyuMidi;
using System;

namespace GameMenu;

public partial class ImportMidiButton : Button
{
	public override void _Ready()
	{
		Pressed += OnPressed;
		GetNode<FileDialog>("../FileDialog").FileSelected += OnFileSelected;
	}

	void OnFileSelected(string path)
	{
		GlobalState.Singleton.MidiFile = new (path);
	}

	void OnPressed()
	{
		GetNode<FileDialog>("../FileDialog").Visible = true;
	}
}
