using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorTest : MonoBehaviour
{
    private SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (renderer != null)
        {
            Color newColor = new Color(
                Random.value,
                Random.value,
                Random.value
                );
            renderer.color = newColor;
        }
    }
}
