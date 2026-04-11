using System.IO;
using UnityEngine;

public class FileEncrypt : MonoBehaviour
{
    string secretFileDir;
    string secretFilePath;
    string encryptedPath;
    string decryptedPath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 2-1 원본 파일 경로 생성
        secretFileDir = Path.Combine(Application.persistentDataPath, "SecretData");
        secretFilePath = Path.Combine(Application.persistentDataPath, "SecretData", "secretFile.txt");
        
        // encrypted 암호화할 파일 경로 ??
        encryptedPath = Path.Combine(Application.persistentDataPath, "SecretData", "encryptedFile.dat");
        // decrypted 복호화할 파일 경로
        decryptedPath = Path.Combine(Application.persistentDataPath, "SecretData", "decryptedFile.dat");
        
        // 2-1 원본 파일 폴더 생성
        if (Directory.Exists(secretFileDir))
        {
            Debug.Log($"폴더가 이미 존재합니다 : {secretFilePath}");
        }
        else
        {
            Directory.CreateDirectory(secretFileDir);
            Debug.Log ($"폴더 생성됨 : {secretFileDir}");
        }

        // 2-1 영문 메세지 파일 생성.
        string secretText = "Hello Unity World";
        File.WriteAllText(secretFilePath, secretText);
        
        // 2-1 폴더 내의 파일 목록 출력
        string[] files  = Directory.GetFiles(secretFileDir);
        foreach (var file in files)
        {
            Debug.Log($"파일 출력 : {Path.GetFileName(file)} ({Path.GetExtension(file)})");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 2-2 FileStream으로 secretFile.txt 열기
        if (Input.GetKeyDown(KeyCode.E))
        {
            using (FileStream fs = new FileStream(secretFilePath, FileMode.Open))
            {
                Debug.Log($"읽기 가능 : {fs.CanRead}");
                Debug.Log($"쓰기 가능 : {fs.CanWrite}");

                // 첫 번째 바이트 읽기 (1바이트씩 읽기)
                int b = fs.ReadByte();
                Debug.Log($"첫 번째 바이트 : {b} ('{(char)b}')");

                byte[] buffer = new byte[15];
                int bytesRead = fs.Read(buffer, 0 , buffer.Length);
                Debug.Log ($"읽은 바이트 수 : {bytesRead}");
            }
            

        }
    }
}
