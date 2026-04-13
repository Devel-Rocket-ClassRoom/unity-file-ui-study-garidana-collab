
using System.IO;
using System.Xml.Linq;
using Unity.Burst.CompilerServices;
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
            // 만들어진 폴더가 없을 시 폴더 생성
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
            // 파일 스트림 생성 (FileMode.Open은 쓰기/읽기 모두 가능)
            using (FileStream fs = new FileStream(secretFilePath, FileMode.Open))
            {
                Debug.Log($"읽기 가능 : {fs.CanRead}");
                Debug.Log($"쓰기 가능 : {fs.CanWrite}");

                // File Stream (Open) 중 암호화
                // FileStream writer 생성 (FileMode.Create)
                using (FileStream fsWriter = new (encryptedPath, FileMode.Create))
                {
                    int b;
                    while ((b = fs.ReadByte()) != -1)
                    {
                        // 지정된 키 값(0xAB)으로 암호화 (XOR 연산) 진행
                        fsWriter.WriteByte((byte)(b ^ 0xAB));
                    }
                }
                Debug.Log($"암호화 완료 (파일 크기 : {new FileInfo(encryptedPath).Length} bytes)");
            }
        }

        // 2-3 복호화 D
        if (Input.GetKeyDown(KeyCode.D))
        {
            using (FileStream dfs = new FileStream(encryptedPath, FileMode.Open))
            {
                Debug.Log ($"읽기 가능 : {dfs.CanRead}");
                Debug.Log($"쓰기 가능 : {dfs.CanWrite}");

                using (FileStream dfsWriter = new (decryptedPath, FileMode.Create))
                {
                    int b;
                    while ((b = dfs.ReadByte()) != -1)
                    {
                        // 키 다시 적용하여 암호해독
                        dfsWriter.WriteByte((byte)(b ^ 0xAB));
                    }
                }
            }
            // 암호완료 파일 읽기
            string decryptedText = File.ReadAllText(decryptedPath);
            // 원본 파일 읽기
            string originalText = File.ReadAllText(secretFilePath);

            Debug.Log ($"복호화 완료");
            Debug.Log ($"복호화 결과 : {decryptedText}");
            Debug.Log ($"원본과 일치 : {decryptedText == originalText}");
        }
    }
}
