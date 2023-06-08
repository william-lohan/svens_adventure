using Godot;
using System;
using System.Xml.Linq;



public partial class UserConfig : Node
{
    const string CONFIG_PATH = "user://config.cfg";

    private static ConfigFile configFile;

    public static float Music
    {
        get { return (float) configFile.GetValue("Sound", "music", 100.0f); }
        set { configFile.SetValue("Sound", "music", value); }
    }

    public static float SFX
    {
        get { return (float) configFile.GetValue("Sound", "SFX", 100.0f); }
        set { configFile.SetValue("Sound", "SFX", value); }
    }

    public static string Up
    {
        get { return (string) configFile.GetValue("Input", "up", ""); }
        set { configFile.SetValue("Input", "up", value); }
    }

    public static string Down
    {
        get { return (string) configFile.GetValue("Input", "down", ""); }
        set { configFile.SetValue("Input", "down", value); }
    }

    public static string Left
    {
        get { return (string) configFile.GetValue("Input", "left", ""); }
        set { configFile.SetValue("Input", "left", value); }
    }

    public static string Right
    {
        get { return (string) configFile.GetValue("Input", "right", ""); }
        set { configFile.SetValue("Input", "right", value); }
    }

    public static string Jump
    {
        get { return (string) configFile.GetValue("Input", "jump", ""); }
        set { configFile.SetValue("Input", "jump", value); }
    }

    public override void _Ready()
    {
        configFile = new ConfigFile();
        TryLoadConfig();
    }

    public override void _Notification(int what)
    {
        if (what == NotificationWMCloseRequest)
            SaveConfig();
    }

    private static void LoadDefault()
    {
        configFile = new ConfigFile();
        configFile.SetValue("Sound", "music", 100.0f);
        configFile.SetValue("Sound", "SFX", 100.0f);
        SaveConfig();
    }

    private static void TryLoadConfig()
    {
        Error error = configFile.Load(CONFIG_PATH);

        if (error != Error.Ok)
            return;

        LoadDefault();

    }

    public static void SaveConfig()
    {
        configFile.Save(CONFIG_PATH);
    }
}
