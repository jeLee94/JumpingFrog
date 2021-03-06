using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject target_box;       // 복제할 대상(프리팹)
    public GameObject target_box1;       // 복제할 대상(프리팹)
    public GameObject Block_Parent; // 블록 클론용 위한 부모 오브젝트
    public Transform box_spawnerPosition; // 위치 잡아주는 오브젝트
    public GameObject target_coin;       // 복제할 대상(프리팹)
    public GameObject Coin_Parent; // 블록 클론용 위한 부모 오브젝트
    public Transform Coin_spawnerPosition; // 위치 잡아주는 오브젝트
    //private int count;
    public float y;
    
    private float coin_y;
    public float time;
    //public int Stair; // 마지막에 유저에게 보여줄 스코어 층수
    private int ran;
    private int X_Range;
    private int NextLevel;
    public int count;

    void Start()
    {
        y = 0.6f; // y축 값 변화
        //coin_y = 6.6f;
        //Stair = 1;
        X_Range = 0;
        NextLevel = 0;
        //count = 0;
        time = 0f;

        GameObject g = Instantiate(target_box, box_spawnerPosition.transform.GetChild(2).position + new Vector3(0f, y, 0f), box_spawnerPosition.rotation, Block_Parent.transform);
        g.name = "블록1";
        //타켓 프리팹 인스턴스화 : 초기위치 고정위해 for문 밖에 배치
        for (int i = 0; i < 24; i++)
        {

            NextLevel = Random.Range(-1, 1); //-1 0 1
            y += 0.6f;

            if (X_Range < 1)
                NextLevel++;
            else if (X_Range == 4)
                NextLevel = -1;

            if (NextLevel == 0) // NextLevel이 0이면 같은 x축에 놓이니까 +나 -해줘야함
                NextLevel++;

            //타켓 프리팹 인스턴스화
            g = Instantiate(target_box, box_spawnerPosition.transform.GetChild(X_Range + NextLevel).position + new Vector3(0f, y, 0f), box_spawnerPosition.rotation, Block_Parent.transform);
            g.name = "블록" + (Block_Parent.transform.childCount);
            X_Range += NextLevel;

        }
        y = 0f;
        count = 1;
    }

    private void Update()
    {
        //print("Block_Parent.transform.childCount : " + Block_Parent.transform.childCount);
        ran = Random.Range(0, 2);

        
        if (Block_Parent.transform.childCount < 24)
        {
            NextLevel = Random.Range(-1, 1); //-1 0 1
            // y += 0.55f;
            coin_y += 0.6f;

            if (X_Range < 1)
                NextLevel++;
            else if (X_Range == 4)
                NextLevel = -1;

            if (NextLevel == 0) // NextLevel이 0이면 같은 x축에 놓이니까 +나 -해줘야함
                NextLevel++;

            //타켓 프리팹 인스턴스화
            print(Block_Parent.transform.childCount - 1);
            var t = Block_Parent.transform.GetChild(Block_Parent.transform.childCount-1).localPosition.y;
            print("1. 복제 기준 오브젝트" + Block_Parent.transform.GetChild(Block_Parent.transform.childCount - 1).name);
            print("2. " + box_spawnerPosition.transform.GetChild(X_Range + NextLevel).position);
            print("3. " + t);
            print("4. " + Block_Parent.transform.GetChild(Block_Parent.transform.childCount - 1).position.y);
            GameObject g = Instantiate(target_box1, new Vector3(0, 0, 0), box_spawnerPosition.rotation, Block_Parent.transform);
            g.name = "복제" + count;
            g.transform.localPosition = new Vector3(box_spawnerPosition.transform.GetChild(X_Range + NextLevel).position.x, t + 0.6f, 0);

            count++;
            //var temp = 
            //Instantiate(target_box1, box_spawnerPosition.transform.GetChild(X_Range + NextLevel).position +new Vector3(0, y, 0), box_spawnerPosition.rotation, Block_Parent.transform);
            //temp.transform.localPosition += new Vector3(0, y, 0);
            // if (ran == 0)
            {
                //Instantiate(target_coin, Coin_spawnerPosition.transform.GetChild((X_Range + NextLevel)).position + new Vector3(0f, y, 0f), Coin_spawnerPosition.rotation, Coin_Parent.transform);
            }
           
            X_Range += NextLevel;
            
        }

    }


}
