using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class UnlockSkillButton: MonoBehaviour
    {
        SkillManager skillManager;
        private void Awake()
        {
            skillManager = FindObjectOfType<SkillManager>();
        }
        public void UnlockDoubleShotForCrossbow()
        {
            skillManager.crossbowCanDoubleShot = true;

            AppMetrica.Instance.ReportEvent("ballista_double_shot_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockFireGunLeft()
        {
            skillManager.fireGunleftFireEnable = true;

            AppMetrica.Instance.ReportEvent("flamethrower_left_firegun_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }        
        public void UnlockFireGunRight()
        {
            skillManager.fireGunrightFireEnable = true;

            AppMetrica.Instance.ReportEvent("flamethrower_right_firegun_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockBigYadro()
        {
            skillManager.bigYadroEnable = true;
            skillManager.smallYadroEnable= false;

            AppMetrica.Instance.ReportEvent("cannon_big_cannonball_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockSmallYadro()
        {
            skillManager.bigYadroEnable = false;
            skillManager.smallYadroEnable = true;

            AppMetrica.Instance.ReportEvent("cannon_small_cannonball_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockOneArrow()
        {
            skillManager.crossbowPlusOneArrow = true;

            AppMetrica.Instance.ReportEvent("ballista_one_arrow_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockTwoArrow()
        {
            skillManager.crossbowPlusTwoArrow = true;

            AppMetrica.Instance.ReportEvent("ballista_two_arrow_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockThreeArrow()
        {
            skillManager.crossbowPlusThreeArrow = true;

            AppMetrica.Instance.ReportEvent("ballista_three_arrow_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockFourArrow()
        {
            skillManager.crossbowPlusFourArrow = true;

            AppMetrica.Instance.ReportEvent("ballista_four_arrow_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockFiveArrow()
        {
            skillManager.crossbowPlusFiveArrow = true;

            AppMetrica.Instance.ReportEvent("ballista_five_arrow_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockFireMortar()
        {
            skillManager.fireBomb = true;

            AppMetrica.Instance.ReportEvent("mortar_fire_bomb_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockHolyMortar()
        {
            skillManager.holyBomb = true;

            AppMetrica.Instance.ReportEvent("mortar_holy_bomb_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockCursedMortar()
        {
            skillManager.cursedBomb = true;

            AppMetrica.Instance.ReportEvent("mortar_cursed_bomb_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockSuckingMortar()
        {
            skillManager.suckingCannonball = true;

            AppMetrica.Instance.ReportEvent("mortar_sucking_bomb_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockKnockbackMortar()
        {
            skillManager.knockbackCannonball = true;

            AppMetrica.Instance.ReportEvent("mortar_knockback_bomb_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockHolyCrossbow()
        {
            skillManager.holyArrow = true;

            AppMetrica.Instance.ReportEvent("crossbow_holy_arrow_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockCursedCrossbow()
        {
            skillManager.cursedArrow = true;

            AppMetrica.Instance.ReportEvent("crossbow_cursed_arrow_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }
    }
}
