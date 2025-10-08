using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatOverlay : OverlayCanvas
{
    [SerializeField] private Chat chatPrefab;
    [SerializeField] private Chat chatPrefabOther;
    [SerializeField] private RectTransform scrollContentTransform;

    [SerializeField] private TMP_InputField chatInput;
    [SerializeField] private Button sendButton;
    [SerializeField] private Button backButton;
    private string chat;
    [SerializeField] private GameNetworkEventSender networkSender;

    protected override void AddListener()
    {
        chatInput.onValueChanged.AddListener(OnChatSet);
        sendButton.onClick.AddListener(OnSendClick);
        backButton.onClick.AddListener(OnBackClick);
    }

    protected override void RemoveListener()
    {
        chatInput.onValueChanged.RemoveListener(OnChatSet);
        sendButton.onClick.RemoveListener(OnSendClick);
        backButton.onClick.RemoveListener(OnBackClick);
    }

    private void Start()
    {
        networkSender.OnChatReceived += OnChatReceived;
    }


    private void OnDestroy()
    {
        networkSender.OnChatReceived -= OnChatReceived;
    }

    private void OnBackClick()
    {
        HideOverlay();
    }

    private void OnChatSet(string input)
    {
        chat = input;
    }

    private void OnSendClick()
    {
        if (chat.Length > 0)
        {
            networkSender.SendChat(NetworkManager.LocalPlayer.ActorNumber, chat);
        }

        chatInput.text = string.Empty;
        chat = string.Empty;
    }

    private void OnChatReceived(int id, string message)
    {
        var icon = GameNetworkData.GetPlayerIcon(id);
        if (id == NetworkManager.LocalPlayer.ActorNumber)
        {
            var chatInstance = Instantiate(chatPrefab, scrollContentTransform);

            chatInstance.SetText(icon, message);
        }
        else
        {
            var chatInstance = Instantiate(chatPrefabOther, scrollContentTransform);
            chatInstance.SetText(icon, message);
        }
    }
}