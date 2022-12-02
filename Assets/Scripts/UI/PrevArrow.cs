using UnityEngine;
public class PrevArrow : MonoBehaviour, IClickable
{

    public Customization customizationScript;

    public void OnClick()
    {
        customizationScript.ChangePolytope(-1);
    }
}
