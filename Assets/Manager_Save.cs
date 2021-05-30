using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Manager_Save : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;  // 저장할 폴더 경로
    private string SAVE_FILENAME = "/SaveFile.txt"; // 파일 이름

    public PlayerState thePlayer; // 플레이어의 위치, 회전값 가져오기 위해 필요


    public Items_Info[] AllItem;
    public BuffInfo[] AllBuff;

    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Save/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY)) // 해당 경로가 존재하지 않는다면
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY); // 폴더 생성(경로 생성)
    }

    // Start is called before the first frame update
    public void SaveData()
    {

        // 플레이어 위치, 회전값 저장
        saveData.playerPos = thePlayer.transform.position;
        saveData.playerRot = thePlayer.transform.rotation.eulerAngles;

        for (int i = 0; i < Manager.instance.manager_Inven.slot_BagFrame.Length; i++)
        {
            if (Manager.instance.manager_Inven.slot_BagFrame[i].childCount != 0)
            {
                string item = Manager.instance.manager_Inven.slot_BagFrame[i].GetChild(0).name;
                saveData.invenItemName.Add(item);
            }
        }

        // 버프 정보 저장
        for (int i = 0; i < Manager.instance.manager_Buff.slot_Buff.Length; i++)
        {
            if (Manager.instance.manager_Buff.slot_Buff[i].childCount != 0)
            {
                string buff = Manager.instance.manager_Buff.slot_Buff[i].GetChild(0).name;
                saveData.buffName.Add(buff);
            }
        }

        // 최종 전체 저장
        string json = JsonUtility.ToJson(saveData); // 제이슨화

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("저장 완료");
        Debug.Log(json);
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            // 전체 읽어오기
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            // 플레이어 위치, 회전 로드
            thePlayer.transform.position = saveData.playerPos;
            thePlayer.transform.eulerAngles = saveData.playerRot;

            // 인벤토리 로드
            for (int i = 0; i < saveData.invenItemName.Count; i++)
            {
                if(saveData.invenItemName[i] != null)
                {
                    for(int j = 0; j < AllBuff.Length; j++)
                    {
                        if(AllItem[j].name  == saveData.invenItemName[i])
                        {
                            Instantiate(AllBuff[j].gameObject, Manager.instance.manager_Inven.slot_BagFrame[i]);
                        }
                    }
                }
            }

            // 버프 로드
            for (int i = 0; i < saveData.buffName.Count; i++)
            {
                if (saveData.buffName[i] != null)
                {
                    for (int j = 0; j < AllBuff.Length; j++)
                    {
                        if (AllBuff[j].name + "(Clone)" == saveData.buffName[i])
                        {
                            Manager.instance.manager_Buff.AddBuff(AllBuff[j], i);
                        }
                    }
                }
            }

            Debug.Log("로드 완료");
        }
        else
            Debug.Log("세이브 파일이 없습니다.");
    }
}

[System.Serializable] // 직렬화 해야 한 줄로 데이터들이 나열되어 저장 장치에 읽고 쓰기가 쉬워진다.
public class SaveData
{
    public Vector3 playerPos;
    public Vector3 playerRot;

    public List<string> invenItemName = new List<string>();

    public List<string> buffName = new List<string>();
}