using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ポーズ機能インターフェース
/// </summary>
interface IPauseAble
{
    /// <summary>
    /// ポーズ中に動いてほしくないもの全てを停止させてください
    /// </summary>
    void Pause();
    /// <summary>
    /// ポーズ時に止めていたものを全て元通りに動きを再開させて下さい
    /// </summary>
    void Reboot();
}

