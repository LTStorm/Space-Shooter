using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    //Parameters
    [Header("Player Movement")]
    [SerializeField] private float playerSpeed = 8f;

    private float xMin;
    private float xMax;
    private float yMax;
    private float yMin;
    [SerializeField] private float xPadding;
    [SerializeField] private float yPadding;
    [SerializeField] private int health = 500;

    [Header("Projectile")]
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject laserUpgrade1;
    [SerializeField] private GameObject laserUpgrade2;
    [SerializeField] private float projectiveSpeed;
    [SerializeField] private float projectileFiringPeriod = 0.1f;
    private Coroutine firingCoroutine;
    private GameObject laserPrefabs;

    [Header("SFX")] 
    [SerializeField] private AudioClip beingHitSFX;
    [SerializeField] [Range(0,1)] private float beingHitSFXVolume = 1f;
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] private float deathSFXVolume = 1f;

    private GameSessions gameSessions;

    // Use this for initialization
    void Start()
    {
        SetUpMoveBoundaries();
        laserPrefabs = laser;
        gameSessions = FindObjectOfType<GameSessions>();
        gameSessions.SetHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
            Move();
            Fire();
    }
    //Hàm xử lý khi người chơi phải chịu sát thương
    void ProcessHit(DamageDealer damageDealer)
    {
        AudioSource.PlayClipAtPoint(beingHitSFX,Camera.main.transform.position,beingHitSFXVolume);//Phát âm thanh bị dính sát thương
        if (laserPrefabs == laserUpgrade2)
        {
            laserPrefabs = laserUpgrade1;
            return;
        }
        if (laserPrefabs == laserUpgrade1)
        {
            laserPrefabs = laser;
            return;
        }
        //Hai lệnh if trên thụt lùi nâng cấp đạn lại 1 cấp 
        health -= damageDealer.GetComponent<DamageDealer>().GetDamage();//Máu bị giảm bằng lượng sát thương do vật thể va chạm gây ra
    
        if (health <= 0)// Xử lý khi máu về 0
        {
            health = 0;
            gameSessions.SetHealth(health);
            Death();
        }
        gameSessions.SetHealth(health);//Truyền lượng máu hiện tại của người chơi về gameSession
 
    }
    //Hàm xử lý khi nhân vật người chơi chết
    private void Death()
    {
        AudioSource.PlayClipAtPoint(deathSFX,Camera.main.transform.position,deathSFXVolume);//Phát ra âm thanh người chơi chết
        Destroy(gameObject);// Phá hủy nhân vật của người chơi
        FindObjectOfType<LoadLevel>().LoadGameOver();//Load màn kết thúc
    }

    void OnTriggerEnter2D(Collider2D other)//Hàm nhận dạng vật thể va chạm với người chơi
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();//Lấy phần scipt chứa sát thương cảu vật thể va chạm
        if (damageDealer) ProcessHit(damageDealer);//Nếu tồn tại scipt chứa sát thương gây ra thì xử lý nó với người chơi
        PrizeDealer prizeDealer = other.GetComponent<PrizeDealer>();
        if (prizeDealer) PrizeReceive(prizeDealer);//Nếu vật thể va chạm là gift thì xử lý
        
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy"))//Nếu vật thể không phải là kẻ địch thì phá hủy vật thể va chạm
        {
            Destroy(other.gameObject);
        }
        //Phần trên được dùng chủ yếu để phá hủy đạn của kẻ địch và gift khi va chạm với người chơi
    }

    //Hàm này xử lý sự kiện khi người chơi nhấn hoặc đè nút Space để xả đạn ra liên tục
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine=StartCoroutine(FireContinously());//Bắt đầu quá trình bắn
        
        }
                
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);//Dừng quá trình bắn
        }
    }
    IEnumerator FireContinously()
    {
        while (true)//Ko cần điều kiện dừng loop vì hàm sẽ được dừng lại từ bên ngoài
        {
            GameObject playerLaser = Instantiate(laserPrefabs, transform.position +new Vector3(0,0.8f,0),Quaternion.identity) as GameObject;//Tạo tia laser
            playerLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectiveSpeed);//Gán vận tốc cho tia laser
            yield return new WaitForSeconds(projectileFiringPeriod);//Chờ khoản thời gian giữa hai lần bắn đạn
        }
    }
    //Hàm này dùng để đặt khoản di chuyển cho người chơi. Tránh tình trạng nhân vật người chơi thoát ra khỏi màn hình chơi game
    private void SetUpMoveBoundaries()
    {
        xMin = -3.61f + xPadding;
        xMax = 3.62f- xPadding;
        yMin = -6.63f + yPadding;
        yMax = 6.63f - yPadding;
    }

    //Hàm di chuyển của người chơi
    void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;//Nhân thêm Time.deltaTime để cố định tốc độ di chuyển trên các máy tính khác nhau
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
    //Hàm xử lý sự kiện khi nhận được gift
    void PrizeReceive(PrizeDealer prizeDealer) { 
        if (prizeDealer.GetScore() > 0) FindObjectOfType<GameSessions>().AddToScore(prizeDealer.GetScore());//Xử lý khi nhận được gift cho điểm
        health += (int)prizeDealer.GetHeath();
        gameSessions.SetHealth(health);
        if (prizeDealer.isUprade())//Xử lý khi nhận được gift upgrade đạn
        {
            if (laserPrefabs == laserUpgrade1) laserPrefabs = laserUpgrade2;
            else if (laserPrefabs == laser) laserPrefabs = laserUpgrade1;            
        }
        prizeDealer.Hit();//Phá hủy đối tượng gift trên màn hình
    }
}