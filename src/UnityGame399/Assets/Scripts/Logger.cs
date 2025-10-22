using UnityEngine;
using System;
using System.IO;
using Game399.Shared.Diagnostics;

public class Logger : MonoBehaviour, IGameLog
{
    public static Logger Instance {get; private set;}
    
    private static string currentLogFilePath;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        createLogFile();
    }
    public void createLogFile()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string folderPath = Application.persistentDataPath + "/Logs";

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        
        currentLogFilePath = Path.Combine(folderPath, $"log_{timestamp}.txt");
        UnityEngine.Debug.Log("log file created at " + currentLogFilePath);
        File.WriteAllText(currentLogFilePath, $"Log Started at {DateTime.Now}\n");
        
        WriteToLog("Game Initialized.", "Info");
    }
    public void Info(string message)
    {
        UnityEngine.Debug.Log(message);
        WriteToLog(message, "Info");
    }

    public void Warn(string message)
    {
        UnityEngine.Debug.LogWarning(message);
        WriteToLog(message, "Warning");
    }

    public void Error(string message)
    {
        UnityEngine.Debug.LogError(message);
        WriteToLog(message, "Error");
    }

    public void WriteToLog(string message, string severity)
    {
        string logEntry = $"{DateTime.Now}, {severity}: {message}\n";
        File.AppendAllText(currentLogFilePath, logEntry);
    }
}
