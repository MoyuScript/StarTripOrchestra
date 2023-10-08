using Godot;
using System;
using System.IO;

public partial class BackgroundMusic : AudioStreamPlayer
{
	public override void _Ready()
	{
		GlobalState.Singleton.AudioPathChanged += OnAudioPathChanged;
		GlobalSignal.Singleton.StartPlay += OnStartPlay;
	}

	void OnStartPlay()
	{
		if (GlobalState.Singleton.MidiFile is null || GlobalState.Singleton.AudioPath is null)
		{
			return;
		}
		Play();
	}

	void OnAudioPathChanged()
	{
		string audioPath = GlobalState.Singleton.AudioPath;
		
		try
		{
			var audioFile = Godot.FileAccess.Open(audioPath, Godot.FileAccess.ModeFlags.Read);
			var buffer = audioFile.GetBuffer((long)audioFile.GetLength());

			Stream = Path.GetExtension(audioPath) switch
			{
				".mp3" => new AudioStreamMP3()
				{
					Data = buffer
				},
				_ => throw new FormatException("不支持的音频格式"),
			};
		}
		catch (Exception e)
		{
			Main.Alert($"音频加载失败：{e.Message}");
		}
	}
}
