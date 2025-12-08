using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollbarController : MonoBehaviour
{
    private Scrollbar scrollbar;
    [SerializeField] private float scrollbarSpeed = 3.5f;
    private float oldValue;

    private void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
        oldValue = scrollbar.value;
    }

    public void BoostScrollbar()
    {
        scrollbar.value = Mathf.Clamp01(scrollbar.value + ((scrollbar.value - oldValue) * scrollbarSpeed));

        oldValue = scrollbar.value;
    }
}
