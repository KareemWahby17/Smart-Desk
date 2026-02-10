using UnityEngine;

public class closeDrawer : MonoBehaviour
{
    [SerializeField] Animator animator;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    if (animator.GetBool("openDrawer") == true)
                    {
                        animator.SetBool("openDrawer", false);
                    }
                }
            }

        }

    }
}