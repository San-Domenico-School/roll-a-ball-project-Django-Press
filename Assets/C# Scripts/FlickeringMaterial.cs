using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringMaterial : MonoBehaviour
{
    public Material flickerMaterial; // The material to control (make sure this is public)
    public float flickerSpeed = 5f; // Speed of the flickering
    public float minIntensity = 0.1f; // Minimum emission intensity
    public float maxIntensity = 1.5f; // Maximum emission intensity

    private Renderer rend;
    private Color originalEmissionColor;

    void Start()
    {
        // Get the material's renderer
        rend = GetComponent<Renderer>();

        // Check if the object has a material with emission
        if (rend != null && rend.material.HasProperty("_EmissionColor"))
        {
            // Get the original emission color of the material
            originalEmissionColor = rend.material.GetColor("_EmissionColor");

            // Enable the emission keyword to make sure the material emits light
            rend.material.EnableKeyword("_EMISSION");
        }
    }

    void Update()
    {
        // Ensure the renderer and material are valid
        if (rend != null && rend.material != null)
        {
            // Calculate the intensity for flickering using Mathf.PingPong
            float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(Time.time * flickerSpeed, 1f));

            // Set the emission color with the calculated intensity
            rend.material.SetColor("_EmissionColor", originalEmissionColor * intensity);
        }
    }

    void OnDisable()
    {
        // Optionally, reset the emission when the object is disabled
        if (rend != null)
        {
            rend.material.SetColor("_EmissionColor", originalEmissionColor);
            rend.material.DisableKeyword("_EMISSION");
        }
    }
}

