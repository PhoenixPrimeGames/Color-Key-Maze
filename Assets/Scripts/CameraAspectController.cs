using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAspectController : MonoBehaviour
{
    private const float ReferenceAspect = 16f / 9f;

    private Camera cameraComponent;
    private float referenceOrthographicSize;
    private float previousAspect;

    private void Awake()
    {
        cameraComponent = GetComponent<Camera>();
        referenceOrthographicSize = cameraComponent.orthographicSize;

        previousAspect = cameraComponent.aspect;
        AdjustCamera();
    }

    private void Update()
    {
        float currentAspect = cameraComponent.aspect;

        if (!Mathf.Approximately(currentAspect, previousAspect))
        {
            previousAspect = currentAspect;
            AdjustCamera();
        }
    }

    private void AdjustCamera()
    {
        float currentAspect = cameraComponent.aspect;

        if (currentAspect < ReferenceAspect)
        {
            cameraComponent.orthographicSize =
                referenceOrthographicSize * (ReferenceAspect / currentAspect);
        }
        else
        {
            cameraComponent.orthographicSize = referenceOrthographicSize;
        }
    }
}