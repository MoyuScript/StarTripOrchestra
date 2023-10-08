using Godot;
using System;
using System.IO;

public partial class BackgroundMusic : AudioStreamPlayer
{
	public override void _Ready()
	{
		GlobalState.Singleton.AudioPathChanged += OnAudioPathChanged;
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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
