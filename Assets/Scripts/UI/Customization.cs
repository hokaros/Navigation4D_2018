using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    [SerializeField] Text title;

    [SerializeField] private GameObject activeGameObject;
    private Polytope4 activePolytope;
    private Transform4 activeTransform;

    public void SetPolytope(GameObject polytope)
    {
        activeGameObject = polytope;
        activePolytope = polytope.GetComponent<Polytope4>();
        title.text = activePolytope.name;
    }

    void Start()
    {
        if (activeGameObject != null)
        {
            SetPolytope(activeGameObject);
        }
    }

    void Update()
    {
        
    }
}
