using UnityEngine;
[System.Serializable]
public class ObjectSaveData
{
    // 어떤 형태의 3D 오브젝트인지 구분할 필드
    public string prefabName;
    public Vector3 pos;
    public Quaternion rot;
    public Color color;
    public Vector3 scale;
}