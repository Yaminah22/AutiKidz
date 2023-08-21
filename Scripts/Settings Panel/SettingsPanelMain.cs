using UnityEngine;
using TMPro;

public class SettingsPanelMain : MonoBehaviour
{
    public static SettingsPanelMain settings { get; set; }
    public GameObject panelMainMenu;
    public GameObject panelMain;
    public GameObject panelLanguages;
    public GameObject panelAccountDetail;
    public GameObject panelHelp;
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI emailField;
    SaveManager saveManager;
    [SerializeField]
    private GameConfig configuration;
    LanguageCheck changeLanguage;
    AudioManager audioManager;
    private void Awake()
    {
        
        if (settings == null) settings = this;
        
        changeLanguage = FindObjectOfType<LanguageCheck>();
        
    }
    private void Start()
    {
        panelAccountDetail.SetActive(false);
        panelMain.SetActive(false);
        panelLanguages.SetActive(false);
        panelHelp.SetActive(false);
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("bg");
        saveManager = FindObjectOfType<SaveManager>();
        configuration.SetUserEmail(References.ReferencedUser.Email);
        configuration.SetUserName(References.ReferencedUser.DisplayName);
    }


    public void ResumeFunction()
    {
        PlayClickAudio();
        panelMainMenu.SetActive(true);
        panelMain.SetActive(false);
    }
    public void SettingsOpenFunction()
    {
        PlayClickAudio();
        panelMainMenu.SetActive(false);
        panelMain.SetActive(true);
    }
    public void BackFunction()
    {
        PlayClickAudio();
        panelMain.SetActive(true);
        panelLanguages.SetActive(false);
        panelAccountDetail.SetActive(false);
        panelHelp.SetActive(false);
    }
    public void LanguagePanelOpen()
    {
        PlayClickAudio();
        panelMain.SetActive(false);
        panelLanguages.SetActive(true);
    }
    public void SetEngActive()
    {
        PlayClickAudio();
        configuration.SetLanguage("English");
        //Debug.Log("called eng");
        saveManager.SaveLanguage("English");
        changeLanguage.CheckCurrentlySetLanguage();
    }
    public void SetUrduActive()
    {
        PlayClickAudio();
        configuration.SetLanguage("Urdu");
        //Debug.Log("called Urdu");
        saveManager.SaveLanguage("Urdu");
        changeLanguage.CheckCurrentlySetLanguage();
    }
    public void SetAccountDetails()
    {
        PlayClickAudio();
        nameField.text = configuration.GetUserName();
        emailField.text = configuration.GetUserEmail();
        panelAccountDetail.SetActive(true);
        panelMain.SetActive(false);
    }
    public void PlayClickAudio()
    {
        audioManager.Play("click");
    }
    public void HelpFunction()
    {
        PlayClickAudio();
        panelHelp.SetActive(true);
        panelMain.SetActive(false);
    }
}
