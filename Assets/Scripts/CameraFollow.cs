using UnityEngine;

public class CameraFollow : FollowTarget
{
    [SerializeField]
    float targetSize;

    [SerializeField]
    float zoomSpeed;

    new Camera camera;

    private void Awake() => camera = GetComponent<Camera>();

    // Update is called once per frame
    void Update()
    {
        Follow();
        if (!Mathf.Approximately(camera.orthographicSize, targetSize))
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetSize, zoomSpeed * Time.deltaTime);
    }
}
