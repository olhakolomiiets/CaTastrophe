using UnityEngine;

public class MaskTransformation : MonoBehaviour
{
    public RectTransform targetButton;
    public Canvas tutorialCanvas;
    public Camera uiCamera;

    private void OnRectTransformDimensionsChange()
    {
        UpdateMaskPosition();
    }

    public void UpdateMaskPosition()
    {
        RectTransform mask = GetComponent<RectTransform>();

        Vector3[] worldCorners = new Vector3[4];
        targetButton.GetWorldCorners(worldCorners);

        Vector3 centerWorldPosition = (worldCorners[0] + worldCorners[2]) / 2;

        RectTransform tutorialCanvasRect = tutorialCanvas.GetComponent<RectTransform>();
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            tutorialCanvasRect,
            RectTransformUtility.WorldToScreenPoint(uiCamera, centerWorldPosition),
            uiCamera,
            out localPoint
        );

        mask.localPosition = localPoint;

        mask.sizeDelta = targetButton.sizeDelta;
    }
}

