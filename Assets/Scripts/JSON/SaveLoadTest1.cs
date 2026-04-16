using UnityEngine;


public class SaveLoadTest1 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data = new SaveDataV3();
            SaveLoadManager.Data.Name = "TEST1234";
            SaveLoadManager.Data.Gold = 4321;
            SaveLoadManager.Data.ItemId = DataTableManager.ItemTable.GetItem();

            SaveLoadManager.Save();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (SaveLoadManager.Load())
            {
                Debug.Log(SaveLoadManager.Data.Name);
                Debug.Log(SaveLoadManager.Data.Gold);
                foreach (var item in SaveLoadManager.Data.ItemId)
                {
                    Debug.Log(DataTableManager.ItemTable.Get(item).Name);
                }
            }
            else
            {
                Debug.Log("세이브 파일 없음");
            }
        }


    }
}
