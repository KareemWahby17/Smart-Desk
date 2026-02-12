using UnityEngine;

public class openDrawer : MonoBehaviour
{
    [SerializeField] GameObject drawer;
    Animator animator;

    void Start()
    {
        animator = drawer.GetComponent<Animator>();
    }
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
                        drawer.GetComponent<AudioSource>().Play();
                        animator.SetBool("openDrawer", true);
                    }
                }
            }

        }

    } 
}