using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager instance;
    public delegate void ExperienceChangeHandler(int amount);
    public event ExperienceChangeHandler OnExperienceChange;
    private void Awake()
    {
        instance = this;
        if (instance!=null && instance!=this)
        {
            Destroy(this);
        }
        else
            instance = this;
        
    }
    public void AddExp(int amount)
    {
        OnExperienceChange?.Invoke(amount);
    }
}
