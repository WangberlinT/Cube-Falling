
public interface WorldObserver
{
    void OnEnermyDie(MonsterSubject e);
    void OnPlayerDie();
    void AddPlayer();
    void AddEnermy(MonsterSubject e);

}
