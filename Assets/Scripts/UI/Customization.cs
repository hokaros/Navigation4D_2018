using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    [SerializeField] Text title;

    [SerializeField] private GameObject customizationMenu;
    [SerializeField] private GameObject positionPanel;
    [SerializeField] private GameObject rotationPanel;

    [SerializeField] private float toolInfluence;


    [SerializeField] private GameObject activeGameObject;

    [SerializeField] private Text xPosText;
    [SerializeField] private Text yPosText;
    [SerializeField] private Text zPosText;
    [SerializeField] private Text wPosText;

    [SerializeField] private Text xyRotText;
    [SerializeField] private Text xzRotText;
    [SerializeField] private Text xwRotText;
    [SerializeField] private Text yzRotText;
    [SerializeField] private Text ywRotText;
    [SerializeField] private Text zwRotText;

    [SerializeField] private Text toolInfluenceText;

    private Polytope4 activePolytope;
    private Transform4 activeTransform;
    private CustomizationAspect customizationAspect;

    private InputManager inputManager;


    private void EnableCustomization()
    {
        customizationMenu.SetActive(true);
        inputManager.DisableController();
    }
    private void DisableCustomization()
    {
        customizationMenu.SetActive(false);
        inputManager.EnableController();
    }


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

    private void UpdatePositionLabels()
    {
        Vector4 position = activeTransform.Position;
        xPosText.text = position.x.ToString();
        yPosText.text = position.y.ToString();
        zPosText.text = position.z.ToString();
        wPosText.text = position.w.ToString();
    }

    private void UpdateRotationLabels()
    {
        Vector6 rotation = activeTransform.Rotation;
        xyRotText.text = rotation.Xy.ToString();
        xzRotText.text = rotation.Xz.ToString();
        xwRotText.text = rotation.Xw.ToString();
        yzRotText.text = rotation.Yz.ToString();
        ywRotText.text = rotation.Yw.ToString();
        zwRotText.text = rotation.Zw.ToString();
    }


    public void SetPolytope(GameObject polytope)
    {
        activeGameObject = polytope;
        activePolytope = polytope.GetComponent<Polytope4>();
        activeTransform = activePolytope.GetComponent<Transform4>();
        ChangeCustomizationAspect(CustomizationAspect.POSITION);
        title.text = activePolytope.name;
        UpdatePositionLabels();
        UpdateRotationLabels();
        ChangeToolInfluence();
    }

    void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        if (activeGameObject != null)
        {
            SetPolytope(activeGameObject);
        }
    }

    public void OnPositionButtonClicked()
    {
        ActivatePanel(positionPanel);
        ChangeCustomizationAspect(CustomizationAspect.POSITION);
    }

    public void OnRotationButtonClicked()
    {
        ActivatePanel(rotationPanel);
        ChangeCustomizationAspect(CustomizationAspect.ROTATION);
    }

    public void OnChangeInfluenceButtonClicked()
    {
        ChangeCustomizationAspect(CustomizationAspect.CHANGE_INFLUENCE);
    }

    private void ChangeCustomizationAspect(CustomizationAspect aspect)
    {
        customizationAspect = aspect;
    }

    private void UpdateRotation()
    {
        activeTransform.RotateInLocal(Axis.x, Axis.y, inputManager.GetXYRotation() * Time.deltaTime * toolInfluence);
        activeTransform.RotateInLocal(Axis.x, Axis.z, inputManager.GetXZRotation() * Time.deltaTime * toolInfluence);
        activeTransform.RotateInLocal(Axis.x, Axis.w, inputManager.GetXWRotation() * Time.deltaTime * toolInfluence);
        activeTransform.RotateInLocal(Axis.y, Axis.z, inputManager.GetYZRotation() * Time.deltaTime * toolInfluence);
        activeTransform.RotateInLocal(Axis.y, Axis.w, inputManager.GetYWRotation() * Time.deltaTime * toolInfluence);
        activeTransform.RotateInLocal(Axis.z, Axis.w, inputManager.GetZWRotation() * Time.deltaTime * toolInfluence);
    }

    private void ChangeToolInfluence()
    {
        toolInfluence += inputManager.GetXAxis() * Time.deltaTime * toolInfluence;
        toolInfluenceText.text = toolInfluence.ToString();
    }

    private void UpdatePosition()
    {
        //Debug.Log($"{Input.GetAxis("Forward")}, {Input.GetAxis("4D_W")}");
        Vector4 movement = activeTransform.Forward * inputManager.GetZAxis(); //Input.GetAxis("Vertical");
        movement += activeTransform.Right * inputManager.GetXAxis(); //Input.GetAxis("Horizontal");
        movement += activeTransform.Up * inputManager.GetYAxis(); //Input.GetAxis("Forward");
        movement += activeTransform.WPositive * inputManager.GetWAxis(); // Input.GetAxis("4D_W");
        movement *= Time.deltaTime;
        movement *= toolInfluence;
        activeTransform.Position += movement;
    }

    void Update()
    {
        if (customizationMenu.activeInHierarchy)
        {
            if (activeTransform != null)
            {
                switch (customizationAspect)
                {
                    case CustomizationAspect.POSITION:
                        UpdatePosition();
                        UpdatePositionLabels();
                        break;
                    case CustomizationAspect.ROTATION:
                        UpdateRotation();
                        UpdateRotationLabels();
                        break;
                    case CustomizationAspect.CHANGE_INFLUENCE:
                        ChangeToolInfluence();
                        break;
                    default:
                        break;
                }
            }
        }
        


        // Toggle customization
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (customizationMenu.activeInHierarchy)
            {
                DisableCustomization();
            }
            else
            {
                EnableCustomization();
            }
        }
    }
}


public enum CustomizationAspect
{
    POSITION,
    ROTATION,
    CHANGE_INFLUENCE,
}