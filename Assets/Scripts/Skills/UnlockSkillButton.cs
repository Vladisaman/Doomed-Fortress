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
    }
}
