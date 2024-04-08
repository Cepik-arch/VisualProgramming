using UnityEngine;
using UnityEngine.EventSystems;

public class BlockDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private RectTransform blockRectTransform;
    private CanvasGroup canvasGroup;
    private RectTransform parentRectTransform; // Reference to the parent RectTransform
    private Vector2 offset;
    private BlockConnector blockConnector;

    private void Awake()
    {
        blockRectTransform = GetComponent<RectTransform>();
        blockConnector = GetComponentInParent<BlockConnector>();

        // Add a CanvasGroup component if it doesn't exist
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Get reference to the parent RectTransform
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
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
        // Calculate the anchored position within the parent RectTransform
        Vector2 newPos = eventData.position + offset;

        // Convert mouse position to local position in parent RectTransform
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out newPos);

        // Calculate boundaries in parent RectTransform space
        float minX = parentRectTransform.rect.xMin + blockRectTransform.rect.width / 2; // Adjusted to account for block size
        float maxX = parentRectTransform.rect.xMax - blockRectTransform.rect.width / 2; // Adjusted to account for block size
        float minY = parentRectTransform.rect.yMin + blockRectTransform.rect.height / 2; // Adjusted to account for block size
        float maxY = parentRectTransform.rect.yMax - blockRectTransform.rect.height / 2; // Adjusted to account for block size

        // Clamp the position within the parent RectTransform boundaries
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

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

    public void RemoveConnection()
    {
        blockConnector.RemoveAllConnections(GetComponent<Block>());
    }
}
