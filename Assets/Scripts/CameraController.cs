/*************************************************
*** Designer : 御堂
*** Date : 2025.7.3
*** Purpose : カメラの移動
*************************************************/
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 10f;
    public float moveSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 50f;

    private Vector3 lastMousePosition;

    void Update()
    {
        HandleZoom();
        HandleDrag();
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Camera.main.orthographic)
        {
            // 2D用：Orthographic Sizeを変える
            Camera.main.orthographicSize -= scroll * zoomSpeed;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
        }
    }

    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0)) // 右クリックで開始
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 move = new Vector3(-delta.x * moveSpeed * Time.deltaTime, -delta.y * moveSpeed * Time.deltaTime, 0);
            Camera.main.transform.Translate(move, Space.Self);
            lastMousePosition = Input.mousePosition;
        }
    }
}
