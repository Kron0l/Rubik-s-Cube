using UnityEngine;

public class CamController : MonoBehaviour
{

    public bool canRotate = true;
    public bool canZoom = true;
    float Horizangle = 0;
    float Verticangle = 0;
    public float Distance = 5;
    Vector2 OldMousePos;
    bool MouseClicked = false;
    public Camera Kamera;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            OldMousePos = Input.mousePosition;
            MouseClicked = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            MouseClicked = false;
        }

        if (MouseClicked && canRotate)
        {
            {
                Horizangle += 0.01f * (Input.mousePosition.x - OldMousePos.x);
                Verticangle += 0.01f * (Input.mousePosition.y - OldMousePos.y);
                if (Verticangle > 1.5f) Verticangle = 1.5f;
                if (Verticangle < -1.5f) Verticangle = -1.5f;
            }
            OldMousePos = Input.mousePosition;
        }

        Distance *= 1 - 1.5f * (canZoom?(Input.GetAxis("Mouse ScrollWheel")):0);

        Kamera.transform.localPosition = Distance * (new Vector3(Mathf.Sin(Horizangle) * Mathf.Cos(Verticangle), Mathf.Sin(Verticangle), Mathf.Cos(Horizangle) * Mathf.Cos(Verticangle)));
        Kamera.transform.LookAt(transform.position);
    }
}