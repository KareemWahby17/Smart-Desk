using System.ComponentModel.Design.Serialization;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    TextMeshPro textMesh;
    float timer = 0f;
    void Start()
    {
        // initialize the TextMesh component of the GameObject with the value 0
        textMesh = GetComponent<TextMeshPro>();
    }
    void Update()
    {
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
