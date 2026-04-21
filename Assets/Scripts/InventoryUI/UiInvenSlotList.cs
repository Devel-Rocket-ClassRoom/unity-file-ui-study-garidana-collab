using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;

public class UiInvenSlotList : MonoBehaviour
{
    // 아이템 인벤토리 생성

    // 분류 옵션
    public enum SortingOptions
    {
        // CompareTo 메서드 (델리게이트) 사용
        ObtainTimeAscending,
        ObtainTimeDescending,
        NameAcsending,
        NameDescending,
        CostAscending,
        CostDescending,
    }

    // 필터링 옵션
    public enum FilteringOptions
    {
        // Predicate (인자 받아서 bool 반환) 사용
        None,
        Weapon,
        Equip,
        Consumable,
        NonConsumable
    }

    public readonly System.Comparison<SaveItemData>[] comparisons =
    {
        // SortingOptions 들과 짝이 맞아야함
        (lhs, rhs) => lhs.obtainTime.CompareTo(rhs.obtainTime),
        (lhs,rhs) => rhs.obtainTime.CompareTo(lhs.obtainTime),
        (lhs,rhs) => lhs.itemData.StringName.CompareTo(rhs.itemData.StringName),
        (lhs,rhs) => rhs.itemData.StringName.CompareTo(lhs.itemData.StringName),
        (lhs,rhs) => lhs.itemData.Cost.CompareTo(rhs.itemData.Cost),
        (lhs,rhs) => rhs.itemData.Cost.CompareTo(lhs.itemData.Cost),

    };

    public readonly System.Func<SaveItemData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.itemData.Type == ItemTypes.Weapon,
        (x) => x.itemData.Type == ItemTypes.Equip,
        (x) => x.itemData.Type == ItemTypes.Consumable,
        (x) => x.itemData.Type != ItemTypes.Consumable
    };

    public UIInvenSlot prefab;
    public ScrollRect scrollRect;

    private List<UIInvenSlot> uiSlotList = new List<UIInvenSlot>();

    private List<SaveItemData> saveItemDataList = new();

    private SortingOptions sorting = SortingOptions.ObtainTimeAscending;
    private FilteringOptions filtering = FilteringOptions.None;

    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            if (sorting != value)
            {
                sorting = value;
                UpdateSlots();
            }
        }
    }

    public FilteringOptions Filtering
    {
        get => filtering;
        set
        {
            if (filtering != value)
            {
                filtering = value;
                UpdateSlots();
            }
        }
    }

    private int selectedSlotIndex = -1;

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveItemData> onSelectSlot;


    private void OnSelectSlot(SaveItemData saveItemData)
    {
        Debug.Log(saveItemData);
    }

    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);
    }

    private void OnDisable()
    {
        saveItemDataList = null;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddRandomItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RemoveItem();
        }
    }

    public void SetSaveItemDataList(List<SaveItemData> source)
    {
        saveItemDataList = source.ToList();
        UpdateSlots();
    }


    public List<SaveItemData> GetSaveItemDataList()
    {
        return saveItemDataList;
    }


    private void UpdateSlots()
    {
        var list = saveItemDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    // 이벤트 발행
                    onSelectSlot.Invoke(newSlot.SaveItemData);
                });

                uiSlotList.Add(newSlot);

            }
        }

        for (int i = 0; i < uiSlotList.Count; i++)
        {
            if (i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetItem(list[i]);
            }
            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }

            selectedSlotIndex = -1;
            onUpdateSlots.Invoke();
        }
    }
    public void AddRandomItem()
    {
        saveItemDataList.Add(SaveItemData.GetRandomItem());
        UpdateSlots();
    }
    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }
        saveItemDataList.Remove(uiSlotList[selectedSlotIndex].SaveItemData);
        UpdateSlots();
    }

}
