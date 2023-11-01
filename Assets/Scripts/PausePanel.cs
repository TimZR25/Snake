using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    public void Quit() 
    {
        Application.Quit();
    }
    private void OnDisable()
    {
        AudioManager.Instance.PlaySound("KeyTap");
    }
}
