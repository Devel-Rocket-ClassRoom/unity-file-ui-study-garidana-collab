using UnityEngine;
using UnityEngine.EventSystems;



public class GenericWindowCopy : MonoBehaviour
{
    public GameObject firstSelected;

    protected WindowManagerCopy windowManager;

    public void Init(WindowManagerCopy manager)
    {
        manager = windowManager;
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}