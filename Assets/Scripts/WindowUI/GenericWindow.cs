using UnityEngine;
using UnityEngine.EventSystems;

public class GenericWindow : MonoBehaviour
{
    // 윈도우 열리자마자 포커스를 줄 오브젝트
    public GameObject firstSelected;

    protected WindowManager windowManager;

    public void Init(WindowManager mng)
    {
        mng = windowManager;
    }


    public virtual void Open()
    {
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelected); // firstSelected가 포커스된 오브젝트가 됨
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
