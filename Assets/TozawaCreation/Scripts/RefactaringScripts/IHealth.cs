interface IHealth
{
    /// <summary>
    /// �_���[�W���󂯂鏈���B�U����^���鑤���_���[�W�ʂ�n���Ȃ���Ă�
    /// </summary>
    /// <param name="damage">�󂯂�_���[�W��</param>
    abstract void TakeDamage(int damage);
    
    /// <returns>���݂̗̑͒l</returns>
    abstract int CurrentHealth();
}
