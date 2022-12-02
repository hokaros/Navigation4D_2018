using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationUI : MonoBehaviour
{
    [SerializeField] private PolytopeReader polytopeReader;
    private Customization customizationScript;

    private void OnPolytopeGenerated(GameObject polytope)
    {
        polytope.transform.SetParent(customizationScript.polytopesParent);
        customizationScript.SetPolytope(polytope);
    }

    public void SimplexGenerateButtonClick()
    {
        GameObject polytope = polytopeReader.GenerateSimplex();
        OnPolytopeGenerated(polytope);
    }

    public void TesseractGenerateButtonClick()
    {
        GameObject polytope = polytopeReader.GenerateTesseract();
        OnPolytopeGenerated(polytope);
    }

    public void DodecaplexGenerateButtonClick()
    {
        GameObject polytope = polytopeReader.GenerateDodecaplex();
        OnPolytopeGenerated(polytope);
    }
    private void Awake()
    {
        customizationScript = FindObjectOfType<Customization>();
    }
}
