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
    public static ClassesHandler instance;
    public static bool isClassSelected;
    public GameObject[] bulletIcons;
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
    }
    public void FishClassEnable()
    {
        isClassSelected = true;
        bulletIcons[1].SetActive(true);
        classSelected = fishClass.gameObject.transform;
        Cursor.visible = false;
        Camera.followTransform = classSelected.transform;
        CameraFollow.Instance.Setup(() => Camera.followTransform.position, () => 7f, true, true);
    }
    public void MageClassEnable()
    {
        isClassSelected = true;
        bulletIcons[2].SetActive(true);
        classSelected = mageClass.gameObject.transform;
        Cursor.visible = false;
        Camera.followTransform = classSelected.transform;
        CameraFollow.Instance.Setup(() => Camera.followTransform.position, () => 7f, true, true);
    }
    public void AxeMasterClassEnable()
    {
        isClassSelected = true;
        classSelected = axeMasterClass.gameObject.transform;
        Cursor.visible = false;
        Camera.followTransform = classSelected.transform;
        CameraFollow.Instance.Setup(() => Camera.followTransform.position, () => 7f, true, true);
    }
}
