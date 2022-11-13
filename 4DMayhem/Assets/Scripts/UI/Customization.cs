using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class Customization : MonoBehaviour
{
    [SerializeField] Text title;

    private GameObject activeGameObject;
    private Polytope4 activePolytope;
    private Transform4 activeTransform;

    public void SetPolytope(GameObject polytope)
    {
        activeGameObject = polytope;
        activePolytope = polytope.GetComponent<Polytope4>();
        title.text = "Customize " + activePolytope.name;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
