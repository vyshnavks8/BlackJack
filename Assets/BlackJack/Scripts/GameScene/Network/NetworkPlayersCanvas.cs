using System;
using System.Collections.Generic;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class NetworkPlayersCanvas : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private Button startButton;
    [SerializeField] private Transform playerListPivot;
    [SerializeField] private NetworkPlayerUI networkPlayerUI;
    private readonly Dictionary<int, NetworkPlayerUI> networkPlayersDictionary = new();
    public event Action OnStartButtonPressed;

    private void OnEnable()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
    }


    private void OnDisable()
    {
        startButton.onClick.RemoveListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        OnStartButtonPressed?.Invoke();
    }

    public void ShowCanvas(bool showStart)
    {
        startButton.gameObject.SetActive(showStart);
        pivot.gameObject.SetActive(true);
    }

    public void AddPlayer(int id, (string playerName, Sprite playerIcon) data)
    {
        var playerUI = Instantiate(networkPlayerUI, playerListPivot);
        playerUI.SetData(data.playerIcon, data.playerName);
        networkPlayersDictionary.Add(id, playerUI);
    }

    public void RemovePlayer(int id)
    {
        networkPlayersDictionary.TryGetValue(id, out var playerUI);
        if (playerUI != null) Destroy(playerUI.gameObject);
        networkPlayersDictionary.Remove(id);
    }

    public void ClearPlayers()
    {
        foreach (var playerUI in networkPlayersDictionary.Values)
        {
            Destroy(playerUI.gameObject);
        }

        networkPlayersDictionary.Clear();
    }

    public void HideCanvas()
    {
        ClearPlayers();
        pivot.gameObject.SetActive(false);
    }
}