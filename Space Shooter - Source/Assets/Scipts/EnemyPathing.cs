using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    private List<Transform> waypoints;
    private int waypointIndex=0;
    private WaveConfig waveConfig;

	// Use this for initialization
	void Start ()
	{
        waypoints = waveConfig.GetWayPoints();
	    transform.position = waypoints[waypointIndex].transform.position;
	}
    //Hàm nhận wave
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
	// Update is called once per frame
	void Update () {
	    Move();
	}

    private void Move()
    {
        if (waypointIndex < waypoints.Count )//Kẻ địch sẽ di chuyển theo từng vị trí được thiết lập trong wave đó
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed()* Time.deltaTime;//Set tốc độ di chuyển cho kẻ địch
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);//Di chuyển dần đến vị trí ktiếp theo
            if (transform.position == waypoints[waypointIndex].transform.position) waypointIndex++;//Nếu đã đến vị trí cần đến thì set vị trí di chuyển tiếp theo
        }
        else
        {
            Destroy(gameObject);//Đi hết quãng đường của wave thì phá hủy đối tượng
        }
        
    }
}
