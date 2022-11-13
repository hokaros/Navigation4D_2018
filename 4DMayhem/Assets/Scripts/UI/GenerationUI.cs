using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationUI : MonoBehaviour
{
    [SerializeField] private PolytopeReader polytopeReader;
    private SelectionMenu selectionMenu;
    public void SimplexGenerateButtonClick()
    {
        GameObject polytope = polytopeReader.GenerateSimplex();
        selectionMenu.AddSelectable(polytope);
    }

    public void TesseractGenerateButtonClick()
    {
        GameObject polytope = polytopeReader.GenerateTesseract();
        selectionMenu.AddSelectable(polytope);
    }

    public void DodecaplexGenerateButtonClick()
    {
        GameObject polytope = polytopeReader.GenerateDodecaplex();
        selectionMenu.AddSelectable(polytope);
    }
    private void Awake()
    {
        selectionMenu = FindObjectOfType<SelectionMenu>();   
    }
}
