using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    private Slider sliderBar;
    private KJHPlayer player;
    public Text hpText;
    private GameObject startEndUI;

    private void Start()
    {
        player = FindObjectOfType<KJHPlayer>();
        sliderBar = GetComponent<Slider>();
        

        sliderBar.value = 1f;

        player.status = new PCStatus(Define.Data_ID_List.PC);
        player.currHp = player.status.MaxHp ;  // currHp 초기화 231016_박시연
        StartCoroutine(OnDamaged());
    }

    public IEnumerator OnDamaged()
    {
        // TODO : 플레이어 HP가 0이면 SliderBar의 Value도 0이다. 
        // 플레이어 hp가 감소되는 것은 프레임 마다 감소되게 한다.
        while(true)
        {
            // 플레이어의 현재 체력을 슬라이더의 값에 반영합니다.
            sliderBar.value = (float)player.currHp / player.status.MaxHp;

            // 텍스트UI
            hpText.text = $"HP : {player.currHp} / {player.status.MaxHp}";
            if(player.currHp <= 0)
            {
                Managers.GameManager.GameOverLose(); //게임 패배

                sliderBar.value = 0f;
                hpText.text = $"HP: 0 / {player.status.MaxHp}";
                yield break;
            }

            yield return null ;
        }
    }
}