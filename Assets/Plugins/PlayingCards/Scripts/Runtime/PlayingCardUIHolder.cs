using System.Collections.Generic;
using System.Linq;
using RedDevil.PlayingCards;
using UnityEngine;

namespace PlayingCards
{
    public class PlayingCardUIHolder : ScriptableObject
    {
        public Sprite cardBackBlack;
        public Sprite cardBackBlue;
        public Sprite cardBackRed;
        [SerializeField] private List<PlayingCardUI> playingCardUI = new();

        public Sprite GetPlayingCardBack(Card card)
        {
            return (from playCards in playingCardUI
                where playCards.card.IsSameRankAndSuit(card)
                select playCards.sprite).FirstOrDefault();
        }

        public Sprite GetRandomBackCard()
        {
            var ran= Random.Range(0, 3);
            return ran switch
            {
                0 => cardBackBlack,
                1 => cardBackBlue,
                2 => cardBackRed,
                _ => cardBackBlack
            };
        }
    }
}