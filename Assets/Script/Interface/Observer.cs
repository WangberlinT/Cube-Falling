
public interface WorldObserver
{
    void OnEnermyDie(EnermySubject e);
    void OnPlayerDie();
    void AddPlayer();
    void AddEnermy(EnermySubject e);

}
