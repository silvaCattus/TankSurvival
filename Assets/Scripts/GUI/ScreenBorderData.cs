using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorderData : MonoBehaviour
{
    public static ScreenBorderData instance;

    public Vector2 bottomLeft { get; private set; }
    public Vector2 topLeft { get; private set; }
    public Vector2 topRight { get; private set; }
    public Vector2 bottomRight { get; private set; }

    private void Awake()
    {
        instance = this;

        var cam = Camera.main;

        bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));
    }
}
