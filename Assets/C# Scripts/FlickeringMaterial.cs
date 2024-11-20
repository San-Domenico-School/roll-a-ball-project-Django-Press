using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringMaterial : MonoBehaviour
{
    public Material flickerMaterial; // The material to control
    public float flickerSpeed = 5f; // Speed of the flickering
    public float minIntensity = 0.1f; // Minimum emission intensity
    public float maxIntensity = 1.5f; // Maximum emission intensity

    private Renderer rend;
    private Color originalEmissionColor;

    void Start()
    {
        // Get the material's renderer
        rend = GetComponent<Renderer>();

        // Get the original emission color of the material
        originalEmissionColor = flickerMaterial.GetColor("_EmissionColor");

        // Enable the emission keyword to make sure the material emits light
        flickerMaterial.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        // Calculate the intensity for flickering using Mathf.PingPong
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(Time.time * flickerSpeed, 1f));

        // Set the emission color with the calculated intensity
        flickerMaterial.SetColor("_EmissionColor", originalEmissionColor * intensity);
    }
}

