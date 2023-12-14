using Cinemachine;
using CodeMonkey.MonoBehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassesHandler : MonoBehaviour
{
    public Transform classSelected; 
    public CameraFollowSetup Camera;
    public Transform techClass;
    public Transform fishClass;
    public Transform mageClass;
    public Transform axeMasterClass;
    public Transform smgClass;
    public static ClassesHandler instance;
    public static bool isClassSelected;
    public GameObject[] bulletIcons;
    public GameObject minimapIcon;
    
    private void Awake()
    {
        Camera = GameObject.Find("Camera").GetComponent<CameraFollowSetup>();
        instance = this;
        if (instance!=this || instance == null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        isClassSelected = false;
        
    }
    public void TechClassEnable()
    { 
        isClassSelected = true;
        bulletIcons[0].SetActive(true);
        classSelected = techClass.gameObject.transform;
        Cursor.visible = false;
        Camera.followTransform = classSelected.transform;
        CameraFollow.Instance.Setup(() => Camera.followTransform.position, () => 7f, true, true);
        minimapIcon.SetActive(true);
    }
    public void SmgClassEnable()
    {
        isClassSelected = true;
        bulletIcons[3].SetActive(true);
        classSelected = smgClass.gameObject.transform;
        Cursor.visible = false;
        Camera.followTransform = classSelected.transform;
        CameraFollow.Instance.Setup(() => Camera.followTransform.position, () => 7f, true, true);
        minimapIcon.SetActive(true);
    }
    public void FishClassEnable()
    {
        isClassSelected = true;
        bulletIcons[1].SetActive(true);
        classSelected = fishClass.gameObject.transform;
        Cursor.visible = false;
        Camera.followTransform = classSelected.transform;
        CameraFollow.Instance.Setup(() => Camera.followTransform.position, () => 7f, true, true);
        minimapIcon.SetActive(true);
    }
    public void MageClassEnable()
    {
        isClassSelected = true;
        bulletIcons[2].SetActive(true);
        classSelected = mageClass.gameObject.transform;
        Cursor.visible = false;
        Camera.followTransform = classSelected.transform;
        CameraFollow.Instance.Setup(() => Camera.followTransform.position, () => 7f, true, true);
        minimapIcon.SetActive(true);
    }
    public void AxeMasterClassEnable()
    {
        isClassSelected = true;
        classSelected = axeMasterClass.gameObject.transform;
        Cursor.visible = false;
        Camera.followTransform = classSelected.transform;
        CameraFollow.Instance.Setup(() => Camera.followTransform.position, () => 7f, true, true);
        minimapIcon.SetActive(true);
    }
}
