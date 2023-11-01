using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    public void OnDisable()
    {
        AudioManager.Instance.PlaySound("KeyTap");
        GameManager.Instance.DisplayIsActive = false;
        Time.timeScale = 1f;
    }
}
