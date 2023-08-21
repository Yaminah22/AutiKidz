using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameScene : MonoBehaviour
{
    public GameObject CellPrefabs;
    public GameObject Parent;
    int TotalCells=8;
    public Sprite[] ObjectSprite;
    public List<Sprite> PickedSprite = new List<Sprite>();
    bool FirstClick=false;
    bool SecondClick=false;
    public string FirstMemorySpriteName,SecondMemorySpriteName;
    public Sprite CellBg;
    int FirstCellPosVal,SecondCellPosVal;
    int WinCellCount=0;
    public List<int> RandomCellValue=new List<int>();
  


    ScoreManager starResult;

    AudioManager audioManager;

   
   
    // Start is called before the first frame update
    void Start()
    {
        starResult = ScoreManager.Instance;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("startsound");

        for (int i=0;i<TotalCells;i++){
            GameObject CellInstance=Instantiate(CellPrefabs);
            CellInstance.transform.SetParent(Parent.transform);
           CellInstance.gameObject.name=i.ToString();
           ButtonListener(CellInstance);
           RandomCellValue.Add(i);
       
        }
        CollectingSpriteToCells();
        // startuiresult=GameObject.FindGameObjectWithTag("Star").GetComponent<ScoreManager>();
        
    }
    public void SoundBtn()
    {
        audioManager.Play("startsound");
    }
   //sound

    //to add sprite assigning object
    public void CollectingSpriteToCells(){
        int index=0;
        for(int i=0;i<TotalCells;i++){
            if(i== TotalCells / 2){
                index=0;
            }
            PickedSprite.Add(ObjectSprite[index]);
            index++;
           
        }
        RandomNumberGenerator();
    }
     //random number generator 
     public void RandomNumberGenerator(){
        RandomCellValue=RandomCellValue.OrderBy(Outval=>System.Guid.NewGuid()).ToList();
     }
     //sound
     public void playsound(){
       audioManager.Play("swapsound");
        
     }
     //soundmatch
   
    //button listener
     void ButtonListener(GameObject CellInstance){
           CellInstance.GetComponent<Button>().onClick.AddListener(DetectClick);
           CellInstance.GetComponent<Button>().onClick.AddListener(playsound);

           
    }
    public void DetectClick(){
    //    Parent.transform.GetChild(ClickedValue).GetComponent<Image>().sprite=PickedSprite[ClickedValue];
       if(!FirstClick){
       FirstCellPosVal=int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        FirstClick=true;
        Parent.transform.GetChild(FirstCellPosVal).GetComponent<Image>().sprite=PickedSprite[RandomCellValue[FirstCellPosVal]];
        //Debug.Log("the click has been done: "+ UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        FirstMemorySpriteName= Parent.transform.GetChild(FirstCellPosVal).GetComponent<Image>().sprite.name;
        Parent.transform.GetChild(FirstCellPosVal).GetComponent<Button>().enabled=false;
       }
       else if(!SecondClick){
       SecondCellPosVal=int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        SecondClick=true;
        Parent.transform.GetChild(SecondCellPosVal).GetComponent<Image>().sprite=PickedSprite[RandomCellValue[SecondCellPosVal]];
        //Debug.Log("the click has been done: "+ UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        SecondMemorySpriteName= Parent.transform.GetChild(SecondCellPosVal).GetComponent<Image>().sprite.name;
        Parent.transform.GetChild(SecondCellPosVal).GetComponent<Button>().enabled=false;
        Invoke("Detect", 0.40f );


       }
    }
  void matching(){

        if (FirstMemorySpriteName == "character5") audioManager.Play("shipsound");
        else if (FirstMemorySpriteName == "character6") audioManager.Play("carsound");
        else if (FirstMemorySpriteName == "character7") audioManager.Play("buildingsound");
        else if (FirstMemorySpriteName == "character8") audioManager.Play("orangejuicesound");
  }
   void Detect(){
   
    if(FirstMemorySpriteName == SecondMemorySpriteName){
        
         FirstClick=false;
         SecondClick=false;
         //Debug.Log("matched");     
         WinCellCount++;
         starResult.progress();
         matching();
     
         if(WinCellCount == TotalCells/ 2){
            //Debug.Log("");
         }
    }
    else{
         FirstClick=false;
         SecondClick=false;
         //Debug.Log("");
        Parent.transform.GetChild(FirstCellPosVal).GetComponent<Image>().sprite=CellBg;
        Parent.transform.GetChild(SecondCellPosVal).GetComponent<Image>().sprite=CellBg;
        Parent.transform.GetChild(FirstCellPosVal).GetComponent<Button>().enabled=true;
        Parent.transform.GetChild(SecondCellPosVal).GetComponent<Button>().enabled=true;


    }
   }
   
}
