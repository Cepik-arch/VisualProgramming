using UnityEngine;
using UnityEngine.EventSystems;

public class BlockDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private RectTransform blockRectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas; // Reference to the Canvas component
    private RectTransform canvasRectTransform; // Reference to the Canvas RectTransform
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

        // Get reference to the Canvas component
        canvas = GetComponentInParent<Canvas>();
        // Get reference to the Canvas RectTransform
        canvasRectTransform = canvas.GetComponent<RectTransform>();
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
        // Calculate the anchored position within the canvas
        Vector2 newPos = eventData.position + offset;
        
        // Clamp the position within the canvas boundaries
        newPos.x = Mathf.Clamp(newPos.x, canvasRectTransform.rect.xMin, canvasRectTransform.rect.xMax - blockRectTransform.rect.width);
        newPos.y = Mathf.Clamp(newPos.y, canvasRectTransform.rect.yMin, canvasRectTransform.rect.yMax - blockRectTransform.rect.height);
        
        // Set the new anchored position
        blockRectTransform.anchoredPosition = newPos;

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
