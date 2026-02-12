using TMPro;
using UnityEngine;

public class IRsensor : MonoBehaviour
{
    [SerializeField] private GameObject txt;
    [SerializeField] private GameObject lightBulb;
    [SerializeField] private GameObject drawer;
    TextMeshPro textMesh;
    Animator drawerAnim;
    float timer = 0f;
    bool isDeskApproached = false;

    private void Start()
    {
        textMesh = txt.GetComponent<TextMeshPro>();
        drawerAnim = drawer.GetComponent<Animator>();
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        lightBulb.GetComponent<Light>().enabled = false;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                Count();
                lightBulb.GetComponent<Light>().enabled = true;
                
                
                if (!isDeskApproached) 
                {
                    if (drawerAnim.GetBool("openDrawer") == false)
                    {
                        drawer.GetComponent<AudioSource>().Play();
                        drawerAnim.SetBool("openDrawer", true);
                        isDeskApproached = true;
                    } 
                } 
            }
        }
    }

    void Count() {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            timer = 0f;

            int counter = int.Parse(textMesh.text);
            if (counter < 99)
                counter++;
            if (counter < 10)
                textMesh.text = "0" + counter.ToString();
            else
                textMesh.text = counter.ToString();
        }
    }
}
