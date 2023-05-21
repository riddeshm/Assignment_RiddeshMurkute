using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    List<Cell> parts = new List<Cell>();
    List<GameObject> partObjs = new List<GameObject>();
    Cell head;

    [SerializeField] private GameObject partPrefab;

    public Cell Head
    {
        get { return head; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Cell cell)
    {
        SetHead(cell);
        Expand();
    }

    public void Expand()
    {
        GameObject headObj = Instantiate(partPrefab, transform);
        headObj.transform.position = head.Pos;
        parts.Add(head);
        partObjs.Add(headObj);
    }

    public void Move(Cell cell)
    {
        Cell lastPart = parts.First();
        parts.Remove(lastPart);
        lastPart.Status = CellStatus.None;

        SetHead(cell);
        parts.Add(head);
        int partsLastIndex = parts.Count - 1;
        for(int i = 0; i < partObjs.Count; i++)
        {
            partObjs[i].transform.position = parts[partsLastIndex - i].Pos;
        }
    }

    private void SetHead(Cell cell)
    {
        head = cell;
        head.Status = CellStatus.Snake;
    }
}
