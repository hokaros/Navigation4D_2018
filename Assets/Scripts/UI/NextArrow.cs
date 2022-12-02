using UnityEngine;
public class NextArrow : MonoBehaviour, IClickable {

    public Customization customizationScript;

    public void OnClick()
    {
        Debug.Log("Next Arrow clicked!");
        customizationScript.ChangePolytope(1);
    }
}
