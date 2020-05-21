using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.Experimental.Rendering;
using Random = UnityEngine.Random;
//Class chứa các hành vi của kẻ địch
public class Enemy : MonoBehaviour
{
    [Header("Enemy's Attributes")]
    [SerializeField] private float health = 500f;
    [SerializeField] private int score = 100;
    private float shotCounter;
    [Header("Enemy's Laser")]
    [SerializeField] private float minTimeBetweenShot = 0.2f;
    [SerializeField] private float maxTmeBetweenShot = 3f;
    [SerializeField] private GameObject laserPrefabs;
    [SerializeField] private float projectiveSpeed = -5.0f;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private float time = 1f;
    [Header("SFX")]
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] private float deathSFXVolume;
    [SerializeField] private AudioClip laserSFX;
    [SerializeField] [Range(0, 1)] private float laserSFXVolume;


    // Use this for initialization
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShot, maxTmeBetweenShot);
    }


    // Update is called once per frame
    void Update()
    {
        CountDownAndShot();
    }
    //Hàm dùng để đếm ngược thời gian và bắn đạn của kẻ địch
    void CountDownAndShot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0) Fire();
    }
    //Hàm bắn đạn
    void Fire()
    {
        GameObject enemy = Instantiate(laserPrefabs, gameObject.transform.position, Quaternion.identity);//Tạo ra đối tượng laser
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectiveSpeed);//Gán vận tốc cho đối tượng
        shotCounter = Random.Range(minTimeBetweenShot, maxTmeBetweenShot);//Set bộ đếm lại
        AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserSFXVolume);//Phát ra âm thanh bắn đạn
    }
    private void OnTriggerEnter2D(Collider2D other)//Hàm xử lý sự kiện khi va chạm với các đối tượng khác
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (!damageDealer) return;
        ProcessHit(damageDealer);//Xử lý sự kiện gây sát thương cho đối tượng này
    }
    //Hàm xử lý khi đối tường này bị dính sát thương
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }
    //Hàm xử lý khi đối tượng chết
    private void Die()
    {
        FindObjectOfType<GameSessions>().AddToScore(score);//Cộng điểm cho người chơi
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);//Phát ra âm thanh khi đối tương chết
        Destroy(gameObject);//Phá hủy đối tượng
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);//Tạo hiệu ứng khi đối tượng chết
        Destroy(explosion, time);//Phá hủy đối tượng tạo bhiệu ứng
    }
}