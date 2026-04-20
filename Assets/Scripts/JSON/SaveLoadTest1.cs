using UnityEngine;


public class SaveLoadTest1 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data = new SaveDataV4();
            SaveLoadManager.Data.Name = "TEST1234";
            SaveLoadManager.Data.Gold = 4321;
            //SaveLoadManager.Data.ItemList = DataTableManager.ItemTable.GetItem();

            SaveLoadManager.Save();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (SaveLoadManager.Load())
            {
                Debug.Log(SaveLoadManager.Data.Name);
                Debug.Log(SaveLoadManager.Data.Gold);
                foreach (SaveItemData item in SaveLoadManager.Data.ItemList)
                {
                    Debug.Log(item.instanceId);
                    Debug.Log(item.itemData);
                    Debug.Log(item.obtainTime);
                }
            }
            else
            {
                Debug.Log("세이브 파일 없음");
            }
        }


    }
}
