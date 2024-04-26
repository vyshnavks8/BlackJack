using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatCanvas : CanvasBase
{
    [SerializeField] private Chat chatPrefab;
    [SerializeField] private RectTransform scrollContentTransform;

    [SerializeField] private TMP_InputField chatInput;
    [SerializeField] private Button sendButton;
    [SerializeField] private Button backButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase gameCanvas;

    private string chat;

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

    private void OnBackClick()
    {
        OnSetCanvasActive(gameCanvas);
    }

    private void OnChatSet(string input)
    {
        chat = input;
    }

    private void OnSendClick()
    {
        if (chat.Length > 0)
        {
            var chatInstance = Instantiate(chatPrefab, scrollContentTransform);
            chatInstance.SetText(chat);
        }

        chatInput.text = string.Empty;
        chat = string.Empty;
    }
}