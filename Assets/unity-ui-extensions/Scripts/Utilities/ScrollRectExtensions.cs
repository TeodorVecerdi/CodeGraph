/// Credit Feaver1968 
/// Sourced from - http://forum.unity3d.com/threads/scroll-to-the-bottom-of-a-scrollrect-in-code.310919/

using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Utilities
{
    public static class ScrollRectExtensions
    {
        public static void ScrollToTop(this ScrollRect scrollRect)
        {
            scrollRect.normalizedPosition = new Vector2(0, 1);
        }
        public static void ScrollToBottom(this ScrollRect scrollRect)
        {
            scrollRect.normalizedPosition = new Vector2(0, 0);
        }
    }
}