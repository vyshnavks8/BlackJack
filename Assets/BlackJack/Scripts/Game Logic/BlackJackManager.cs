using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class BlackJackManager : MonoBehaviour
{
    [SerializeField] private BlackJackPlayer dealer;
    [SerializeField] private BlackJackPlayer[] players;
    [SerializeField, Range(1, 5)] private int currentPlayerCount;
    [SerializeField] private BlackJackStateMachine stateMachine;
    [SerializeField] private GameCanvas gameCanvas;
    [SerializeField] private GameType gameType;
    [SerializeField, Range(5, 100)] private float gameTime = 10;

    private void OnEnable()
    {
        gameCanvas.OnStartGame += StartGame;
        gameCanvas.OnExitGame += StopGame;
    }

    private void OnDisable()
    {
        gameCanvas.OnStartGame -= StartGame;
        gameCanvas.OnExitGame -= StopGame;
    }

    public void ExitGame()
    {
        gameCanvas.GotoHome();
    }

    public void StartGame()
    {
        var currentPlayers = BlackJackGameUtility.GetPlayers(currentPlayerCount, players);
        SetPlayerData(currentPlayers);
        stateMachine.Init(dealer, currentPlayers);
        stateMachine.GotoStartState();
    }

    public void RestartGame()
    {
        StopGame();
        StartGame();
    }

    public void StopGame()
    {
        foreach (var player in players)
        {
            player.ShowProfile(false);
        }

        stateMachine.ResetData();
    }

    private void SetPlayerData(List<BlackJackPlayer> currentPlayers)
    {
        if (GameType.AI == gameType)
        {
            foreach (var player in currentPlayers)
            {
                SetData(player, player.playerPosition == PlayerPosition.Bottom ? PlayerType.Player : PlayerType.Bot);
            }
        }
        else
        {
            foreach (var player in currentPlayers)
            {
                SetData(player, PlayerType.Player);
            }
        }
    }

    private void SetData(BlackJackPlayer player, PlayerType playerType)
    {
        player.SetData(playerType, gameTime);
        player.ShowProfile(true);
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(BlackJackManager))]
public class BlackJackGameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var manager = target as BlackJackManager;
        if (manager == null) return;
        if (GUILayout.Button("Start Game"))
        {
            manager.StartGame();
        }

        if (GUILayout.Button("Restart Game"))
        {
            manager.RestartGame();
        }

        if (GUILayout.Button("Stop Game"))
        {
            manager.StopGame();
        }
    }
}
#endif