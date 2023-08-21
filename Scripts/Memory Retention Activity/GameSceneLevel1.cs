using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameSceneLevel1 : MonoBehaviour
{
    public GameObject CellPrefabs;
    public GameObject Parent;
    public Sprite[] ObjectSprite;
    public List<Sprite> PickedSprite = new List<Sprite>();
    bool FirstClick = false;
    bool SecondClick = false;
    public string FirstMemorySpriteName, SecondMemorySpriteName;
    public Sprite CellBg;
    int FirstCellPosVal, SecondCellPosVal;
    int WinCellCount = 0;
    public List<int> RandomCellValue = new List<int>();

    public GameObject starResult;
    public AudioManager audiomanager;
     SoundMatch soundMatchObj;
    // Start is called before the first frame update
    void Start()
    {
        MatchingSetup soundmatch = new MatchingSetup(ObjectSprite);
        soundMatchObj = new SoundMatch(soundmatch);
        audiomanager = FindObjectOfType<AudioManager>();

        audiomanager.Play("startsound");

        for (int i = 0; i < soundmatch.TotalCells; i++)
        {
            GameObject CellInstance = Instantiate(CellPrefabs);
            CellInstance.transform.SetParent(Parent.transform);
            CellInstance.gameObject.name = i.ToString();
            soundMatchObj.ButtonListener(CellInstance);
            RandomCellValue.Add(i);
        }
        soundMatchObj.CollectingSpriteToCells();
        // startuiresult=GameObject.FindGameObjectWithTag("Star").GetComponent<ScoreManager>();
       
    }
    public void SoundBtn()
    {
        audiomanager.Play("startsound");
    }
    public void Detect(){
        soundMatchObj.Detect();
    }
    interface ISetup
    {
        void CollectingSpriteToCells();
        void RandomNumberGenerator();
        public List<Sprite> PickedSprite { get; set; }
        public int TotalCells {get;set;}
        
    }

    // matching
    class MatchingSetup : ISetup
    {
        private Sprite[] ObjectSprite;
        public int TotalCells {get;set;}
        public List<Sprite> PickedSprite { get; set; }
        public List<int> RandomCellValue { get; private set; }
            

        public MatchingSetup(Sprite[] objectSprite)
        {
            ObjectSprite = objectSprite;
            PickedSprite = new List<Sprite>();
            RandomCellValue = new List<int>();
            TotalCells=6;
        }

        // to add sprite assigning object
        public void CollectingSpriteToCells()
        {
            int index = 0;
            for (int i = 0; i < TotalCells; i++)
            {
                if (i == TotalCells / 2)
                {
                    index = 0;
                }
                PickedSprite.Add(ObjectSprite[index]);
                index++;
            }
            RandomNumberGenerator();
        }

        // random number generator 
        public void RandomNumberGenerator()
        {
            RandomCellValue = RandomCellValue.OrderBy(Outval => System.Guid.NewGuid()).ToList();
        }

        // swap sound
       
    }

    // soundmatch
    class SoundMatch
    {

        private ISetup setupObj;
        public AudioManager audiomanager = FindObjectOfType<AudioManager>();

        public SoundMatch(ISetup setupObj)
        {
            this.setupObj = setupObj;
        }
        public void CollectingSpriteToCells()
        {
            setupObj.CollectingSpriteToCells();
        }

        public void ButtonListener(GameObject CellInstance)
        {
            CellInstance.GetComponent<Button>().onClick.AddListener(DetectClick);
            CellInstance.GetComponent<Button>().onClick.AddListener(playsound);
        }
        public virtual void playsound()
        {
            audiomanager.Play("swapsound");
        }
        public void DetectClick()
        {
            GameSceneLevel1 gameSceneLevel1 = FindObjectOfType<GameSceneLevel1>();
            if (!gameSceneLevel1.FirstClick)
            {
                gameSceneLevel1.FirstCellPosVal = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
                gameSceneLevel1.FirstClick = true;
                gameSceneLevel1.Parent.transform.GetChild(gameSceneLevel1.FirstCellPosVal).GetComponent<Image>().sprite = setupObj.PickedSprite[gameSceneLevel1.RandomCellValue[gameSceneLevel1.FirstCellPosVal]];
                //Debug.Log("The click has been done: " + UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
                gameSceneLevel1.FirstMemorySpriteName = gameSceneLevel1.Parent.transform.GetChild(gameSceneLevel1.FirstCellPosVal).GetComponent<Image>().sprite.name;
                gameSceneLevel1.Parent.transform.GetChild(gameSceneLevel1.FirstCellPosVal).GetComponent<Button>().enabled = false;
            }
            else if (!gameSceneLevel1.SecondClick)
            {
                gameSceneLevel1.SecondCellPosVal = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
                gameSceneLevel1.SecondClick = true;
                gameSceneLevel1.Parent.transform.GetChild(gameSceneLevel1.SecondCellPosVal).GetComponent<Image>().sprite = setupObj.PickedSprite[gameSceneLevel1.RandomCellValue[gameSceneLevel1.SecondCellPosVal]];
                //Debug.Log("The click has been done: " + UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
                gameSceneLevel1.SecondMemorySpriteName = gameSceneLevel1.Parent.transform.GetChild(gameSceneLevel1.SecondCellPosVal).GetComponent<Image>().sprite.name;
                gameSceneLevel1.Parent.transform.GetChild(gameSceneLevel1.SecondCellPosVal).GetComponent<Button>().enabled = false;
                gameSceneLevel1.Invoke("Detect", 0.40f);
            }
        }

        public void Detect()
        {
            GameSceneLevel1 gameSceneLevel1 = FindObjectOfType<GameSceneLevel1>();
            if (gameSceneLevel1.FirstMemorySpriteName == gameSceneLevel1.SecondMemorySpriteName)
            {
                gameSceneLevel1.FirstClick = false;
                gameSceneLevel1.SecondClick = false;
                //Debug.Log("");
                gameSceneLevel1.WinCellCount++;
                gameSceneLevel1.starResult.GetComponent<Starthree>().progress();
                matching();
                if (gameSceneLevel1.WinCellCount ==setupObj.TotalCells / 2)
                {
                    Debug.Log("");
                }
            }
            else
            {
                gameSceneLevel1.FirstClick = false;
                gameSceneLevel1.SecondClick = false;
                //Debug.Log("Not matched");
                gameSceneLevel1.Parent.transform.GetChild(gameSceneLevel1.FirstCellPosVal).GetComponent<Image>().sprite = gameSceneLevel1.CellBg;
                gameSceneLevel1.Parent.transform.GetChild(gameSceneLevel1.SecondCellPosVal).GetComponent<Image>().sprite = gameSceneLevel1.CellBg;
                gameSceneLevel1.Parent.transform.GetChild(gameSceneLevel1.FirstCellPosVal).GetComponent<Button>().enabled = true;
                gameSceneLevel1.Parent.transform.GetChild(gameSceneLevel1.SecondCellPosVal).GetComponent<Button>().enabled = true;
            }
        }

         public  virtual void  matching()
        {
            GameSceneLevel1 gameSceneLevel1 = FindObjectOfType<GameSceneLevel1>();
            if (gameSceneLevel1.FirstMemorySpriteName == "character2")
            {
                gameSceneLevel1.audiomanager.Play("housesound");
            }
            else if (gameSceneLevel1.FirstMemorySpriteName == "character3")
            {
                gameSceneLevel1.audiomanager.Play("burgersound");
            }
            else if (gameSceneLevel1.FirstMemorySpriteName == "character1")
            {
                gameSceneLevel1.audiomanager.Play("ballsound");
            }
        }
    }
}


