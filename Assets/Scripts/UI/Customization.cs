using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    [SerializeField] Text title;

    [SerializeField] private GameObject positionPanel;
    [SerializeField] private GameObject rotationPanel;


    [SerializeField] private GameObject activeGameObject;
    private Polytope4 activePolytope;
    private Transform4 activeTransform;


    private void DeactivateOptions()
    {
        positionPanel.SetActive(false);
        rotationPanel.SetActive(false);
    }

    private void ActivatePanel(GameObject panel)
    {
        DeactivateOptions();
        panel.SetActive(true);
    }


    public void SetPolytope(GameObject polytope)
    {
        activeGameObject = polytope;
        activePolytope = polytope.GetComponent<Polytope4>();
        activeTransform = activePolytope.transform4;
        title.text = activePolytope.name;
    }

    void Start()
    {
        if (activeGameObject != null)
        {
            SetPolytope(activeGameObject);
        }
    }

    public void OnPositionButtonClicked()
    {
        ActivatePanel(positionPanel);
    }

    public void OnRotationButtonClicked()
    {
        ActivatePanel(rotationPanel);
    }

    void Update()
    {
        
    }
}
