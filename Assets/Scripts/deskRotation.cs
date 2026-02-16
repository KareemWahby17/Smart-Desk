using UnityEngine;

public class deskRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            // Rotate the camera around the target object based on mouse movement
            float rotX = Input.GetAxis("Mouse X") * 4f;
            //float rotY = Input.GetAxis("Mouse Y") * 4f;
            transform.Rotate(Vector3.up, -rotX, Space.World);
            //transform.Rotate(Vector3.right, rotY, Space.Self);
        }
        if (Input.GetMouseButton(0))
        {
            // Pan the camera with the left mouse button
            float panX = Input.GetAxis("Mouse X") * 0.3f;
            float panY = Input.GetAxis("Mouse Y") * 0.3f;
            transform.Translate(panX, panY, 0, Space.Self);
        }
    }
}
