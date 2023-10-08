using Godot;
using NAudio.Midi;
using System;
using System.Collections.Generic;

namespace GameMenu;

public partial class TrackItem : HBoxContainer
{
	[Signal] public delegate void TrackNameChangedEventHandler();
	[Signal] public delegate void TrackColorChangedEventHandler();
	[Signal] public delegate void TrackIndexChangedEventHandler();

	string _trackName;
	public string TrackName
	{
		get => _trackName;
		set
		{
			_trackName = value;
			
			if (IsNodeReady())
			{
				_trackNameNode.Text = value;
			}
		}
	}

	int _trackIndex;
	public int TrackIndex
	{
		get => _trackIndex;
		set
		{
			_trackIndex = value;
			
			if (IsNodeReady())
			{
				_trackIndexNode.Text = value.ToString();
			}
		}
	}

	Color _trackColor;
	public Color TrackColor
	{
		get => _trackColor;
		set
		{
			_trackColor = value;

			if (IsNodeReady())
			{
				_trackColorNode.Color = value;
			}
		}
	}

	LineEdit _trackIndexNode;
	LineEdit _trackNameNode;
	ColorPickerButton _trackColorNode;

	void OnNodeTrackNameChanged(string text)
	{
		EmitSignal(SignalName.TrackNameChanged);
	}

	void OnNodeTrackIndexChanged(string text)
	{
		try
		{
			Convert.ToInt32(text);
		}
		catch
		{
			_trackIndexNode.Text = "0";
		}
		EmitSignal(SignalName.TrackIndexChanged);
	}

	void OnNodeTrackColorChanged(Color color)
	{
		EmitSignal(SignalName.TrackColorChanged);
	}


	public override void _Ready()
	{
		_trackIndexNode = GetNode<LineEdit>("Index");
		_trackNameNode = GetNode<LineEdit>("Name");
		_trackColorNode = GetNode<ColorPickerButton>("Color");

		_trackNameNode.Text = TrackName;
		_trackIndexNode.Text = TrackIndex.ToString();
		_trackColorNode.Color = TrackColor;

		_trackIndexNode.TextChanged += OnNodeTrackIndexChanged;
		_trackNameNode.TextChanged += OnNodeTrackNameChanged;
		_trackColorNode.ColorChanged += OnNodeTrackColorChanged;
	}
}
