using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour
{
    [SerializeField] private GameObject selectionButtonPrefab;
    [SerializeField] private RectTransform content;
    [SerializeField] private GameObject customizationCanvas;
    [SerializeField] private float xMin, buttonWidth;

    private GameObject activePolytope;

    public void OnButtonPressed(GameObject polytope)
    {
        if (activePolytope == polytope)
        {
            customizationCanvas.SetActive(!customizationCanvas.activeInHierarchy);
        }
        else
        {
            customizationCanvas.SetActive(true);
        }
        
        activePolytope = polytope;
        customizationCanvas.GetComponent<Customization>().SetPolytope(activePolytope);
    }

    public void AddSelectable(GameObject refPolytope)
    {
        GameObject newButton = Instantiate(selectionButtonPrefab, content);
        Text text = newButton.GetComponentInChildren<Text>();
        if(text != null)
        {
            text.text = refPolytope.name;
        }
        Button button = newButton.GetComponent<Button>();
        if(button != null)
        {
            button.onClick.AddListener(delegate { OnButtonPressed(refPolytope); });
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
