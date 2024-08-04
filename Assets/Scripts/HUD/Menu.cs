using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName;
    public MenuPlacement menuPlacement;

    private bool isMenuOpen = false;
    public bool IsMenuOpen => isMenuOpen;

    public virtual void OpenMenu()
    {
        gameObject.SetActive(true);
        isMenuOpen = true;
    }
    
    public virtual void CloseMenu()
    {
        gameObject.SetActive(false);
        isMenuOpen = false;
    }

    public enum MenuPlacement
    {
        LeftMenuItem,
        RightMenuItem,
        MainScreenMenu
    }
}
