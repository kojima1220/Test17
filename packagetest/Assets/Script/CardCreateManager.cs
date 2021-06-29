using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class CardCreateManager : MonoBehaviour
{

    // 生成するCardオブジェクト
    public Card CardPrefab;

    // 「カード」を生成する親オブジェクト
    public RectTransform CardCreateParent;

    // 生成したカードオブジェクトを保存する
    public List<Card> CardList = new List<Card>();

    // カード情報の順位をランダムに変更したリスト
    private List<CardData> mRandomCardDataList = new List<CardData>();

    // GridLayoutGroup
    public GridLayoutGroup GridLayout;

    // カード配列のインデックス
    private int mIndex;

    // カードを生成する時の高さインデックス
    private int mHelgthIdx;
    // カードを生成する時の幅インデックス
    private int mWidthIdx;

    // カードの生成アニメーションのアニメーション時間
    private readonly float DEAL_CAED_TIME = 1f;

    void Start()
    {
        //debug
        //Card card = Instantiate<Card> (this.CardPrefab, this.CardCreateParent);

        // カード情報リスト
        List<CardData> cardDataList = new List<CardData>();

        // 表示するカード画像情報のリスト
        List<Sprite> imgList = new List<Sprite>();

        // Resources/Imageフォルダ内にある画像を取得する
        imgList.Add(Resources.Load<Sprite>("Image/mark_clover"));
        imgList.Add(Resources.Load<Sprite>("Image/mark_diamond"));
        imgList.Add(Resources.Load<Sprite>("Image/mark_heart"));
        imgList.Add(Resources.Load<Sprite>("Image/mark_spade"));

        // forを回す回数を取得する
        int loopCnt = imgList.Count;

        for (int i = 0; i < loopCnt - 1; i++)
        {

            // カード情報を生成する
            CardData cardata = new CardData(i, imgList[i]);
            cardDataList.Add(cardata);
        }

        this.mIndex = 0;
        this.mHelgthIdx = 0;
        this.mWidthIdx = 0;

        // 生成したカードリスト２つ分のリストを生成する
        List<CardData> SumCardDataList = new List<CardData>();
        SumCardDataList.AddRange(cardDataList);
        SumCardDataList.AddRange(cardDataList);

        // ランダムリストの初期化
        this.mRandomCardDataList.Clear();

        // リストの中身をランダムに再配置する
        this.mRandomCardDataList = SumCardDataList.OrderBy(a => Guid.NewGuid()).ToList();
        this.mRandomCardDataList.AddRange(SumCardDataList.OrderBy(a => Guid.NewGuid()).ToList());

        // GridLayoutを無効
        this.GridLayout.enabled = false;

        // カードを配るアニメーション処理
        this.mSetDealCardAnime();
    }

    /// <summary>
    /// カードを配るアニメーション処理
    /// </summary>
    private void mSetDealCardAnime()
    {

        var _cardData = this.mRandomCardDataList[this.mIndex];

        // Instantiate で Cardオブジェクトを生成
        Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);
        // データを設定する
        card.Set(_cardData);
        // カードの初期値を設定 (画面外にする)
        card.mRt.anchoredPosition = new Vector2(1900, 0f);
        // サイズをGridLayoutのCellSizeに設定
        card.mRt.sizeDelta = this.GridLayout.cellSize;

        // カードの移動先を設定
        float posX = (this.GridLayout.cellSize.x * this.mWidthIdx) + (this.GridLayout.spacing.x * (this.mWidthIdx + 1));
        float posY = ((this.GridLayout.cellSize.y * this.mHelgthIdx) + (this.GridLayout.spacing.y * this.mHelgthIdx)) * -1f;

        // DOAnchorPosでアニメーションを行う
        card.mRt.DOAnchorPos(new Vector2(posX, posY), this.DEAL_CAED_TIME)
            // アニメーションが終了したら
            .OnComplete(() =>
            {
                // 生成したカードオブジェクトを保存する
                this.CardList.Add(card);

                // 生成するカードデータリストのインデックスを更新
                this.mIndex++;
                this.mWidthIdx++;

                // 生成インデックスがリストの最大値を迎えたら
                if (this.mIndex >= this.mRandomCardDataList.Count)
                {
                    // GridLayoutを有効にし、生成処理を終了する
                    this.GridLayout.enabled = true;
                }
                else
                {
                    // GridLayoutの折り返し地点に来たら
                    if (this.mIndex % this.GridLayout.constraintCount == 0)
                    {
                        // 高さの生成箇所を更新
                        this.mHelgthIdx++;
                        this.mWidthIdx = 0;
                    }
                    // アニメーション処理を再帰処理する
                    this.mSetDealCardAnime();
                }
            });
    }

    /// <summary>
    /// カードを生成する
    /// </summary>
    public void CreateCard()
    {
        // Start()のコピペ
        // カード情報リスト
        List<CardData> cardDataList = new List<CardData>();

        // 表示するカード画像情報のリスト
        List<Sprite> imgList = new List<Sprite>();

        // Resources/Imageフォルダ内にある画像を取得する
        imgList.Add(Resources.Load<Sprite>("Image/mark_clover"));
        imgList.Add(Resources.Load<Sprite>("Image/mark_diamond"));
        imgList.Add(Resources.Load<Sprite>("Image/mark_heart"));
        imgList.Add(Resources.Load<Sprite>("Image/mark_spade"));

        // forを回す回数を取得する
        int loopCnt = imgList.Count;

        for (int i = 0; i < loopCnt; i++)
        {

            // カード情報を生成する
            CardData cardata = new CardData(i, imgList[i]);
            cardDataList.Add(cardata);
        }

        this.mIndex = 0;
        this.mHelgthIdx = 0;
        this.mWidthIdx = 0;

        // 生成したカードリスト２つ分のリストを生成する
        List<CardData> SumCardDataList = new List<CardData>();
        SumCardDataList.AddRange(cardDataList);
        SumCardDataList.AddRange(cardDataList);

        // ランダムリストの初期化
        this.mRandomCardDataList.Clear();

        // リストの中身をランダムに再配置する
        this.mRandomCardDataList = SumCardDataList.OrderBy(a => Guid.NewGuid()).ToList();
        this.mRandomCardDataList.AddRange(SumCardDataList.OrderBy(a => Guid.NewGuid()).ToList());

        // GridLayoutを無効
        this.GridLayout.enabled = false;

        // カードを配るアニメーション処理
        this.mSetDealCardAnime();
        /*
        // カード情報リスト
        List<CardData> cardDataList = new List<CardData>();

        // 表示するカード画像情報のリスト
        List<Sprite> imgList = new List<Sprite>();

        // Resources/Imageフォルダ内にある画像を取得する
        imgList.Add(Resources.Load<Sprite>("Image/mark_clover"));
        imgList.Add(Resources.Load<Sprite>("Image/mark_diamond"));
        imgList.Add(Resources.Load<Sprite>("Image/mark_heart"));
        imgList.Add(Resources.Load<Sprite>("Image/mark_spade"));

        // forを回す回数を取得する
        int loopCnt = imgList.Count;

        for (int i = 0; i < loopCnt; i++)
        {

            // カード情報を生成する
            CardData cardata = new CardData(i, imgList[i]);
            cardDataList.Add(cardata);
        }
        // 生成したカードリスト２つ分のリストを生成する
        List<CardData> SumCardDataList = new List<CardData>();
        SumCardDataList.AddRange(cardDataList);
        SumCardDataList.AddRange(cardDataList);
        // リストの中身をランダムに再配置する
        List<CardData> randomCardDataList = SumCardDataList.OrderBy(a => Guid.NewGuid()).ToList();
        // カードオブジェクトを生成する
        foreach (var _cardData in randomCardDataList)
        {

            // Instantiate で Cardオブジェクトを生成
            Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);
            // データを設定する
            card.Set(_cardData);

            // 生成しかカードオブジェクトを保存する
            this.CardList.Add(card);
        
        }
        */
    }

    // <summary>
    /// 取得していないカードを背面にする
    /// </summary>
    public void HideCardList(List<int> containCardIdList)
    {

        foreach (var _card in this.CardList)
        {

            // 既に獲得したカードIDの場合、非表示にする
            if (containCardIdList.Contains(_card.Id))
            {

                // カードを非表示にする
                _card.SetInvisible();
            }
            // カードが表麺 && 獲得していないカードは裏面表示にする
            else if (_card.IsSelected)
            {

                // カードを裏面表示にする
                _card.SetHide();
            }
        }
    }
}