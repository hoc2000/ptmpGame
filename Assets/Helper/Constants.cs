using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{

    public const int MAX_LEVEL = 30;

    public static class TAG
    {
        public const string PLAYER = "Player";
        public const string WEAPON = "Weapon";
        public const string ENEMY = "Enemy";
        public const string NEXTLEVEL = "DoorComplete";
        public const string TRAP = "Trap";
        public const string CHECK_POINT = "CheckPoint";
        public const string WATER = "Water";
        public const string DEADWATER = "DeadWater";
        public const string SPIKE = "Spike";
        public static string GROUND = "Ground";

    }

    public static class LAYER
    {
        public const int GROUND = 6;
        public const int DEADWATER = 16;
        public const int FORGOTTEN = 31;
        public const int PLAYER = 9;
    }

    public static class KEY
    {
        public const string LAST_DATE_TIME_FREE_SPIN = "last_date_time_free_spin";
        public const string COUNT_SPIN_IN_DAY = "count_spin_in_day";
        //public const string LAST_DATE_TIME_FREE_SPIN = "last_date_time_free_spin";
        public const string OLD_PRICES = "old_prices";
        public const string COUNT_SPIN_LUCKY = "count_spin_lucky";
        public const string RESOURCE_PREFIX = "res_";
        public const string IAP_IS_BOUGHT_PREFIX = "iap_is_bought_prefix_";
        public const string IS_IAP_USER = "is_iap_user";
        public const string IS_VIP = "is_vip";
        public const string LAST_RECEIVED_VIP_DAILY_REWARD = "last_received_vip_daily_reward";
        public const string LEVEL_UNLOCK = "level_unlock";
        public const string SELECTED_CHARACTER = "selected_character";
        public const string SELECTED_CHARACTER_WITH_WEAPON = "selected_character_with_weapon";
        public const string TRY_FREE_CHARACTER = "try_free_character";
        public const string RESCUE_COUNT = "rescue_count";
        public const string REMOVE_ADS = "remove_ads";
        public const string LEVEL_PASS = "level_pass";

        public const string UP_COIN_RECEIVED = "total_coins_receive";
        public const string UP_GEM_RECEIVED = "total_gem_receive";

        public const string LAST_DAILY_REWARD_CLAIM = "last_daily_reward_claim";
        public const string LAST_DAILY_REWARD_DAY_INDEX = "last_daily_reward_day_index";
        public const string FIRST_OPEN_TIME = "first_open_time";
        public const string LAST_OPENED = "last_opened";

    }

    public static class SCENE
    {
        public const int SCENE_LOADING = 0;
        public const int SCENE_HOME = 1;
        public const int SCENE_GAMEPLAY = 2;
    }
    public static class POOLING
    {
        public const string RAGDOLLENEMY1 = "ragdollEnemy1";
        public const string RAGDOLLENEMY2 = "ragdollEnemy2";
        public const string RAGDOLLENEMY3 = "ragdollEnemy3";
        public const string RAGDOLLENEMY4 = "ragdollEnemy4";
        public const string RAGDOLLENEMY5 = "ragdollEnemy5";
        public const string RAGDOLLENEMY6 = "ragdollEnemy6";
        public const string HITFX = "hitFx";
        public const string BLOODENEMY = "bloodFxEnemy";
        public const string SMOKE = "smoke";
        public const string FXBOSSDEATH = "fxBossDeath";
        public const string FXBOSSJUMP = "FxBossJump";
        public const string PLAYERDEATHFX = "PlayerDeathFx";
    }
    public static class AUDIO
    {
        public const string TAP = "Tap";
        public const string PLAYERJUMP = "PlayerJump";
        public const string PLAYERDASH = "PlayerDash";
        public const string PLAYERATTACK = "PlayerAttack";
        public const string CHECKPOINT = "CheckPoint";
        public const string COINCOLLECT = "CoinCollect";
        public const string HEALING = "Healing";
        public const string OPENCHEST = "OpenChest";
        public const string ZOMBIEATTACK = "ZombieAttack";
        public const string ZOMBIEDEATH = "ZombieDeath";
        public const string ENEMY2ATTACK= "Enemy2Attack";
        public const string ARROWIMPACT = "ArrowImpact";
        public const string HIT = "Hit";
        public const string SWORDATTACK = "SwordAttack";
        public const string SWORDATTACK2 = "SwordAttack2";
        public const string SWORDATTACK3 = "SwordAttack3";
        public const string BOSS1ROAR = "Boss1Roar";
        public const string EXPLOSION = "Explosion";
        public const string EXPLOSIONBOSS = "ExplosionBoss";
        public const string COLLAPSE = "Collapse";
        public const string LASERSHOOT = "LaserShoot";
        public const string TELEPORT = "Teleport";
    }
}
