using UnityEngine;

public class SSkinInfo : MonoBehaviour
{
    public enum SkinIDs { Default,Orange }

    [SerializeField] public SkinIDs skinID;
    public SkinIDs _skinID { get { return skinID; } }

    [SerializeField] private int skinPrice;
    public int _skinPrice { get { return skinPrice; } }
}