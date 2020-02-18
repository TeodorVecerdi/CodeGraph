using Scripts.Layout;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Examples
{
    public class PaginationScript : MonoBehaviour, IPointerClickHandler
    {
        public HorizontalScrollSnap hss;
        public int Page;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (hss != null)
            {
                hss.GoToScreen(Page);
            }
        }
    }
}