using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHueShifter : MonoBehaviour
{

    public Color32[] hues;

    // Start is called before the first frame update
    void Start()
    {
        // loop over all siblings of hue shifter
        foreach (Transform child in transform.parent)
        {
            // exclude shifter itself
            if(child != this.transform)
            {
                // get object renderer
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer)
                {
                    // get random hue from list
                    int index = Random.Range(0, hues.Length);
                    // set material colour
                    Material material = renderer.material;
                    if (material && material.name == "Trees_Maple (Instance)")
                    {
                        material.color = hues[index];
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
