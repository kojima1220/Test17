/*******************************************************************
*** File Name   :   LeaderBoardManager.cs
*** Version     :   1.0
*** Desiner     :   熊倉裕人
*** Date        :   2021.6.20
*** Purpose     :   ランキングについて
*******************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeaderBoardManager : MonoBehaviour
{
    private LeaderBoard lBoard;
    private NCMB.HighScore currentHighScore;
    //public GameObject[] top=new GameObject[5];
    //public GameObject[] nei=new GameObject[5];
    public Text[] top=new Text[5];
    public Text[] nei=new Text[5];
    bool isScoreFetched;
    bool isRankFetched;
    bool isLeaderBoardFetched;
    //ボタンが押されると対応する変数がtrueになる
    private bool backButton;

    /****************************************************************************
    *** Function Name       : Start()
    *** Designer            : 熊倉裕人
    *** Date                : 2021.6.20
    *** Function            : 現在のスコアを取得する
    *** Return              : void
    ****************************************************************************/
    void Start()
    {
      lBoard=new LeaderBoard();

      //フラグ初期化
      isScoreFetched=false;
      isRankFetched=false;
      isLeaderBoardFetched=false;

      //現在のハイスコアを取得
      string name=FindObjectOfType<UserAuth>().currentPlayer();
      currentHighScore=new NCMB.HighScore(-1,name);
      currentHighScore.fetch();
    }

    /****************************************************************************
    *** Function Name       : Update()
    *** Designer            : 熊倉裕人
    *** Date                : 2021.6.20
    *** Function            : 現在のランキングを表示する
    *** Return              : void
    ****************************************************************************/
    void Update()
    {
      //現在のハイスコアの取得が完了したら1度だけ実行
      if(currentHighScore.score!=-1 && !isScoreFetched){
        lBoard.fetchRank(currentHighScore.score);
        isScoreFetched=true;
      }
      //現在の順位の取得が完了したら1度だけ実行
      if(lBoard.currentRank!=0 && !isRankFetched){
        lBoard.fetchTopRankers();
        lBoard.fetchNeighbors();
        isRankFetched=true;
      }
      //ランキングの取得が完了したら1度だけ実行
      if(lBoard.topRankers!=null && lBoard.neighbors!=null && !isLeaderBoardFetched){
        //自分が1位の時と2位の時だけ順位を調整
        int offset=2;
        if(lBoard.currentRank==1)offset=0;
        if(lBoard.currentRank==2)offset=1;

        //取得したトップ5ランキングを表示
        for(int i=0;i<lBoard.topRankers.Count; ++i){
          //top[i].GetComponent<Text>.text
          this.top[i].text=i+1 + ". " + lBoard.topRankers[i].print();
        }

        //取得したライバルランキングを表示
        for(int i=0;i<lBoard.neighbors.Count; ++i){
          this.nei[i].text=lBoard.currentRank- offset + i + ". " + lBoard.neighbors[i].print();
        }
        isLeaderBoardFetched=true;
      }
    }

    /****************************************************************************
    *** Function Name       : OnGUI()
    *** Designer            : 熊倉裕人
    *** Date                : 2021.6.20
    *** Function            : 戻るボタンを押したらゲーム選択画面へ移動する
    *** Return              : void
    ****************************************************************************/
    void OnGUI(){
      drawMenu();

      //戻るボタンが押されたら
      if(backButton)
        Application.LoadLevel("GameChoice");
    }

    /****************************************************************************
    *** Function Name       : drawMenu()
    *** Designer            : 熊倉裕人
    *** Date                : 2021.6.20
    *** Function            : ボタンのを設置する
    *** Return              : void
    ****************************************************************************/
    private void drawMenu(){
      //ボタンの設置
      int btnW=170, btnH=30;
      GUI.skin.button.fontSize=20;
      backButton=GUI.Button(new Rect(Screen.width*1/2-btnW*1/2, Screen.height*7/8-btnH*1/2, btnW,btnH),"Back");
    }
}
