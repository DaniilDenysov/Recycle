using UnityEngine.UI;
using UnityEngine;

public class TropheyListPart : MonoBehaviour
{
    [SerializeField] private Image state, icon;
   
    public void assignTropheyInfo (bool locked,Sprite iconSprite)
    {
        icon.sprite = iconSprite;
        state.color = new Color(state.color.r,state.color.g,state.color.b,locked ? 0f : 0.5f);
    }
}
