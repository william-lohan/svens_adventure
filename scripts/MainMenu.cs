using Godot;
using System;

public partial class MainMenu : Control
{
    [Export]
    private Control firstControl;

    private PackedScene overWorld;

    private PackedScene optionsMenu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        overWorld = GD.Load<PackedScene>("res://scenes/over_world.tscn");
        optionsMenu = GD.Load<PackedScene>("res://prefabs/options_menu.tscn");
        firstControl.GrabFocus();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


    public void _OnStartButtonPressed()
    {
        GetTree().ChangeSceneToPacked(overWorld);
    }

    public void _OnOptionsButtonPressed()
    {}

    public void _OnQuitButtonPressed()
    {
        GetTree().Quit();
    }
}
