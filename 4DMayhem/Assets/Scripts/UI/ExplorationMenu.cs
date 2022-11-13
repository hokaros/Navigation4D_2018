using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationMenu : MonoBehaviour
{
    [SerializeField] private Slider faceOpacitySlider;
    [SerializeField] private Slider edgeWidthSlider;
    private ShaderVisualizer[] shaderVisualizers;


    public void OnFaceOpacitySlider(float value)
    {
        foreach(ShaderVisualizer shaderVisualizer in shaderVisualizers)
        {
            shaderVisualizer.UpdateFaceOpacity(value * value);
        }
    }

    public void OnEdgeWidthSlider(float value)
    {
        foreach (ShaderVisualizer shaderVisualizer in shaderVisualizers)
        {
            shaderVisualizer.UpdateEdgeWidth(1 - Mathf.Lerp(0, 0.15f, value/edgeWidthSlider.maxValue));
        }
    }


    void Start()
    {
        shaderVisualizers = (ShaderVisualizer[])FindObjectsOfType(typeof(ShaderVisualizer));
    }

    void Update()
    {

    }
}
