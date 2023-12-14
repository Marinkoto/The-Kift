using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    [SerializeField] private string playerSR;
    public bool isOrange;
    public bool isBlack;
    public bool isRadioactive;
    public bool isIndigo;
    public bool isCotton;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isOrange = SkinManager.isOrange;
        isBlack = SkinManager.isBlack;
        isRadioactive = SkinManager.isRadioactive;
        isIndigo = SkinManager.isIndigo;
        isCotton = SkinManager.isCotton;
        animator.SetBool("Orange", isOrange);
        animator.SetBool("Default", isBlack);
        animator.SetBool("Radioactive", isRadioactive);
        animator.SetBool("Indigo", isIndigo);
        animator.SetBool("Cotton", isCotton);
    }
}