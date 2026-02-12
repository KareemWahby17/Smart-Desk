using UnityEngine;

public class AutoCloseDrawer : MonoBehaviour
{
    float timeTocloseDrawer = 0f;
    float timeTocloseDrawerSeconds = 0f;
    Animator drawerAnim;
    void Start()
    {
        drawerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (drawerAnim.GetBool("openDrawer") == true)
        {
            DrawerCounter();
        }
        else 
        { 
            timeTocloseDrawerSeconds = 0f;
        }

        if (timeTocloseDrawerSeconds > 10f)
        {
            if (drawerAnim.GetBool("openDrawer") == true)
            {
                GetComponent<AudioSource>().Play();
                drawerAnim.SetBool("openDrawer", false);
            }
        }
    }

    void DrawerCounter()
    {
        timeTocloseDrawer += Time.deltaTime;
        if (timeTocloseDrawer >= 1f)
        {
            timeTocloseDrawer = 0f;
            timeTocloseDrawerSeconds++;
        }
    }
}
