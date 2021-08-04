using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundScroll : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    public float scrollRange;
    public float moveSpeed;
    [SerializeField]
    private Vector3 moveDirection = Vector3.down;
    public GameObject steppedBlock; //밟은 블록 체크용 게임오브젝트
    public int idx; //밟은 블록 층수
    public Text FinalScore;
    public Text BestScore;
    public Text Coin_txt;
    //public Text BonusScore;
    public int gameScore;
    public bool isTrigger;
    public bool isAlive;
    public float Playtime;
    public int Coin;
    public GameObject CoinParent;


    #region 싱글톤
    private static BackGroundScroll Instance;

    public static BackGroundScroll _Instance
    {
        get { return Instance; }
    }
    void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Start()
    {
        isAlive = true;
        isTrigger = false;
        idx = -1;
        gameScore = 0;
        moveSpeed = 10f;
        Coin = 0;
        steppedBlock = GameObject.Find("Block_Parent");
        CoinParent = GameObject.Find("Coin_Parent");
    }

    private void Update()
    {
        Playtime = Time.time;
        //Coin = CharacterController._Instance.Coin;
        //Coin_txt.text = Coin.ToString(); //이 코드 있으면 한 판에서 얻은 코인수로 덮어씌워짐
        //BonusScore.text = "Bonus  " + (Coin / 10).ToString("N0");
        //gameScore += (Coin/10);
        FinalScore.text = gameScore.ToString("N0");

        Frog_Data temp = new Frog_Data
        {
            playtime = Playtime,
            Score = gameScore,
            Coin = Coin
        };
        CSVData.Data.Add(temp);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌한 게임오브젝트가 Destroy면
        print(collision.name);
        if (collision.gameObject.name == "Destroy")
        {
            //충돌한 게임오브젝트의 첫번째 자식 파괴
            print("충돌함");
            Destroy(this.transform.GetChild(0).gameObject);
            idx--;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAlive)
        {
            //print(isAlive);
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                isAlive = false;
                if (collision.gameObject.tag == "Char")
                {
                    isTrigger = true;
                    if (isTrigger)
                    {
                        for (int i = 0; i < this.transform.childCount ; i++)
                        {
                            this.transform.GetChild(i).position += moveDirection * moveSpeed * 2.7f * Time.deltaTime;
                        }
                        CoinParent.gameObject.transform.position += moveDirection * moveSpeed * 2.7f * Time.deltaTime;

                        isTrigger = false;
                        if (idx != -1)
                        {
                            steppedBlock.transform.GetChild(idx).gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                        }
                        idx++;
                        gameScore++;
                    }
                }
            }
            //this.transform.position += moveDirection * moveSpeed*0.1f ;
        }
        isAlive = true;
    }

}
