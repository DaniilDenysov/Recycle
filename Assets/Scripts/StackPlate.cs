public class StackPlate 
{
    private int _localRange = 0, _prefabNumber = 0;
    public StackPlate(int _localRange, int _prefabNumber)
    {
        this._localRange = _localRange;
        this._prefabNumber = _prefabNumber;
    }
    public int GetLocalRange()
    {
        return _localRange;
    }
    public int GetPrefabNumber()
    {
        return _prefabNumber;
    }
    public override bool Equals(object obj)
    {
        return ((StackPlate)obj).GetLocalRange() == _localRange && ((StackPlate)obj).GetPrefabNumber() == _prefabNumber;
    }
}