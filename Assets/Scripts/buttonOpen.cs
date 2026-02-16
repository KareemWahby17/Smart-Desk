using UnityEngine;

public class buttonOpen : MonoBehaviour
{
    [SerializeField] GameObject drawer;
    Animator animator;

    void Start()
    {
        animator = drawer.GetComponent<Animator>();
    }
    public void OnButtonClick()
    {
        if (animator.GetBool("openDrawer") == false)
        {
            drawer.GetComponent<AudioSource>().Play();
            animator.SetBool("openDrawer", true);
        }
    }
}