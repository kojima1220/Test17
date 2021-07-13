/*****************************************************************
***File Name      :HighScore.cs
***Version        :V2.0
***Designer       :熊倉裕人
***Date                 :2021.7.10
***Purpose        :スコアについて
******************************************************************/

using System.Collections.Generic;
using NCMB;

namespace NCMB{
  public class HighScore
  {
      public int score{ get; set;}
      public string name{get; private set;}

      //コンストラクタ
      public HighScore(int _score, string _name){
        score=_score;
        name=_name;
      }

      /************************************************************
      ***Function Name     :save()
      ***Desiner           :熊倉裕人
      ***Date              :2021.6.20
      ***Function          :スコアをデータベースに保存する
      ***Return            :void
      ************************************************************/

      public void save(){
        //データストアの「HighScore」クラスから、Nameをキーにして検索
        NCMBQuery<NCMBObject> query=new NCMBQuery<NCMBObject>("HighScore");
        query.WhereEqualTo("Name",name);
        query.FindAsync((List<NCMBObject> objList ,NCMBException e)=>{
          //検索成功したら
          if(e==null){
            objList[0]["Score"]=score;
            objList[0].SaveAsync();
          }
          });
      }

      /************************************************************
      ***Function Name     :fetch()
      ***Desiner           :熊倉裕人
      ***Date              :2021.6.20
      ***Function          :スコアをデータベースから取得する
      ***Return            :void
      ************************************************************/

      //サーバーからスコアを取得
      public void fetch(){
        //データストアの「HighScore」クラスから、Nameをキーにして検索
        NCMBQuery<NCMBObject> query=new NCMBQuery<NCMBObject>("HighScore");
        query.WhereEqualTo("Name",name);
        query.FindAsync((List<NCMBObject> objList ,NCMBException e)=>{
          //検索成功したら
          if(e==null){
            //スコアが未登録だったら
            if(objList.Count==0){
              NCMBObject obj=new NCMBObject("HighScore");
              obj["Name"]=name;
              obj["Score"]=0;
              obj.SaveAsync();
              score=0;
            }
            //スコアが登録済みだったら
            else{
              score=System.Convert.ToInt32(objList[0]["Score"]);
            }
          }
        });
      }

      //ランキングで表示するために文字列を整形
      public string print(){
        return name+" "+score;
      }
  }
}
