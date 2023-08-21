using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private List<PuzzleSlot> slotprefab;
    [SerializeField] private PuzzlePiece pieceprefab;
    [SerializeField] private Transform slotParent, pieceParent,slotchild, piecechild;
    AudioManager audiomanager;
    void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();
        audiomanager.Play("solvepuzzle");
        Spawn();
    }
    //Sound button functionality to be implemented in every activity
    public void SoundButton()
    {
        audiomanager.Play("solvepuzzle");
    }
    void Spawn()
    {
        var randomSet = slotprefab.Take(4).ToList();
        for (int i = 0; i < randomSet.Count; i++)
        {
            slotchild = slotParent.GetChild(i);
            var spawnedSlot = Instantiate(randomSet[i],slotchild.transform.position,Quaternion.identity);
            spawnedSlot.transform.localScale = new Vector3(0.36f, 0.36f, 0.36f);
            spawnedSlot.transform.SetParent(slotchild);
            piecechild = pieceParent.GetChild(randomSet.Count-1-i);
            var spawnedPiece = Instantiate(pieceprefab, piecechild.position, Quaternion.identity);
            spawnedPiece.transform.localScale= new Vector3(0.36f, 0.36f, 0.36f);
            spawnedPiece.transform.SetParent(piecechild);
            spawnedPiece.transform.localPosition = new Vector3(0, 0, 0);
            spawnedPiece.Init(spawnedSlot);
      
        }
    }
}
