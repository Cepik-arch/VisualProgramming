using UnityEngine;
using UnityEngine.EventSystems;

public class BlockDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private RectTransform blockRectTransform;
    private CanvasGroup canvasGroup;

    private Vector2 offset;

    private BlockConnector blockConnector;

    private void Awake()
    {
        blockRectTransform = GetComponent<RectTransform>();
        blockConnector = GetComponentInParent<BlockConnector>();

        // Add a CanvasGroup component if it doesn't exist
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (eventData.clickCount == 2)
        {
            if (blockConnector != null)
            {
                blockConnector.SetBlockClicked(GetComponent<Block>());
            }
        }
        Debug.Log("Block clicked: " + name);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = blockRectTransform.anchoredPosition - eventData.position;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        blockRectTransform.anchoredPosition = eventData.position + offset;

        // Update connection lines only if a connected block is moved
        if (blockConnector != null)
        {
            blockConnector.UpdateConnectionLines();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }
}
