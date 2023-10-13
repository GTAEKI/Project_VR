#region 유닛 스텟
/// <summary>
/// 유닛 스탯 클래스
/// 231013_박시연
/// </summary>
public class UnitStatus
{
    #region 프로퍼티 

    public int ID { protected set; get; }
    public string Type { protected set; get; }
    public float Damage { protected set; get; }
    public float DurationTime { protected set; get; }
    public int MaxCount { protected set; get; }
    public int Price { protected set; get; }

    #endregion

    public UnitStatus(int id, string type, float durationTime, int maxCount, int price)
    {
        ID = id;
        Type = type;
        DurationTime = durationTime;
        MaxCount = maxCount;
        Price = price;
    }
}

/// <summary>
/// 유닛의 현재 스탯 클래스
/// 231013_박시연
/// </summary>
public class CurrentUnitStatus : UnitStatus
{
    public CurrentUnitStatus(int id, string type ,float durationTime, int maxCount, int price) : base( id,  type,  durationTime,  maxCount,  price)
    { }

    public CurrentUnitStatus(BaseUnitStatus status) : base(status.ID, status.Type, status.DurationTime, status.MaxCount, status.Price)
    { }
}


/// <summary>
/// 유닛의 원본 스탯 클래스
/// 231013_박시연
/// </summary>
public class BaseUnitStatus : UnitStatus
{
    public BaseUnitStatus(int id, string type, float durationTime, int maxCount, int price) : base(id, type, durationTime, maxCount, price)
    { }
}
#endregion

#region 총알 스텟
public class BulletStatus
{
    #region 프로퍼티 

    public int ID { protected set; get; }
    public string Info { protected set; get; }
    public float Delay { protected set; get; }
    public float Damage { protected set; get; }
    public float CriChance { protected set; get; }
    public float CriDamage { protected set; get; }
    public float Speed { protected set; get; }
    public float LifeTime { protected set; get; }

    #endregion

    public BulletStatus(int id, string info, float delay, float damage ,float criChance, float criDamage, float speed , float lifeTime)
    {
        ID = id;
        Info = info;
        Delay = delay; 
        Damage = damage;
        CriChance = criChance;
        CriDamage = criDamage;
        Speed = speed;
        LifeTime = lifeTime;
    }
}

/// <summary>
/// 총알의 현재 스탯 클래스
/// 231013_박시연
/// </summary>
public class CurrentBulletStatus : BulletStatus
{
    public CurrentBulletStatus(int id, string info, float delay, float damage, float criChance, float criDamage, float speed, float lifeTime) : base( id,  info, delay , damage,  criChance,  criDamage,  speed,  lifeTime)
    { }

    public CurrentBulletStatus(BaseBulletStatus status) : base(status.ID, status.Info,status.Delay ,status.Damage, status.CriChance, status.CriDamage,status.Speed,status.LifeTime)
    { }

    /// <summary>
    /// 타겟에게 데미지를 주는 함수
    /// 231014_박시연
    /// </summary>
    public void OnDamaged()
    {
        // TODO : 
    }
}


/// <summary>
/// 총알의 원본 스탯 클래스
/// 231013_박시연
/// </summary>
public class BaseBulletStatus : BulletStatus
{
    public BaseBulletStatus(int id, string info,float delay, float damage, float criChance, float criDamage, float speed, float lifeTime) : base(id, info, delay, damage, criChance, criDamage, speed, lifeTime)
    { }
}
#endregion