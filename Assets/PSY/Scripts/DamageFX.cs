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

        Destroy(gameObject, 2.1f);

        StartCoroutine(DamageText());
    }

    private IEnumerator DamageText()
    {
        float timer = 0f;

        while(true)
        {
            transform.position += Vector3.up * Time.deltaTime;

            timer += Time.deltaTime;
            
            if(timer >= 2f)
            {
                yield break;
            }
            yield return null;
        }
    }

    //IEnumerator DamageText()
    //{
    //    float second = 0f;
    //    while (true)
    //    {
    //        second += Time.deltaTime;

    //        if ( second >= 2f )
    //        {
    //            Managers.Resource.Destroy( gameObject );
    //        }
    //        yield return null;
    //    }
    //}
}
