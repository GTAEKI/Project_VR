using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game
    }

    public enum Sound
    {
        Bgm,
        Sfx,
        MaxCount
    }

    public enum UIEvent
    {
        Click,
        Drag
    }

    public enum MouseEvent
    {
        Press,
        Click
    }

    public enum CameraMode
    {
        QuaterView
    }

    /// <summary>
    /// 모든 데이터 ID 리스트
    /// 김민섭_231014
    /// </summary>
    public enum Data_ID_List
    {
        Unit_Minion = 10,       // 졸개 전용 유닛 ID
        Unit_Golem,             // 골렘 전용 유닛 ID
        Bullet_Minion = 1003,   // 졸개 전용 총알 ID
        Bullet_Golem,           // 골렘 전용 총알 ID
        Minion_Fast = 2000,     // 빠른 졸개 ID
        Minion_Power,           // 강한 졸개 ID
        Spawn_Phase1 = 3000,    // 스폰 조건1 ID
        Spawn_Phase2,           // 스폰 조건2 ID
        Meteor = 2004,          // 스킬 메테오 ID
        Golem = 10000           // 골렘 ID
    }
}
