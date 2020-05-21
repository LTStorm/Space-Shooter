using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sử dụng trong mỗi đối tượng Gift được tạo ra để chứa thông tin về các phần thưởng của Gift 
public class PrizeDealer : MonoBehaviour {

    [Header("Prize")]
    [SerializeField] private int health = 0;
    [SerializeField] private int score = 0;
    [SerializeField] private bool upgrade = false;
    [Header("VFX")]
    [SerializeField] private GameObject VFX;
    [SerializeField] private float time = 1f;
    [Header("SFX")]
    [SerializeField] private AudioClip SFX;
    [SerializeField] [Range(0, 1)] private float SFXVolume;
    public int GetHeath()
    {
        return health;
    }

    public int GetScore()
    {
        return score;
    }

    public bool isUprade()
    {
        return upgrade;
    }

    // Hành vi khi biến mất người chơi nhận thưởng
    public void Hit()
    {
        AudioSource.PlayClipAtPoint(SFX, Camera.main.transform.position, SFXVolume);
        Destroy(gameObject);
        GameObject explosion = Instantiate(VFX, transform.position, transform.rotation);
        Destroy(explosion, time);
    }
}
