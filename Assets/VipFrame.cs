using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VipFrame : MonoBehaviour
{
    [Header("Frame")]
    public PlayerState player;
    public Slider vipSlider;
    public TextMeshProUGUI level;

    // Start is called before the first frame update
    void OnEnable()
    {
        vipSlider.value = player.spantDia / ((player.DiaLevel + 1) * 1000);
        level.text = string.Format("VIP {0}LV", player.DiaLevel);
    }

}
