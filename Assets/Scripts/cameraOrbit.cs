using UnityEngine;

public class cameraOrbit : MonoBehaviour
{
    [SerializeField] private GameObject target;
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        // Zoom the camera in and out based on scroll input while clamping the distance to prevent it from getting too close or too far
        if (scroll != 0)
        {
            Vector3 direction = transform.position - target.transform.position;
            float distance = direction.magnitude;
            float zoomAmount = scroll * 5f; // Adjust the zoom speed as needed
            distance = Mathf.Clamp(distance - zoomAmount, 5f, 30f); // Clamp the distance between 2 and 20 units
            transform.position = target.transform.position + direction.normalized * distance;
        }
    }
}
//transform.Translate(Vector3.forward * scroll * 10f);
