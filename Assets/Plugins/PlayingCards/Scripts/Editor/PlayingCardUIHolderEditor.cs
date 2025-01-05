using RedDevil.PlayingCards;
using UnityEditor;
using UnityEngine;

namespace PlayingCards
{
    [CustomEditor(typeof(PlayingCardUIHolder))]
    public class PlayingCardUIHolderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            // if (GUILayout.Button("Generate Cards"))
            // {
            //     var t = target as PlayingCardUIHolder;
            //     if (t != null)
            //     {
            //         t.playingCardUI.Clear();
            //        var deck= CardSystem.GenerateDeck(true,1);
            //        foreach (var deckCards in deck.GetCards())
            //        {
            //            var card = new PlayingCardUI
            //            {
            //                card = new Card("", deckCards.Suit, deckCards.Rank)
            //            };
            //
            //            card.Name = card.card.CardName;
            //            card.sprite = null;
            //            t.playingCardUI.Add(card);
            //        }
            //         
            //     }
            // }
        }
    }
}