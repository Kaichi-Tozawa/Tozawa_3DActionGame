interface IVirtualStick : IPause
{
    /// <summary>
    /// �������̓��͂�Ԃ�
    /// </summary>
    public abstract float HorizonInput();
    /// <summary>
    /// �c�����̓��͂�Ԃ�
    /// </summary>
    public abstract float VerticalInput();
    /// <summary>
    /// �^�b�v���ꑱ���Ă��邩�ǂ�����Bool�l��Ԃ�
    /// </summary>
    public abstract bool Pressed();
    /// <summary>
    /// ����^�C�~���O�ł̔�\������
    /// </summary>
    public abstract void DisAble();
}
