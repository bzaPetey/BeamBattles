using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilar : MonoBehaviour {
    [SerializeField] Light light;
    [SerializeField] Vector3 offset;



    private void Start()
    {
        OffsetPlacement();

        int rnd = Random.Range(0, 3);

        if (rnd > 0)
            return;

        light.gameObject.SetActive(true);
        AdjustLightColor();
        AdjustLightHeight();
    }


    void OffsetPlacement()
    {
        Vector3 pos = transform.position;
        pos += offset;
        transform.position = pos;
    }


    void AdjustLightHeight()
    {
        float lightOffset = Random.Range(0, light.transform.localScale.y);
        Vector3 pos = light.transform.position;
        pos.y += 1 + lightOffset;
        light.transform.position = pos;


    }



    void AdjustLightColor()
    {
        int r = Random.Range(0, 256);
        int g = Random.Range(0, 256);
        int b = Random.Range(0, 256);

        light.color = new Color(r, g, b);
    }
}
