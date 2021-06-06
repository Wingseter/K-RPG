using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Manager_Save : MonoBehaviour
{
    public GameObject charSelectFrame;
    public GameObject[] selectArrow;
    public SlotFrame[] savelist;

    private SaveData saveData;
    private int select;
    private string SAVE_DATA_DIRECTORY;  // 저장할 폴더 경로
    private string[] SAVE_FILENAME = { "/SaveFile1.txt", "/SaveFile2.txt", "/SaveFile3.txt" }; // 파일 이름

    public PlayerState thePlayer; // 플레이어의 위치, 회전값 가져오기 위해 필요


    public Items_Info[] AllItem;
    public BuffInfo[] AllBuff;

    void Start()
    {
        select = -1;
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Save/";
        charSelectFrame.SetActive(true);

        if (!Directory.Exists(SAVE_DATA_DIRECTORY)) // 해당 경로가 존재하지 않는다면
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY); // 폴더 생성(경로 생성)

        for (int i = 0; i < SAVE_FILENAME.Length; i++)
        {
            if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME[i]))
            {
                savelist[i].SetActive();
            }
            else
            {
                savelist[i].SetDisable();
            }
        }
    }

    public void selectSlotBtn(int num)
    {
        select = num;
        for(int i = 0; i < selectArrow.Length; i++)
        {
            selectArrow[i].SetActive(false);
        }
        selectArrow[num].SetActive(true);
    }

    public void deleteBtn(int num)
    {
        if(num == select)
        {
            selectArrow[num].SetActive(false);
            select = -1;
        }
        
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.btnB);

        File.Delete(SAVE_DATA_DIRECTORY + SAVE_FILENAME[num]);
        savelist[num].SetDisable();
    }

    public void newSlotBtn(int num)
    {
        saveData = new SaveData();
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.btnA);

        saveData.firstTime = true;
        string json = JsonUtility.ToJson(saveData); // 제이슨화

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME[num], json);
        savelist[num].SetActive();
    }

    // Start is called before the first frame update
    public void SaveData()
    {
        saveData = new SaveData();

        saveData.firstTime = false;
        // 플레이어 위치, 회전값 저장
        saveData.playerPos = thePlayer.transform.position;
        saveData.playerRot = thePlayer.transform.rotation.eulerAngles;

        saveData.lev = thePlayer.lev;     // 래밸

        saveData.hp = thePlayer.hp;    // 체력
        saveData.hp_Cur = thePlayer.hp_Cur;    // 현재 체력

        saveData.atk = thePlayer.atk;     // 공격력
        saveData.def = thePlayer.def;     // 방어력
        saveData.cri = thePlayer.cri;   // 크리티컬

        saveData.exp_Max = thePlayer.exp_Max;   // 맥스 경험치
        saveData.exp_Cur = thePlayer.exp_Cur;   // 현재 경험치
        saveData.exp_Multiply = thePlayer.exp_Multiply;

        saveData.mana = thePlayer.mana;
        saveData.mana_Cur = thePlayer.mana_Cur;

        saveData.spantDia = thePlayer.spantDia;
        saveData.DiaLevel = thePlayer.DiaLevel;

        saveData.Dia = Manager.instance.manager_Inven.dia;
        saveData.Gold = Manager.instance.manager_Inven.gold;
        saveData.invenSize = Manager.instance.manager_Inven.invenSize;

        // 인벤 저장
        for (int i = 0; i < Manager.instance.manager_Inven.slot_BagFrame.Length; i++)
        {
            if (Manager.instance.manager_Inven.slot_BagFrame[i].childCount != 0)
            {
                string item = Manager.instance.manager_Inven.slot_BagFrame[i].GetChild(0).GetComponent<Items_Info>().name_Item;
                if (Manager.instance.manager_Inven.slot_BagFrame[i].GetChild(0).GetComponent<Items_Info>().equipped)
                {
                    saveData.equipItemNum.Add(i);
                }
                saveData.invenItemName.Add(item);
                saveData.invenItemPlace.Add(i);
            }
        }


        // 버프 정보 저장
        for (int i = 0; i < Manager.instance.manager_Buff.slot_Buff.Length; i++)
        {
            if (Manager.instance.manager_Buff.slot_Buff[i].childCount != 0)
            {
                string buff = Manager.instance.manager_Buff.slot_Buff[i].GetChild(0).GetComponent<BuffInfo>().name_Buff;
                saveData.buffName.Add(buff);
            }
        }

        // 최종 전체 저장
        string json = JsonUtility.ToJson(saveData); // 제이슨화

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME[select], json);

    }

    private void Go()
    {
        Manager.instance.manager_SE.BGM.SetActive(true);
        charSelectFrame.SetActive(false);
    }

    public void LoadData()
    {
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.btnA);
        if(select == -1)
        {
            Manager.instance.manager_Popup.notice.text = "Select the slot first";
            Manager.instance.manager_Popup.notice.gameObject.SetActive(true);
            return;
        }
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME[select]))
        {

            // 전체 읽어오기
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME[select]);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);
            if (saveData.firstTime == true)
            {
                Go();
                return;
            }
           

            // 인벤토리 로드
            for (int i = 0; i < saveData.invenItemName.Count; i++)
            {
                if (saveData.invenItemName[i] != null)
                {
                    for (int j = 0; j < AllItem.Length; j++)
                    {
                        if (AllItem[j].name_Item == saveData.invenItemName[i])
                        {
                            GameObject obj = Instantiate(AllItem[j].gameObject, Manager.instance.manager_Inven.slot_BagFrame[saveData.invenItemPlace[i]]);
                            obj.GetComponent<items_action>().inBag = true;
                        }
                    }
                }
            }
            Items_Info info;
            for (int i = 0; i < saveData.equipItemNum.Count; i++)
            {
                
                info = Manager.instance.manager_Inven.slot_BagFrame[saveData.equipItemNum[i]].GetChild(0).GetComponent<Items_Info>();
                info.equipped = true;
                info.transform.GetChild(0).gameObject.SetActive(true);

                Transform obj = Instantiate(Manager.instance.manager_Inven.slot_BagFrame[saveData.equipItemNum[i]].GetChild(0),
                    Manager.instance.manager_Inven.gameObject.GetComponent<Equip>().slot_Equip[info.equipNum]);
                obj.GetChild(0).gameObject.SetActive(false);
                Manager.instance.manager_Inven.gameObject.GetComponent<Equip>().cur_Equip[info.equipNum] = info;
            }
            // 버프 로드
            for (int i = 0; i < saveData.buffName.Count; i++)
            {
                if (saveData.buffName[i] != null)
                {
                    for (int j = 0; j < AllBuff.Length; j++)
                    {
                        if (AllBuff[j].name_Buff == saveData.buffName[i])
                        {
                            Manager.instance.manager_Buff.AddBuff(AllBuff[j], i);
                        }
                    }
                }
            }

            thePlayer.exp_Multiply = saveData.exp_Multiply;

            thePlayer.lev = saveData.lev;     // 래밸

            thePlayer.hp = saveData.hp;    // 체력
            thePlayer.hp_Cur = saveData.hp_Cur;    // 현재 체력

            thePlayer.atk = saveData.atk;     // 공격력
            thePlayer.def = saveData.def;     // 방어력
            thePlayer.cri = saveData.cri;   // 크리티컬

            thePlayer.exp_Max = saveData.exp_Max;   // 맥스 경험치
            thePlayer.exp_Cur = saveData.exp_Cur;   // 현재 경험치

            thePlayer.mana = saveData.mana;
            thePlayer.mana_Cur = saveData.mana_Cur;

            thePlayer.spantDia = saveData.spantDia;
            thePlayer.DiaLevel = saveData.DiaLevel;

            Manager.instance.manager_Inven.dia = saveData.Dia;
            Manager.instance.manager_Inven.gold = saveData.Gold;
            Manager.instance.manager_Inven.invenSize = saveData.invenSize;
            // 플레이어 위치, 회전 로드
            thePlayer.transform.position = saveData.playerPos;
            thePlayer.transform.eulerAngles = saveData.playerRot;

            Go();
        }
    }
}

[System.Serializable] // 직렬화 해야 한 줄로 데이터들이 나열되어 저장 장치에 읽고 쓰기가 쉬워진다.
public class SaveData
{
    public bool firstTime;
    public Vector3 playerPos;
    public Vector3 playerRot;

    public int lev;     // 래밸

    public float hp;    // 체력
    public float hp_Cur;    // 현재 체력

    public int atk;     // 공격력
    public int def;     // 방어력
    public float cri;   // 크리티컬

    public float exp_Max;   // 맥스 경험치
    public float exp_Cur;   // 현재 경험치

    public float exp_Multiply;

    public float mana;
    public float mana_Cur;

    public float spantDia;
    public float DiaLevel;

    public int invenSize;
    public int Dia;
    public int Gold;
    public List<int> equipItemNum = new List<int>();
    public List<int> invenItemPlace = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<string> buffName = new List<string>();

}