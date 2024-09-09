using UnityEngine;
using UnityEngine.UI;

public class ScrollViewBox : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;

    private void OnEnable()
    {
        scrollRect.content.anchoredPosition=Vector2.zero;
    }
    
}