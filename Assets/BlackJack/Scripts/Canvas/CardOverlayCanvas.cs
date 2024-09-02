using System;
using UnityEngine;
using UnityEngine.UI;

public class CardOverlayCanvas : OverlayCanvas
{
    [SerializeField] private Button backButton;

    private void Awake()
    {
        
    }

    protected override void AddListener()
    {
        backButton.onClick.AddListener(OnBackClick);
    }

    protected override void RemoveListener()
    {
        backButton.onClick.RemoveListener(OnBackClick);
    }

    private void OnBackClick()
    {
        HideOverlay();
    }
}