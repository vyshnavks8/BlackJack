using System.Collections.Generic;
using UnityEngine;

public class MobileLogViewer : MonoBehaviour
{
    private readonly List<string> logs = new();
    private const int maxLogs = 20;
    private static MobileLogViewer viewer;

    private GUIStyle logStyle;
    private Vector2 scrollPos;

    private void Awake()
    {
        if (viewer == null)
        {
            viewer = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (viewer != null)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
        logStyle = new GUIStyle
        {
            fontSize = 28,
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.black }
        };
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        logs.Add(logString);
        if (logs.Count > maxLogs)
            logs.RemoveAt(0);
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.height / 2));
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear Logs"))
        {
            logs.Clear();
        } if (GUILayout.Button("Random Colour"))
        {
            logStyle.normal.textColor = new Color(
                Random.value,
                Random.value,
                Random.value
            );
        }
        GUILayout.EndHorizontal();
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        foreach (var log in logs)
        {
            GUILayout.Label(log, logStyle);
        }
        GUILayout.EndScrollView();

        GUILayout.EndArea();
    }
}