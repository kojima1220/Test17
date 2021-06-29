using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	// スコアを表示するGUIText
	public Text scoreGUIText;

	// ハイスコアを表示するGUIText
	public Text highScoreGUIText;

	// スコア
	private int score;

	// ハイスコア
	private NCMB.HighScore highScore;
	private bool isNewRecord;

	void Start ()
	{
		Initialize ();

		//ハイスコアを取得する。保存されてなければ0点。
		string name=FindObjectOfType<UserAuth>().currentPlayer();
		highScore=new NCMB.HighScore(0,name);
		highScore.fetch();
	}

	void Update ()
	{
		// スコアがハイスコアより大きければ
		if (highScore.score < score) {
			isNewRecord=true;
			highScore.score = score;
		}

		// スコア・ハイスコアを表示する
		this.scoreGUIText.text = score.ToString ();
		this.highScoreGUIText.text = "HighScore : " + highScore.score.ToString ();
	}

	// ゲーム開始前の状態に戻す
	private void Initialize ()
	{
		// スコアを0に戻す
		score = 0;

		//フラグを初期化する
		isNewRecord=false;
	}

	// ポイントの追加
	public void AddPoint (int point)
	{
		score = score + point;
	}

	// ハイスコアの保存
	public void Save ()
	{
		// ハイスコアを保存する(記録の更新があったときのみ)
		if(isNewRecord)
			highScore.save();

		// ゲーム開始前の状態に戻す
		Initialize ();
	}
}
