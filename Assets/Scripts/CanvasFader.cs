//  CanvasFader.cs
//  http://kan-kikuchi.hatenablog.com/entry/CanvasFader
//
//  Created by kan.kikuchi on 2016.05.11.

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CanvasGroup))]
/// <summary>
/// キャンバスをフェードするクラス
/// </summary>
public class CanvasFader : MonoBehaviour {

  //フェード用のキャンバス
  private CanvasGroup _canvasGroup;

  //フェードの状態
  private enum FadeState{
    None, FadeIn, FadeOut
  }
  private FadeState _fadeState = FadeState.None;

  //フェード時間
  [SerializeField]
  private float _duration;

  //タイムスケールを無視するか
  [SerializeField]
  private bool _ignoreTimeScale = true;

  //フェード終了後のコールバック
  private Action _onFinished = null;

  //=================================================================================
  //更新
  //=================================================================================

  private void Update (){
    if(_fadeState == FadeState.None){
      return;
    }

    float fadeSpeed = 1f / _duration;
    if(_ignoreTimeScale){
      fadeSpeed *= Time.unscaledDeltaTime;
    }
    else{
      fadeSpeed *= Time.deltaTime;
    }

    _canvasGroup.alpha += fadeSpeed * (_fadeState == FadeState.FadeIn ? 1f : -1f);

    //フェード終了判定
    if(_canvasGroup.alpha > 0 && _canvasGroup.alpha < 1){
      return;
    }

    if(_onFinished != null){
      _onFinished();
    }

    _fadeState = FadeState.None;
    this.enabled = false;
  }

  //=================================================================================
  //開始
  //=================================================================================

  /// <summary>
  /// 対象のオブジェクトのフェードを開始する
  /// </summary>
  public static CanvasFader Begin(GameObject target, bool isFadeOut, float duration, bool ignoreTimeScale = true, Action onFinished = null){
    CanvasFader canvasFader = target.GetComponent<CanvasFader>();
    if(canvasFader == null){
      canvasFader = target.AddComponent<CanvasFader>();
    }

    canvasFader.Play(isFadeOut, duration, ignoreTimeScale, onFinished);

    return canvasFader;
  }

  /// <summary>
  /// フェードを開始する
  /// </summary>
  public void Play(bool isFadeOut, float duration, bool ignoreTimeScale = true, Action onFinished = null){
    this.enabled = true;

    if(_canvasGroup == null){
      _canvasGroup = GetComponent<CanvasGroup>();
    }

    _canvasGroup.alpha = isFadeOut ? 1 : 0;
    _fadeState         = isFadeOut ? FadeState.FadeOut :FadeState.FadeIn;

    _duration        = duration;
    _ignoreTimeScale = ignoreTimeScale;
    _onFinished      = onFinished;
  }

}