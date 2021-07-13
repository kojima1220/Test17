/*****************************************************************
***File Name      :HighScore.cs
***Version        :V2.0
***Designer       :熊倉裕人
***Date                 :2021.7.10
***Purpose        :ランキングについて
******************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class LeaderBoard : MonoBehaviour
{
  public int currentRank=0;
  public List<NCMB.HighScore> topRankers=null;
  public List<NCMB.HighScore> neighbors=null;

  /****************************************************************************
  *** Function Name       : fetchRank
  *** Designer            : 熊倉裕人
  *** Date                : 2021.6.20
  *** Function            : プレイヤーのスコアを受け取ってランクを取得する
  *** Return              : void
  ****************************************************************************/
  //元プレイヤーのハイスコアを受け取ってランクを取得
  public void fetchRank(int currentScore){
    //データスコアの「HighScore」から検索
    NCMBQuery<NCMBObject> rankQuery=new NCMBQuery<NCMBObject>("HighScore");
    rankQuery.WhereGreaterThan("Score",currentScore);
    rankQuery.CountAsync((int count, NCMBException e)=>{
      if(e!=null){
        //件数取得失敗
      }else{
        //件数取得成功
        currentRank=count+1;
      }
    });
  }

  /****************************************************************************
  *** Function Name       : fetchTopRankers
  *** Designer            : 熊倉裕人
  *** Date                : 2021.6.20
  *** Function            : サーバからトップ5を取得する
  *** Return              : void
  ****************************************************************************/
  //サーバーからトップ5を取得
  public void fetchTopRankers(){
    //データストアの「HighScore」クラスから検索
    NCMBQuery<NCMBObject> query=new NCMBQuery<NCMBObject>("HighScore");
    query.OrderByDescending("Score");
    query.Limit=5;
    query.FindAsync((List<NCMBObject> objList, NCMBException e)=>{
      if(e!=null){
        //検索失敗時の処理
      }else{
        //検索成功時の処理
        List<NCMB.HighScore> list=new List<NCMB.HighScore>();

        //取得したレコードをHIghScoreクラスとして保存
        foreach(NCMBObject obj in objList){
          int s=System.Convert.ToInt32(obj["Score"]);
          string n=System.Convert.ToString(obj["Name"]);
          list.Add(new HighScore(s,n));
        }
        topRankers=list;
      }
      });
  }

  /****************************************************************************
  *** Function Name       : fetchNeighbors
  *** Designer            : 熊倉裕人
  *** Date                : 2021.6.20
  *** Function            : サーバから自分のランク前後２件を取得する
  *** Return              : void
  ****************************************************************************/
  //サーバーからrankの前後２件を取得
  public void fetchNeighbors(){
    neighbors=new List<NCMB.HighScore>();

    //スキップする数を決める
    int numSkip=currentRank-3;
    if(numSkip<0)numSkip=0;

    //データストアの「HighScore」クラスから検索
    NCMBQuery<NCMBObject> query=new NCMBQuery<NCMBObject>("HighScore");
    query.OrderByDescending("Score");
    query.Skip=numSkip;
    query.Limit=5;
    query.FindAsync((List<NCMBObject> objList, NCMBException e)=>{
      if(e!=null){
        //検索失敗時の処理
      }else{
        //検索成功時の処理
        List<NCMB.HighScore> list=new List<NCMB.HighScore>();

        //取得したレコードをHighScoreクラスとして保存
        foreach(NCMBObject obj in objList){
          int s=System.Convert.ToInt32(obj["Score"]);
          string n=System.Convert.ToString(obj["Name"]);
          list.Add(new HighScore(s,n));
        }
        neighbors=list;
      }
      });
  }
}
