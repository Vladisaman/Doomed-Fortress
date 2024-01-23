using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

namespace Assets
{
    public class UnlockSkillButton: MonoBehaviour
    {
        SkillManager skillManager;
        public Mortar mortar;
        public Crossbow crossbow;
        public WallBehavior wall;
        public Enemy enemy;

        private void Awake()
        {
            skillManager = FindObjectOfType<SkillManager>();
            mortar = GameObject.Find("Mortar").GetComponent<Mortar>();
            crossbow = GameObject.Find("Crossbow").GetComponent<Crossbow>();
            wall = GameObject.Find("Wall").GetComponent<WallBehavior>();
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

        public void UnlockFanArrows()
        {
            skillManager.FanArrows = true;

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
		}

        public void UnlockThorns()
        {
            skillManager.Thorns = true;

            AppMetrica.Instance.ReportEvent("Thorns");
            AppMetrica.Instance.SendEventsBuffer();
        }

        /////////////////////////////////////////////////////////////////////////////////

        public void UnlockAddHP()
        {
            skillManager.WallHp += 1;
            wall.maxhealth += 100;
            wall._health += 100;

            AppMetrica.Instance.ReportEvent("WallAddHP");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockBlackFire()
        {
            skillManager.BlackFire = true;

            AppMetrica.Instance.ReportEvent("BlackFire");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockMortarBlessing()
        {
            skillManager.MortarBlessing += 1;

            mortar.projectileSpeed += 1.3f;
            mortar.projectileDamage *= 1.3f;

            if (skillManager.MortarBlessing >= 3)
            {
                GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());
            }

            AppMetrica.Instance.ReportEvent("Blessing");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockBlessingForCrossbow()
        {
            skillManager.CrossbowBlessing += 1;

            crossbow.projectileSpeed += 1.3f;
            crossbow.projectileDamage *= 1.3f;

            if (skillManager.MortarBlessing >= 3)
            {
                GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());
            }

            AppMetrica.Instance.ReportEvent("BlessingForCrossbow");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockColdArrow()
        {
            skillManager.ColdArrow = true;

            AppMetrica.Instance.ReportEvent("ColdArrow");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockColdYadro()
        {
            skillManager.ColdYadro = true;

            AppMetrica.Instance.ReportEvent("ColdYadro");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockPhantomArrow()
        {
            skillManager.PhantomArrow = true;

            AppMetrica.Instance.ReportEvent("PhantomArrow");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockPoisonArrow()
        {
            skillManager.PoisonArrow = true;

            AppMetrica.Instance.ReportEvent("PoisonArrow");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockPoisonYadro()
        {
            skillManager.PoisonYadro = true;

            AppMetrica.Instance.ReportEvent("PoisonYadro");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockVampirism()
        {
            skillManager.Vampirism = true;

            AppMetrica.Instance.ReportEvent("Vampirism");
            AppMetrica.Instance.SendEventsBuffer();
        }
    }
}
