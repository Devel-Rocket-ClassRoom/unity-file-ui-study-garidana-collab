using UnityEngine;
using CsvHelper;
using System.IO;
using System.Globalization;
using UnityEngine.UI;


// 엑셀 유니코드 해결법
// 1. 데이터 테이블 Visual code에서 열고 UTF - 8 with BOM으로 인코딩하여 저장
public class CSVData
{
    public string Id { get; set; }
    public string String { get; set; }
}


public class CSVTest1 : MonoBehaviour
{
    public TextAsset textAsset;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 에셋을 드래그 드롭으로 참조하는게 아닌, 이름을 호출해서 가져옴 (Resources.Load);
            // 이 코드가 실행될때 에셋이 메모리에 올라감 (드래그드롭시에는 바로 올라가버림);
            TextAsset textAsset = Resources.Load<TextAsset> ("DataTables/StringTableKr");
            string csv = textAsset.text;
            using (var reader = new StringReader(csv))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<CSVData>();
                foreach (var record in records)
                {
                    Debug.Log($"{record.Id} : {record.String}");
                }
            }
        }
    }
}