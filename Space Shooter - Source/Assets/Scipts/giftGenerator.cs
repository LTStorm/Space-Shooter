using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// Class này tạo ra ngẫu nhiên các Gift tại các thời điểm ngẫu nhiên cách nhau một khoảng cho phép
public class giftGenerator : MonoBehaviour {

    // Danh sách các Gift có sắn
    [SerializeField] private List<Gifts> gifts;

    // Lặp phát Gift
    [SerializeField] private bool loop = false;

    // Chỉ số Gift thứ end là Gift cuối trong danh sách sẽ được phân phát
    [SerializeField] private int endGift = 0;

    // Khoảng thời gian giữa 2 lần tạo Gift trong khoảng này
    [SerializeField] [Range(2, 10)] private float minTimeBetweenGifts = 5;
    [SerializeField] [Range(5, 15)] private float maxTmeBetweenGifts = 10;

    // Use this for initialization
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawmAllGifts());
        } while (loop);
    }

    // Phân phát tất cả Gift ngẫu nhiên
    private IEnumerator SpawmAllGifts()
    {
        int i = (int)Random.Range(0, endGift);
        GameObject currentGift = gifts[i].GetGiftDesign();
        yield return StartCoroutine(SpawnGift(currentGift));
    }

    // Tạo đối tượng ngẫu nhiên Gift tại vị trí có hoành độ ngẫu nhiên và tung độ trên Top
    private IEnumerator SpawnGift(GameObject currentGift)
    {
        Instantiate(currentGift, currentGift.transform.position, Quaternion.identity);
        var seconds = Random.Range(minTimeBetweenGifts, maxTmeBetweenGifts);
        yield return new WaitForSeconds(seconds);
    }
}

