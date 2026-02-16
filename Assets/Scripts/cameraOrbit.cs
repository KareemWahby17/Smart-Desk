using UnityEngine;

public class cameraOrbit : MonoBehaviour
{
void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.forward * scroll * 10f);
    }
}
