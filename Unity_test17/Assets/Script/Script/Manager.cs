using UnityEngine;

public class Manager : MonoBehaviour
{
  // Playerプレハブ
  public GameObject player;

  // タイトル
  private GameObject title;

  // ボタンが押されると対応する変数がtrueになる
  private bool leaderBoardButton;
  private bool commentButton;
  private bool logOutButton;

  void Start ()
  {
    // Titleゲームオブジェクトを検索し取得する
    title = GameObject.Find ("Title");
  }

  void OnGUI() {
    if( !IsPlaying() ){
      drawButton();

      // ログアウトボタンが押されたら
      if( logOutButton )
         FindObjectOfType<UserAuth> ().logOut ();

      // 画面タップでゲームスタート
      if ( Event.current.type == EventType.MouseDown) 
         GameStart ();
    }

    // ログアウト完了してたらログインメニューに戻る
    if( FindObjectOfType<UserAuth>().currentPlayer() == null )
      Application.LoadLevel("Login");   
  }

  void GameStart() {
    // ゲームスタート時に、タイトルを非表示にしてプレイヤーを作成する
    title.SetActive (false);
    Instantiate (player, player.transform.position, player.transform.rotation);
  }

  //public void GameOver() {
    //FindObjectOfType<Score> ().Save ();
    // ゲームオーバー時に、タイトルを表示する
    //title.SetActive (true);
  //}

  public bool IsPlaying () {
    // ゲーム中かどうかはタイトルの表示/非表示で判断する
    return title.activeSelf == false;
  }

  private void drawButton() {
    // ボタンの設置
    int btnW = 140, btnH = 50;
    GUI.skin.button.fontSize = 18;
    leaderBoardButton = GUI.Button( new Rect(0*btnW, 0, btnW, btnH), "Leader Board" );
    commentButton     = GUI.Button( new Rect(1*btnW, 0, btnW, btnH), "Comment" );
    logOutButton      = GUI.Button( new Rect(2*btnW, 0, btnW, btnH), "Log Out" );
  }
}
