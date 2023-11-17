public interface IStateMachine
{
    public void OnEnter(Character character);
    public void OnExcute(Character character);
    public void OnExit(Character character);
}
