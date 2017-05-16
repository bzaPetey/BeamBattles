/*
 * Pilar.cs
 * Peter Laliberte - BurgZerg Arcade
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilar : MonoBehaviour
{
    [SerializeField] Light pointLight;
    [SerializeField] Vector3 transformOffset;


    private void Start()
   {
        OffsetPlacement();
        LightSetUp();
    }



    /// <summary>
    /// Setup the light on the pilar
    /// </summary>
    void LightSetUp()
    {
        int rnd = Random.Range(0, 3);

        if (rnd > 0)
            return;

        pointLight.gameObject.SetActive(true);
        AdjustLightColor();
        AdjustLightHeight();
    }


    /// <summary>
    /// Place the pilar on the ground. Each pilar model has a different offset.
    /// </summary>
    void OffsetPlacement()
    {
        Vector3 pos = transform.position;
        pos += transformOffset;
        transform.position = pos;
    }


    /// <summary>
    /// Randomly adjust the height of the light on the pilar so all of the lights are at a different spot.
    /// </summary>
    void AdjustLightHeight()
    {
        Vector3 pos = pointLight.transform.position;
        pos.y += 1 + Random.Range(0, pointLight.transform.localScale.y);
        pointLight.transform.position = pos;


    }



    /// <summary>
    /// Adjust the color of the light randomly so they are all different
    /// </summary>
    void AdjustLightColor()
    {
        pointLight.color = new Color(
                                    Random.Range(0, 256),       //red value
                                    Random.Range(0, 256),       //green value
                                    Random.Range(0, 256)        //blue value
                                    );
    }
}
