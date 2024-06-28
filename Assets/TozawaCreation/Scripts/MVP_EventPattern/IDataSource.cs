interface IDataSource
{
    void OnDataChanged();
    abstract public TimeData ChangedData();
}
