using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager instance;
    public int loggedIn = 0;
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] GameObject profilePage;
    [SerializeField] GameObject loginPage;
    [Header("Login")]
    [SerializeField] TMP_InputField emailLoginInput;
    [SerializeField] TMP_InputField passwordLoginInput;
    [Header("Register")]
    [SerializeField] TMP_InputField emailRegisterInput;
    [SerializeField] TMP_InputField passwordRegisterInput;
    [SerializeField] TMP_InputField passwordConfirmRegisterInput;
    [Header("Recovery")]
    [SerializeField] TMP_InputField emailRecoveryInput;
    [Header("UI")]
    [SerializeField] TextMeshProUGUI tokenCount;
    public Button fishButton;
    public Button axeButton;
    public Button mageButton;
    private void OnEnable()
    {
        instance = this;
    }
    void Start()
    {
        fishButton.interactable = !Convert.ToBoolean(PlayerPrefs.GetInt("fishBought"));
        axeButton.interactable = !Convert.ToBoolean(PlayerPrefs.GetInt("axeBought"));
        mageButton.interactable = !Convert.ToBoolean(PlayerPrefs.GetInt("mageBought"));
        loggedIn = Convert.ToInt32(PlayFabClientAPI.IsClientLoggedIn());
        PlayerPrefs.SetInt("loggedIn", loggedIn);
    }
    private void Update()
    {
        
    }

    public void GetVirtualCurrencies()
    {
        try
        {
           PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
        }
        catch (PlayFabException)
        {
            message.text = "You need to login first to proceed to shop";
            StartCoroutine(ResetMessage());
            return;
        }
       
    }
    public void AddCurrency(int amount)
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "CT",
            Amount = amount
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddSuccess, OnError);
    }

    private void OnAddSuccess(ModifyUserVirtualCurrencyResult result)
    {
        GetVirtualCurrencies();
    }

    private void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        int tokens = result.VirtualCurrency["CT"];
        tokenCount.text = tokens.ToString();
        
    }

    public void OpenProfile()
    {
        profilePage.SetActive(true);
        loginPage.SetActive(false);
    }
    public void RegisterUser()
    {
        if (passwordRegisterInput.text.Length<6)
        {
            message.text = "Password must be over 6 symbols";
            StartCoroutine(ResetMessage());
        }
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailRegisterInput.text,
            Password = passwordRegisterInput.text,
            RequireBothUsernameAndEmail = false,
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        if (passwordRegisterInput.text !=passwordConfirmRegisterInput.text)
        {
            message.text = "Passwords dont match";
            StartCoroutine(ResetMessage());
        }
    }
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailLoginInput.text,
            Password = passwordLoginInput.text,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }
    public void RecoverPassword()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailRecoveryInput.text,
            TitleId = "F7449",
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverySuccess, OnErrorRecovery);
    }
    public void LogOut()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        loggedIn = 0;
        PlayerPrefs.SetInt("loggedIn", 0);
        StartCoroutine(ResetMessage());
    }
    private void OnErrorRecovery(PlayFabError error)
    {
        message.text = "No email found";
        StartCoroutine(ResetMessage());
    }

    private void OnRecoverySuccess(SendAccountRecoveryEmailResult result)
    {
        message.text = "Recovery email sent";
        StartCoroutine(ResetMessage());
    }

    private void OnLoginSuccess(LoginResult result)
    {
        message.text = "Logged in";
        loggedIn = 1;
        PlayerPrefs.SetInt("loggedIn", 1);
        OpenProfile();
        StartCoroutine(ResetMessage());
        fishButton.interactable = !Convert.ToBoolean(PlayerPrefs.GetInt("fishBought"));
        axeButton.interactable = !Convert.ToBoolean(PlayerPrefs.GetInt("axeBought"));
        mageButton.interactable = !Convert.ToBoolean(PlayerPrefs.GetInt("mageBought"));
    }

    private void OnError(PlayFabError error)
    {
        message.text = error.ErrorMessage;
        StartCoroutine(ResetMessage());
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        message.text = "New Account is created";
        StartCoroutine(ResetMessage());
    }
    public void OpenProfileIfLogged()
    {
        if (PlayerPrefs.GetInt("loggedIn")!= 1)
        {
            loginPage.SetActive(true);
        }
        else
        {
            profilePage.SetActive(true);
        }
    }
    public IEnumerator ResetMessage()
    {
        yield return new WaitForSeconds(5);
        message.text = "";
    }
}
