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
    public bool ClearPlayerPrefs;
    public static PlayfabManager instance;
    public int loggedIn;
    [SerializeField] public TextMeshProUGUI message;
    [SerializeField] GameObject profilePage;
    [SerializeField] GameObject loginPage;
    [SerializeField] public GameObject mainMenuPage;
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
    [SerializeField] TextMeshProUGUI emailProfile;
    public Button fishButton;
    public Button axeButton;
    public Button mageButton;
    public Button smgButton;
    private PlayFabAuthService _AuthService = PlayFabAuthService.Instance;
    public GetPlayerCombinedInfoRequestParams InfoRequestParams;
    public Toggle RememberMe;
    public int tokens;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        if (ClearPlayerPrefs)
        {
            _AuthService.UnlinkSilentAuth();
            _AuthService.ClearRememberMe();
            _AuthService.AuthType = Authtypes.None;
        }
        RememberMe.isOn = _AuthService.RememberMe;
        RememberMe.onValueChanged.AddListener(
            (toggle) =>
            {
                _AuthService.RememberMe = toggle;
            });
    }
    private void Update()
    {
        if (_AuthService.Email == null && !_AuthService.RememberMe)
        {
            PlayFabClientAPI.ForgetAllCredentials();
        }
    }

    void Start()
    {
        SetProfileEmail();
        bool logged = PlayFabClientAPI.IsClientLoggedIn();
        loggedIn = Convert.ToInt32(logged);
        PlayerPrefs.SetInt("loggedIn", loggedIn);
        PlayFabAuthService.OnDisplayAuthentication += OnDisplayAuthentication;
        PlayFabAuthService.OnLoginSuccess += OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError += OnError;
        fishButton.interactable = !Convert.ToBoolean(PlayerPrefs.GetInt("fishBought"));
        axeButton.interactable = !Convert.ToBoolean(PlayerPrefs.GetInt("axeBought"));
        mageButton.interactable = !Convert.ToBoolean(PlayerPrefs.GetInt("mageBought"));
        smgButton.interactable = !Convert.ToBoolean(PlayerPrefs.GetInt("smgBought"));
        _AuthService.InfoRequestParams = InfoRequestParams;
        _AuthService.Authenticate();
    }
    private void OnDisplayAuthentication()
    {
        this.message.text = "";

        /*
         * Optionally we could Not do the above and force login silently
         * 
         * _AuthService.Authenticate(Authtypes.Silent);
         * 
         * This example, would auto log them in by device ID and they would
         * never see any UI for Authentication.
         * 
         */
    }
    public void GetVirtualCurrencies()
    {
        try
        {
            if (PlayFabClientAPI.IsClientLoggedIn())
            {
                PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
            }
            else
            {
                this.message.text = "You need to login first to proceed to shop";
            }
        }
        catch (PlayFabException)
        {
            this.message.text = "You need to login first to proceed to shop";
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
        tokens = result.VirtualCurrency["CT"];
        tokenCount.text = tokens.ToString();
        
    }

    public void OpenMainMenu()
    {  
        profilePage.SetActive(false);
        loginPage.SetActive(false);
        mainMenuPage.SetActive(true);

    }
    public void SetProfileEmail()
    {
        emailProfile.text = "Email: " + _AuthService.Email;
    }
    public void RegisterUser()
    {
        if (passwordRegisterInput.text != passwordConfirmRegisterInput.text)
        {
            this.message.text = "Passwords dont match";
            return;
        }
        if (passwordRegisterInput.text.Length<6)
        {
            this.message.text = "Password must be over 6 symbols";
            return;
        }
        _AuthService.Email = emailRegisterInput.text;
        _AuthService.Password = passwordRegisterInput.text;
        var request = new RegisterPlayFabUserRequest
        {
            Email = _AuthService.Email,
            Password = _AuthService.Password,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        message.text = "Signed up and logged in";
        SetProfileEmail();
        OpenMainMenu();
    }

    public void OnClearSigninButtonClicked()
    {
        _AuthService.ClearRememberMe();
    }
    public void Login()
    {
        _AuthService.Email = emailLoginInput.text;
        _AuthService.Password = passwordLoginInput.text;
        _AuthService.Authenticate(Authtypes.EmailAndPassword);
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
        loggedIn = 0;
        PlayerPrefs.SetInt("loggedIn", 0);
        _AuthService.Email = null;
        _AuthService.Password = null;
        _AuthService.AuthTicket = null;
        _AuthService.ClearRememberMe();
        tokenCount.text = "";
    }

    

    private void OnErrorRecovery(PlayFabError error)
    {
        this.message.text = "No email found";
    }

    private void OnRecoverySuccess(SendAccountRecoveryEmailResult result)
    {
        this.message.text = "Recovery email sent";
    }

    private void OnLoginSuccess(PlayFab.ClientModels.LoginResult result)
    {
        loggedIn = 1;
        PlayerPrefs.SetInt("loggedIn", 1);
        message.text = "Logged in";
        OpenMainMenu();
        SetProfileEmail();
    }

    private void OnError(PlayFabError error)
    {
        this.message.text = error.ErrorMessage;
    }
    public void OpenProfileIfLogged()
    {
        if (Convert.ToInt32(PlayFabClientAPI.IsClientLoggedIn()) != 1)
        {
            loginPage.SetActive(true);
        }
        else
        {
            profilePage.SetActive(true);
        }
    }

}
