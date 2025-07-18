using RedDevil.PlayingCards;
using UnityEditor;
using UnityEngine;

public class DebugState : BlackJackState
{
    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
    }

    public void SetDeck()
    {
        var deck = CardSystem.GenerateDeck(false);
        deck.Shuffle();
        stateMachine.Context.deck = deck;
    }

    public void AddCard(CardSuit suit, CardRank rank)
    {
        var player = stateMachine.Context.GetCurrentPlayer();
        foreach (var c in stateMachine.Context.deck.GetCards())
        {
            if (c.Suit == suit && c.Rank == rank)
            {
                var cardData = stateMachine.Context.deck.GetCard(c);
                player.AddCard(cardData.card, true);
            }
        }
    }

    public void GetStatus()
    {
        var player = stateMachine.Context.GetCurrentPlayer();
        var stats = player.GetStatus();
        Debug.Log(stats.ToString());
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(DebugState))]
public class DebugStateEditor : Editor
{
    CardSuit cardSuit;
    CardRank cardRank;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var debugState = target as DebugState;
        if (debugState == null) return;
        if (GUILayout.Button("Set Deck"))
        {
            debugState.SetDeck();
        }

        EditorGUILayout.Space(10);
        GUILayout.BeginHorizontal();
        cardSuit = (CardSuit)EditorGUILayout.EnumPopup(cardSuit);
        cardRank = (CardRank)EditorGUILayout.EnumPopup(cardRank);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Add Card"))
        {
            Debug.Log(cardSuit + " " + cardRank);
            debugState.AddCard(cardSuit, cardRank);
        }
        EditorGUILayout.Space(10);
        if (GUILayout.Button("Get Status"))
        {
            debugState.GetStatus();
        }
    }
}
#endif