using System;
using UnityEngine;
using UnityEngine.UI;

public class WindowActivator : MonoBehaviour
{
    [SerializeField] private Button CloseButton;
    [SerializeField] private GameObject Window;

    private void Start()
    {
        Window.SetActive(false);
    }

    private void OnEnable()
    {
        CloseButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        CloseButton.onClick.RemoveListener(Close);
    }

    public void Open()
    {
        Window.SetActive(true);
    }

    public void Close()
    { 
        Window.SetActive(false);
    }
}
