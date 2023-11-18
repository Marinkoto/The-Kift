using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    [SerializeField] private string playerSR;
    public bool isOrange;
    public bool isBlack;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isOrange = SkinManager.isOrange;
        isBlack = SkinManager.isBlack;
        animator.SetBool("Orange", isOrange);
        animator.SetBool("Default", isBlack);
    }
}