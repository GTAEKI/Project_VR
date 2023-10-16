using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// UI 데미지 표시 FX
public class DamageFX : MonoBehaviour
{
    private Canvas Canvas;
    private TextMeshProUGUI textDamage;

    private void Start()
    {
        Canvas = GetComponent<Canvas>();
        textDamage = Canvas.GetComponentInChildren<TextMeshProUGUI>();

        StartCoroutine(DamageText());
    }

    IEnumerator DamageText()
    {
        float second = 0f;
        while (true)
        {
            second += Time.deltaTime;

            if ( second >= 2f )
            {
                Destroy( gameObject );
            }
            yield return null;
        }
    }
}
