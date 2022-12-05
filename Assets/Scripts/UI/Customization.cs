using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Customization : MonoBehaviour
{
    [SerializeField] Text title;

    [SerializeField] private GameObject customizationMenu;
    [SerializeField] private GameObject positionPanel;
    [SerializeField] private GameObject rotationPanel;

    [SerializeField] private float toolInfluence;

    public Transform polytopesParent;

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

    [SerializeField] private Text xxRotMatrixText;
    [SerializeField] private Text xyRotMatrixText;
    [SerializeField] private Text xzRotMatrixText;
    [SerializeField] private Text xwRotMatrixText;
    [SerializeField] private Text yxRotMatrixText;
    [SerializeField] private Text yyRotMatrixText;
    [SerializeField] private Text yzRotMatrixText;
    [SerializeField] private Text ywRotMatrixText;
    [SerializeField] private Text zxRotMatrixText;
    [SerializeField] private Text zyRotMatrixText;
    [SerializeField] private Text zzRotMatrixText;
    [SerializeField] private Text zwRotMatrixText;
    [SerializeField] private Text wxRotMatrixText;
    [SerializeField] private Text wyRotMatrixText;
    [SerializeField] private Text wzRotMatrixText;
    [SerializeField] private Text wwRotMatrixText;

    [SerializeField] private Text toolInfluenceText;

    private GameObject activeGameObject;
    private Polytope4 activePolytope;
    private Transform4 activeTransform;
    private CustomizationAspect customizationAspect;

    private InputManager inputManager;

    private List<GameObject> polytopes;


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
        xPosText.text = position.x.ToString("F2");
        yPosText.text = position.y.ToString("F2");
        zPosText.text = position.z.ToString("F2");
        wPosText.text = position.w.ToString("F2");
    }

    private void UpdateRotationLabels()
    {
        Vector6 rotation = activeTransform.Rotation;
        xyRotText.text = rotation.Xy.ToString("F2");
        xzRotText.text = rotation.Xz.ToString("F2");
        xwRotText.text = rotation.Xw.ToString("F2");
        yzRotText.text = rotation.Yz.ToString("F2");
        ywRotText.text = rotation.Yw.ToString("F2");
        zwRotText.text = rotation.Zw.ToString("F2");
    }

    private void UpdateRotationMatrixLabels()
    {
        Matrix4x4 rotation = activeTransform.RotationMatrix;
        xxRotMatrixText.text = rotation.m00.ToString("F2");
        xyRotMatrixText.text = rotation.m01.ToString("F2");
        xzRotMatrixText.text = rotation.m02.ToString("F2");
        xwRotMatrixText.text = rotation.m03.ToString("F2");

        yxRotMatrixText.text = rotation.m10.ToString("F2");
        yyRotMatrixText.text = rotation.m11.ToString("F2");
        yzRotMatrixText.text = rotation.m12.ToString("F2");
        ywRotMatrixText.text = rotation.m13.ToString("F2");

        zxRotMatrixText.text = rotation.m20.ToString("F2");
        zyRotMatrixText.text = rotation.m21.ToString("F2");
        zzRotMatrixText.text = rotation.m22.ToString("F2");
        zwRotMatrixText.text = rotation.m23.ToString("F2");

        wxRotMatrixText.text = rotation.m30.ToString("F2");
        wyRotMatrixText.text = rotation.m31.ToString("F2");
        wzRotMatrixText.text = rotation.m32.ToString("F2");
        wwRotMatrixText.text = rotation.m33.ToString("F2");
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
        UpdateRotationMatrixLabels();
        ChangeToolInfluence();
    }

    void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        polytopes = new List<GameObject>();

        foreach(Transform polytope in polytopesParent)
        {
            polytopes.Add(polytope.gameObject);
        }
        if (polytopes.Count > 0)
        {
            activeGameObject = polytopes[0];
        }
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
        if (customizationAspect == CustomizationAspect.CHANGE_INFLUENCE)
        {
            ChangeCustomizationAspect(CustomizationAspect.POSITION);
        }
        else
        {
            ChangeCustomizationAspect(CustomizationAspect.CHANGE_INFLUENCE);
        }
    }

    public void AddPolytope(GameObject polytope)
    {
        polytopes.Add(polytope);
    }

    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ChangePolytope(int indexDiff)
    {
        if (polytopes.Count <= 0) return;

        if (activeGameObject == null)
        {
            SetPolytope(polytopes[0]);
        }
        else
        {
            int currIndex = polytopes.IndexOf(activeGameObject);
            SetPolytope(polytopes[(currIndex + polytopes.Count + indexDiff) % polytopes.Count]);
        }
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
        toolInfluenceText.text = toolInfluence.ToString("F2");
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
                if(customizationAspect==CustomizationAspect.POSITION
                    ||customizationAspect==CustomizationAspect.ROTATION)
                {
                    UpdatePosition();
                    UpdatePositionLabels();
                    UpdateRotation();
                    UpdateRotationMatrixLabels();
                }
                else if(customizationAspect==CustomizationAspect.CHANGE_INFLUENCE)
                {
                    ChangeToolInfluence();
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