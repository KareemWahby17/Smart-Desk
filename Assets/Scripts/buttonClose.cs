using UnityEngine;

public class buttonClose : MonoBehaviour
{
    [SerializeField] GameObject drawer;
    Animator animator;

    void Start()
    {
        animator = drawer.GetComponent<Animator>();
    }
    public void OnButtonClick()
    {
        if (animator.GetBool("openDrawer") == true)
        {
            drawer.GetComponent<AudioSource>().Play();
            animator.SetBool("openDrawer", false);
        }
    }
}