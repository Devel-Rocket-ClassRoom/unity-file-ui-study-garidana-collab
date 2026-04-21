using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;

public class UiCharSlotList : MonoBehaviour
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

    // public readonly System.Comparison<SaveCharData>[] comparisons =
    // {
    //     // SortingOptions 들과 짝이 맞아야함
    //     (lhs,rhs) => lhs.Name.CompareTo(rhs.Name),
    //     (lhs,rhs) => rhs.obtainTime.CompareTo(lhs.obtainTime),
    //     (lhs,rhs) => lhs.itemData.StringName.CompareTo(rhs.itemData.StringName),
    //     (lhs,rhs) => rhs.itemData.StringName.CompareTo(lhs.itemData.StringName),
    //     (lhs,rhs) => lhs.itemData.Cost.CompareTo(rhs.itemData.Cost),
    //     (lhs,rhs) => rhs.itemData.Cost.CompareTo(lhs.itemData.Cost),

    // };

    // public readonly System.Func<SaveItemData, bool>[] filterings =
    // {
    //     (x) => true,
    //     (x) => x.itemData.Type == ItemTypes.Weapon,
    //     (x) => x.itemData.Type == ItemTypes.Equip,
    //     (x) => x.itemData.Type == ItemTypes.Consumable,
    //     (x) => x.itemData.Type != ItemTypes.Consumable
    // };

    public UiCharSlot prefab;
    public ScrollRect scrollRect;

    private List<UiCharSlot> uiCharList = new List<UiCharSlot>();

    private List<SaveCharData> saveCharDataList = new();

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
    public UnityEvent<SaveCharData> onSelectSlot;


    private void OnSelectSlot(SaveCharData saveCharData)
    {
        Debug.Log(saveCharData);
    }

    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);
    }

    private void OnDisable()
    {
        saveCharDataList = null;
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

    public void SetSaveCharDataList(List<SaveCharData> source)
    {
        saveCharDataList = source.ToList();
        UpdateSlots();
    }


    public List<SaveCharData> GetSaveCharDataList()
    {
        return saveCharDataList;
    }


    private void UpdateSlots()
    {
        //var list = saveCharDataList.Where(filterings[(int)filtering]).ToList();
        //list.Sort(comparisons[(int)sorting]);

        if (uiCharList.Count < saveCharDataList.Count)
        {
            for (int i = uiCharList.Count; i < saveCharDataList.Count; i++)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    // 이벤트 발행
                    onSelectSlot.Invoke(newSlot.SaveCharData);
                });

                uiCharList.Add(newSlot);

            }
        }

        for (int i = 0; i < uiCharList.Count; i++)
        {
            if (i < saveCharDataList.Count)
            {
                uiCharList[i].gameObject.SetActive(true);
                uiCharList[i].SetCharacter(saveCharDataList[i]);
            }
            else
            {
                uiCharList[i].gameObject.SetActive(false);
                uiCharList[i].SetEmpty();
            }

            selectedSlotIndex = -1;
            onUpdateSlots.Invoke();
        }
    }
    public void AddRandomItem()
    {
        saveCharDataList.Add(SaveCharData.GetRandomCharacter());
        UpdateSlots();
    }
    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }
        saveCharDataList.Remove(uiCharList[selectedSlotIndex].SaveCharData);
        UpdateSlots();
    }

}
