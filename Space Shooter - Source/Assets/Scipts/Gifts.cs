using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "GiftDesign")]

//Class này giúp tạo đối tượng Gift trực quan trong AssetMenu
public class Gifts : ScriptableObject
{

    [SerializeField] private GameObject gift;

    public GameObject GetGiftDesign() { return gift; }
}
