using UnityEngine;
using TMPro;

public class SettingsPanel : MonoBehaviour
{
    public static SettingsPanel settings { get; set; }
    public GameObject topBar;
    public GameObject body;
    public GameObject panelMain;
    public GameObject panelLanguages;
    public GameObject panelAccountDetail;
    public GameObject panelHelp;
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI emailField;

    [SerializeField]
    private GameConfig configuration;
    LanguageCheck changeLanguage;
    AudioManager audioManager;
    SaveManager saveManager;
    private void Awake()
    {
        
        if (settings == null) settings = this;
        panelAccountDetail.SetActive(false);
        panelMain.SetActive(false);
        panelLanguages.SetActive(false);
        panelHelp.SetActive(false);
        changeLanguage = FindObjectOfType<LanguageCheck>();
    }
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("theme");
        saveManager = FindObjectOfType<SaveManager>();
        configuration.SetUserEmail(References.ReferencedUser.Email);
        configuration.SetUserName(References.ReferencedUser.DisplayName);
    }


    public void ResumeFunction()
    {
        PlayClickAudio();
        topBar.SetActive(true);
        body.SetActive(true);
        panelMain.SetActive(false);
    }
    public void SettingsOpenFunction()
    {
        PlayClickAudio();
        topBar.SetActive(false);
        body.SetActive(false);
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
