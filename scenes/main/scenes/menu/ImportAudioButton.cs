using Godot;
using System;

namespace GameMenu;
public partial class ImportAudioButton : Button
{
	public override void _Ready()
	{
		Pressed += OnPressed;
		GetNode<FileDialog>("../FileDialog").FileSelected += OnFileSelected;
	}

	void OnFileSelected(string path)
	{
		GlobalState.Singleton.AudioPath = path;
	}

	void OnPressed()
	{
		GetNode<FileDialog>("../FileDialog").Visible = true;
	}
}
