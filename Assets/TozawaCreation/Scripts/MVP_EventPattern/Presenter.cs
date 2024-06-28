using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(GameEventWaiting))]
public class Presenter : MonoBehaviour
{
    [SerializeField] InterfaceMediary<IDataSource> _im_iDataSource;
    [SerializeField] InterfaceMediary<IDataDestination> _im_iDataDestination;
    IDataSource _iSource;
    IDataDestination _iDestination;

    private void Start()
    {
        _iSource = _im_iDataSource.Interface();
        _iDestination = _im_iDataDestination.Interface();
    }
    public void Transceive()
    {
        var data = _iSource.ChangedData();
        _iDestination.ReceiveData(data);
        _iDestination.UpdateData();
    }
}
