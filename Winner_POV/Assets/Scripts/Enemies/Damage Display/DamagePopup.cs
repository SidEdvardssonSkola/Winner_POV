using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 0.5f;
    [SerializeField] private float lifeTime = 0.25f;
    [SerializeField] private AudioSource sfx;

    [SerializeField] private string textBeforeNumber = "";
    public void SetText(int damage)
    {
        GetComponent<TextMeshPro>().text = textBeforeNumber + damage.ToString();

        Invoke("DestroyMe", lifeTime);
    }

    private void Update()
    {
        transform.Translate(0, floatSpeed * Time.deltaTime, 0);
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}
