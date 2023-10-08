using Godot;
using System;

namespace GameMenu;

public partial class AudioPathLabel : Label
{
	public override void _Ready()
	{
		GlobalState.Singleton.AudioPathChanged += OnAudioPathChanged;
		OnAudioPathChanged();
	}

	void OnAudioPathChanged()
	{
		Text = GlobalState.Singleton.AudioPath ?? "请选择音频文件...";
	}
}
