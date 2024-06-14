interface IHealth
{
    /// <summary>
    /// ダメージを受ける処理。攻撃を与える側がダメージ量を渡しながら呼ぶ
    /// </summary>
    /// <param name="damage">受けるダメージ量</param>
    abstract void TakeDamage(int damage);
    
    /// <returns>現在の体力値</returns>
    abstract int CurrentHealth();
}
