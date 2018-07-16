namespace GameLogic.Actions
{
    public interface IAct
    {
        Unit Execute(Unit unit, object parameters);
    }
}