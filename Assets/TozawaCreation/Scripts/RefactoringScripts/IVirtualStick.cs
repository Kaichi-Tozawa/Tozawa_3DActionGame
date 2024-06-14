interface IVirtualStick : IPause
{
    /// <summary>
    /// 横方向の入力を返す
    /// </summary>
    public abstract float HorizonInput();
    /// <summary>
    /// 縦方向の入力を返す
    /// </summary>
    public abstract float VerticalInput();
    /// <summary>
    /// タップされ続けているかどうかのBool値を返す
    /// </summary>
    public abstract bool Pressed();
    /// <summary>
    /// 特定タイミングでの非表示処理
    /// </summary>
    public abstract void DisAble();
}
