using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.IO;
using System.Linq;
using System.Globalization;

public abstract class DataTable 
{
    // 데이터 테이블의 경로를 정의하는 상수 문자열
    public static readonly string FormatPath = "DataTables/{0}";

    public abstract void Load(string filename);

    public static List<T> LoadCSV<T>(string csvText)
    {
        using (var reader = new StringReader(csvText))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var recrods = csvReader.GetRecords<T>();
            return recrods.ToList();
        }
    }
}
