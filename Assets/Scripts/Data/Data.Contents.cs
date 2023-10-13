/// <summary>
/// 메테오 스탯 클래스
/// 김민섭_231014
/// </summary>
public class MeteorStatus
{
    #region 프로퍼티 

    public int Id { private set; get; }                 // 고유 번호
    public int Hp { private set; get; }                 // 투사체 체력
    public float DurationTime { private set; get; }     // 투사체 활성화 시간
    public int Damage { private set; get; }             // 투사체 피해량
    public float Speed { private set; get; }            // 투사체 이동속도

    #endregion

    /// <summary>
    /// 메테오 스탯 클래스 기본 생성자
    /// 김민섭_231014
    /// </summary>
    public MeteorStatus(int id, int hp, float durTime, int dmg, float speed)
    {
        Id = id;
        Hp = hp;
        DurationTime = durTime;
        Damage = dmg;
        Speed = speed;
    }

    /// <summary>
    /// 데이터 아이디로 세팅하는 생성자
    /// 김민섭_231014
    /// </summary>
    /// <param name="id">데이터 아이디</param>
    public MeteorStatus(Define.Data_ID_List id)
    {
        Id = int.Parse(Managers.Data.Boss_MeteorStatusData[(int)id]["ID"].ToString());
        Hp = int.Parse(Managers.Data.Boss_MeteorStatusData[(int)id]["Hp"].ToString());
        DurationTime = float.Parse(Managers.Data.Boss_MeteorStatusData[(int)id]["Projectile_Lifetime"].ToString());
        Damage = int.Parse(Managers.Data.Boss_MeteorStatusData[(int)id]["Damage"].ToString());
        Speed = float.Parse(Managers.Data.Boss_MeteorStatusData[(int)id]["Speed"].ToString());
    }
}