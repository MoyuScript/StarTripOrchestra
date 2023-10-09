using Godot;
using System;

public partial class MidiPlayer : Node
{
	public override void _Ready()
	{
		
	}

	public void Play()
	{
		if (GlobalState.Singleton.AudioPath is null || GlobalState.Singleton.MidiFile is null)
		{
			throw new Exception("没有设置 AudioPath 或 MidiFile，无法播放");
		}
		GlobalState.Singleton.IsPlaying = true;
		GlobalState.Singleton.HasStartedPlay = true;
	}

	public void Pause()
	{
		GlobalState.Singleton.IsPlaying = false;
	}
}
