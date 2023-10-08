using Godot;
using System;

public partial class Main : Node3D
{
	public static Main Singleton;

	public override void _Ready()
	{
		Singleton = this;
	}
	public static void Alert(string message)
	{
		var dialog = Singleton.GetNode<AcceptDialog>("AlertDialog");
		dialog.DialogText = message;
		dialog.Visible = true;
	}
}
