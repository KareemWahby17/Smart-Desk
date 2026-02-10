using UnityEngine;

public class openDrawer : MonoBehaviour
{
    [SerializeField] Animator animator;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    if (animator.GetBool("openDrawer") == false)
                    {
                        animator.SetBool("openDrawer", true);
                    }
                }
            }

        }

    } 
}