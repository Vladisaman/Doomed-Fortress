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
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("ballista_double_shot_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockFireGunLeft()
        {
            skillManager.fireGunleftFireEnable = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("flamethrower_left_firegun_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }        
        public void UnlockFireGunRight()
        {
            skillManager.fireGunrightFireEnable = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("flamethrower_right_firegun_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockBigYadro()
        {
            skillManager.bigYadroEnable = true;
            skillManager.smallYadroEnable= false;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("cannon_big_cannonball_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockSmallYadro()
        {
            skillManager.bigYadroEnable = false;
            skillManager.smallYadroEnable = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("cannon_small_cannonball_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockFanArrows()
        {
            skillManager.FanArrows = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("ballista_five_arrow_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockFireMortar()
        {
            skillManager.fireBomb = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("mortar_fire_bomb_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockHolyMortar()
        {
            skillManager.holyBomb = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("mortar_holy_bomb_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockCursedMortar()
        {
            skillManager.cursedBomb = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("mortar_cursed_bomb_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockKnockbackMortar()
        {
            skillManager.knockbackCannonball = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("mortar_knockback_bomb_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockHolyCrossbow()
        {
            skillManager.holyArrow = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("crossbow_holy_arrow_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockCursedCrossbow()
        {
            skillManager.cursedArrow = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("crossbow_cursed_arrow_skill");
		}

        public void UnlockThorns()
        {
            skillManager.Thorns = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("wall_thorns_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void UnlockAddHP()
        {
            skillManager.WallHp += 1;
            wall.maxhealth += 100;
            wall._health += 100;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("wall_hp_boost_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockBlackFire()
        {
            skillManager.BlackFire = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("firegun_black_flame_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockMortarBlessing()
        {
            skillManager.MortarBlessing += 1;

            mortar.reloadTime -= 0.1f;
            mortar.projectileDamage *= 1.3f;

            if (skillManager.MortarBlessing >= 3)
            {
                GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());
            }

            AppMetrica.Instance.ReportEvent("mortar_blessing_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockBlessingForCrossbow()
        {
            skillManager.CrossbowBlessing += 1;

            crossbow.reloadTime -= 0.1f;
            crossbow.projectileDamage *= 1.3f;

            if (skillManager.MortarBlessing >= 3)
            {
                GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());
            }

            AppMetrica.Instance.ReportEvent("crossbow_blessing_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockColdArrow()
        {
            skillManager.ColdArrow = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("crossbow_cold_arrow_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockColdYadro()
        {
            skillManager.ColdYadro = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("mortar_cold_bomb_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockPhantomArrow()
        {
            skillManager.PhantomArrow = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("crossbow_phantom_arrow_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockPoisonArrow()
        {
            skillManager.PoisonArrow = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("crossbow_poison_arrow_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockPoisonYadro()
        {
            skillManager.PoisonYadro = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("mortar_poison_bomb_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }
		
        public void UnlockVampirism()
        {
            skillManager.Vampirism = true;
            GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>().skills.Remove(GetComponentInParent<SkillButton>());

            AppMetrica.Instance.ReportEvent("crossbow_vampirism_skill");
            AppMetrica.Instance.SendEventsBuffer();
        }
    }
}