using Godot;
using System;

public partial class PlayButton : Button
{
	public override void _Ready()
	{
		Pressed += OnPressed;
		GlobalState.Singleton.MidiFileChanged += UpdateDisabled;
		GlobalState.Singleton.AudioPathChanged += UpdateDisabled;
		GlobalState.Singleton.IsPlayingChanged += OnIsPlayingChanged;
		UpdateDisabled();
	}

	void OnPressed()
	{
		MidiPlayer player = GetNode<MidiPlayer>("/root/Main/MidiPlayer");
		if (GlobalState.Singleton.IsPlaying)
		{
			player.Pause();
		}
		else
		{
			player.Play();
		}
	}

	void OnIsPlayingChanged()
	{
		if (GlobalState.Singleton.IsPlaying)
		{
			Text = "暂停";
		}
		else
		{
			Text = "播放";
		}
	}

	void UpdateDisabled()
	{
		Disabled = GlobalState.Singleton.MidiFile is null || GlobalState.Singleton.AudioPath is null;
	}
}
