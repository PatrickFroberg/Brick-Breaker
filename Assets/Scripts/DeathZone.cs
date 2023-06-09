using Assets.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;

    private void OnCollisionEnter(Collision other)
    {
        AudioManager.Instance.PlayGameOverSound();
        Destroy(other.gameObject);
        Manager.GameOver();
    }
}
