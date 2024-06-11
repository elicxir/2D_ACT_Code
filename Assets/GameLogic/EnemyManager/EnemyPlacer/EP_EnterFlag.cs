using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

//�G�l�~�[�ݒu���:�G���A�i�����Ɉ�x�������������
public class EP_EnterFlag : EnemyPlacer
{
    /// <summary>
    /// ���̃t���O��true�̂Ƃ��̂ݓG���o��
    /// </summary>
    public string FlagName;
    public bool reverseMode;//true�Ȃ��false�̎��̂ݏo��

    public bool BossFlag;//true�Ȃ��UI��boss�̗͂ɓo�^�����

    bool isFlagTrue
    {
        get
        {
            return GM.Game.PlayData.GetGameFlag(FlagName).isTrue!= reverseMode;
        }
    }

    protected override void InitFunction()
    {
        if (isFlagTrue)
        {
            foreach (EnemyPlaceData epd in enemyPlaceDatas)
            {
                if (epd.enemyMarker != null)
                {
                    GM.UI.BossHP.Boss.Add(Place(epd, epd.enemyMarker.Position));

                }
                else
                {
                    GM.UI.BossHP.Boss.Add(Place(epd));

                    
                }
            }
        }
    }
}
