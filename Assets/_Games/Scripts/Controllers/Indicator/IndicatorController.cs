using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorController : MonoBehaviour
{
    public static IndicatorController indicatorIns;

    void Awake()
    {
        indicatorIns = this;
    }

    [SerializeField] private GameObject arrowObject;
    private Transform playerTransform;
    private Vector3 targetPosition;
    public Vector3 TargetPosition
    {
        get
        {
            return targetPosition;
        }

        set
        {
            targetPosition = value;
        }
    }

    public Transform PlayerTransform
    {

        get
        {
            return playerTransform;
        }

        set
        {
            playerTransform = value;
        }
    }

    private Camera mainCamera;
    private RectTransform arrowTransform;
    private Vector3 screenCenter;

    public void OnInit()
    {
        arrowTransform = arrowObject.GetComponent<RectTransform>();
        mainCamera = Camera.main;
        screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;

    }

    public void ChangePositionTarget(RectTransform rectTransform)
    {
        //Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetPosition);
        Vector3 toPosition = targetPosition;
        Vector3 formPosition = playerTransform.position;
        Vector3 dir = (toPosition - formPosition).normalized;
        //float angle = GetAngleFromVectorFloat(dir);
        //arrowTransform.localEulerAngles = new Vector3(0, 0, angle);

        Vector3 targetPositionScreenPoint = mainCamera.WorldToViewportPoint(targetPosition);

        if (targetPositionScreenPoint.x > 0 &&
        targetPositionScreenPoint.x < 1 &&
        targetPositionScreenPoint.y > 0 &&
        targetPositionScreenPoint.y < 1
        )
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            Vector3 screneEdge = mainCamera.ViewportToScreenPoint(
                new Vector3(
                    Mathf.Clamp(targetPositionScreenPoint.x, 0.1f, 0.9f),
                    Mathf.Clamp(targetPositionScreenPoint.y, 0.1f, 0.9f),
                    mainCamera.nearClipPlane
                )
            );
            arrowTransform.anchoredPosition = new Vector2(screneEdge.x - screenCenter.x, screneEdge.y - screenCenter.y);
        }
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        return angle;
    }
}
