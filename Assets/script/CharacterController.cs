using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public GameObject Frog;
    public GameObject[] panels;
    public string date;
    public int Coin;
    public AudioSource jump;
    public AudioSource apple;
    //public AudioClip jumpingSound;
    //public AudioClip DropSound;

    #region 싱글톤
    private static CharacterController Instance;

    public static CharacterController _Instance
    {
        get { return Instance; }
    }
    void Awake()
    {
        Instance = this;
    }
    #endregion

    //씬전환 함수
    public void restart()
    {
        SceneManager.LoadScene("Frog");
        Time.timeScale = 1f;
    }

    void Start()
    {

        date = DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초");
        date = SqlFormat(date);
        panels[1].SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            jump.PlayOneShot(jump.clip);
            transform.Translate(Vector3.up * 0.6f);
            transform.Translate(Vector3.right * 0.8f);

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            jump.PlayOneShot(jump.clip);
            transform.Translate(Vector3.up * 0.6f);
            transform.Translate(Vector3.left * 0.8f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if ((BackGroundScroll._Instance.gameScore > 1))
        {
            if (collision.transform.tag == "Terrain")
            {
                //print(collision.transform.name + "충돌함");
                Time.timeScale = 0f;
                panels[1].SetActive(true);

                if(panels[1].activeSelf == true)
                sqlite.DbConnectionCHek();

                // DB 연동 코드
                string sql = string.Format("Insert into Game(Datetime, Playtime, Score, Coin) VALUES({0}, {1}, {2}, {3})", date, BackGroundScroll._Instance.Playtime, BackGroundScroll._Instance.gameScore, BackGroundScroll._Instance.Coin);
                print(sql);
                sqlite.DatabaseInsert(sql); // 위에서 짠 SQL문을 디비에 쏴주는 함수 실행

                //DB 최고점수 가져오기
                sql = string.Format("SELECT MAX(Score), SUM(Coin) From Game");
                //sql = string.Format("SELECT MAX(Score), SUM(Coin) From Game ");
                

                sqlite.DataBaseRead(sql);
                while (sqlite.dataReader.Read())                            // 쿼리로 돌아온 레코드 읽기
                {
                    int maxScore = sqlite.dataReader.GetInt32(0);
                    int SumCoin = sqlite.dataReader.GetInt32(1);
                    var MaxScore_Text = BackGroundScroll._Instance.BestScore;
                    var SumCoin_Text = BackGroundScroll._Instance.Coin_txt;
                    MaxScore_Text.text = maxScore.ToString();
                    SumCoin_Text.text = SumCoin.ToString();
                    //break; // 최고점수이므로 처음에 한 번만 레코드값을 가져오면 된다.
                }
                
                sqlite.DBClose();


                // DB 최고점수 가져오기
                //sqlite.DbConnectionCHek(); // DB 연결, 연결상태 확인
                //string sql = string.Format("SELECT MAX(Score) AS Max_Score FROM Game");

                //sqlite.DataBaseRead(sql);

                //sqlite.dataReader.Read();
                //var maxscore = sqlite.dataReader.GetInt32(2);
                //print(maxscore);
                //sqlite.DBClose();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //캐릭터와 충돌한 게임오브젝트가 코인이면

        if (collision.gameObject.tag == "Coin")
        {
            apple.PlayOneShot(apple.clip);
            Destroy(BackGroundScroll._Instance.CoinParent.transform.GetChild(0).gameObject);
            Coin++;
        }
    }

    public static string SqlFormat(string sql)
    {
        return string.Format("\"{0}\"", sql);
    }
}

