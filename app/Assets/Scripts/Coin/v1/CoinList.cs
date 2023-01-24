using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Coin.v1.Main;

namespace Assets.Scripts.Coin.v1
{
    class CoinList : VersionCoinList
    {
        private CoinList()
        {
            // 補助と環境
            Regist(new ChangeAreaType(Defines.AreaType.Null));
            Regist(new ChangeAreaType(Defines.AreaType.A));
            Regist(new ChangeAreaType(Defines.AreaType.B));
            Regist(new ChangeAreaType(Defines.AreaType.C));
            Regist(new ChangeAreaType(Defines.AreaType.D));
            Regist<ClearMyCurse>();
            Regist<ClearMyGravity>();
            Regist<ClearMyHeat>();
            Regist<ClearMyInebriety>();
            Regist<ClearMyPoison>();
            Regist<ClearMyShock>();
            Regist<ClearMyVirus>();
            Regist<ClearMyStatusALL>();
            Regist<ClearSetStatus>();
            Regist<ClearSetStatusALL>();
            Regist<ClearAreaStatus>();
            Regist<ClearALLAreaStatus>();
            Regist<AfterShot>();
            Regist<ExpandArea>();
            Regist<NoNeedCoin>();
            Regist<ExpandTurnResource>();
            Regist<ExpandPack>();
            Regist<ProgressTurnALLSetCoins>();
            Regist<ProgressTurnAreaSetCoins>();
            Regist<ProgressTurnMyHand>();
            Regist<ProgressTurnSetCoin>();
            Regist(new FixDice(1));
            Regist(new FixDice(2));
            Regist(new FixDice(3));
            Regist(new FixDice(4));
            Regist(new FixDice(5));
            Regist(new FixDice(6));
            Regist<DecrementDice>();
            Regist<IncrementDice>();
            Regist<ReverseSet>();
            Regist<MakeCopyOfSetCoin>();
            Regist(new AppendConditionDamage("Fire", nameof(Duel.PlayerCondition.PlayerConditionDetailFire), 20));
            Regist(new AppendConditionDamage("Poison", nameof(Duel.PlayerCondition.PlayerConditionDetailPoison), 20));
            Regist<TurnAdd>();
            Regist<MustUse>();
            Regist<ReturnSetToHand>();
            Regist<ReturnTrashToHand>();
            Regist<ConditionCount>();
            Regist<MyTrashToExclusion>();
            Regist<InvalidEffectSetCoin>();
            Regist<DestroyALLCopySetCoins>();
            Regist<SetBooster>();
            Regist<ShotBooster>();
            Regist<GuardBooster>();

            // テーマなし
            Regist<SmallPhysicsShield>();
            Regist<PhysicsShield>();
            Regist<LargePhysicsShield>();
            Regist<SmallMagicShield>();
            Regist<MagicShield>();
            Regist<LargeMagicShield>();
            Regist<SpikeShield>();
            Regist<ProtoFullProtectionShield>();
            Regist<MassProFullProtectionShield>();
            Regist<ProtoFullReflectionShield>();
            Regist<MassProFullReflectionShield>();
            Regist<LastHopeShield>();
            Regist<BlessedShield>();
            Regist<ConcurrentShield>();
            Regist<GrowUpShield>();
            Regist<ShieldSpirit>();
            Regist<GuardFaintness>();
            Regist<ImpactAbsorbeShield>();
            Regist<ImpactAbsorbeWall>();
            Regist<CuttingRegistShield>();
            Regist<ScaleArmor>();
            Regist<Manifer>();
            Regist<Buckler>();
            Regist<Camouflage>();
            Regist<Invisible>();
            Regist<MystBody>();
            Regist<BricksWall>();
            Regist<Dodge>();
            Regist<SmallShield>();
            Regist<Shield>();
            Regist<LargeShield>();
            Regist<Decoy>();
            Regist<NeedleArmor>();

            Regist<BlessedArrow>();
            Regist<ConcurrentArrow>();
            Regist<InfinityArrow>();
            Regist<BloodDagger>();
            Regist<SlingShot>();
            Regist<DuplicateArrow>();
            Regist<HeavyAttack>();
            Regist<ShotSpirit>();
            Regist<IronDagger>();
            Regist<IronSword>();
            Regist<IronLongSword>();
            Regist<StoneMissile>();
            Regist<Meteor>();
            Regist<FairySting>();
            Regist<CrowdStones>();
            Regist<ToyGun>();
            Regist<EarthQuake>();
            Regist<ColdRain>();
            Regist<CurseSword>();
            Regist<GoldenArrow>();

            Regist<RisingStar>();
            Regist<RisingStarFan>();
            Regist<BlessedTrap>();
            Regist<ConcurrentTrap>();
            Regist<IncertitudeTrap>();
            Regist<BlastTrap>();
            Regist<Diabrosis>();
            Regist<Scapegoat>();
            Regist<FusionBomb>();
            Regist<CowardiceBeetle>();
            Regist<MagicCollector>();
            Regist<ManEater>();
            Regist<GraveKeeper>();
            Regist<PropagateVirus>();
            Regist<CorpseShoulder>();
            Regist<BeastTamer>();
            Regist<MachineRider>();
            Regist<GoldenTrap>();
            Regist<CoinCollector>();
            Regist<Fighter>();
            Regist<Sorcerer>();
            Regist<VeteranFighter>();
            Regist<MoanBeast>();
            Regist<BeastPackHead>();
            Regist<TrashWatcher>();

            // テーマに含まれないコインのうち、ステータス付与するもの
            Regist<SmallPoisonShield>();
            Regist<PoisonShield>();
            Regist<CurseSign>();
            Regist<AnesthetizeShield>();
            Regist<FireWall>();
            Regist<ElectricShield>();
            Regist<ElectricBarrier>();
            Regist<HeavyLargeShield>();
            Regist<SkyAbsolver>();
            Regist<Confusion>();
            Regist<BioShield>();
            Regist<BioWall>();

            Regist<CurseBook>();
            Regist<CurseGhost>();
            Regist<CurseMusicBox>();
            Regist<CurseSong>();
            Regist<FlameProjector>();
            Regist<BottleGrenade>();
            Regist<Cannon>();
            Regist<GravityGun>();
            Regist<BlockLook>();
            Regist<Qualm>();
            Regist<DimensionSlash>();
            Regist<BioBlaster>();
            Regist<VirusScatter>();
            Regist<VirusGun>();
            Regist<DeepSleep>();
            Regist<Slump>();
            Regist<Fireball>();
            Regist<PoisonArrow>();
            Regist<PoisonKnife>();
            Regist<OilBottle>();

            Regist<SickRat>();
            Regist<Fairy>();
            Regist<BurningFairy>();
            Regist<GravityMaker>();
            Regist<DozeFog>();
            Regist<SmallPoisonTrap>();
            Regist<PoisonTrap>();
            Regist<PoisonEater>();
            Regist<GermHole>();

            // 移動
            Regist<NoTrespassing>();
            Regist<PushBlower>();
            Regist<HardPushBlower>();
            Regist<ReflectionWave>();

            // コイン操作
            Regist<Redraw>();
            Regist<MyStockToTrash>();
            Regist<MyTrashToStock>();
            Regist<SwapSetBySideArea>();

            // 特殊勝利
            Regist<RevolutionByCrowd>();
            Regist<WinFlag>();
            Regist<Seizure>();

            // テーマ：実力を発揮するまで時間がかかる
            Regist<BattleDoll1th>();
            Regist<BattleDoll2th>();
            Regist<BattleDoll3th>();
            Regist<ElectricGiant>();
            Regist<ElectricAutomaton>();
            Regist<ElectricKing>();
            Regist<ToyPorn>();
            Regist<ToyFighter>();
            Regist<ToyGiant>();
            Regist<CurseDoll>();

            Regist<ThunderPunch>();
            Regist<GreatThunder>();
            Regist<Thunder>();
            Regist<ElectricBurst>();
            Regist<SelfDynamo>();
            Regist<ThunderFairy>();

            // テーマ：アンデッド軍。基本ダメージがゼロで、タグ[アンデッド]でのみダメージアップ
            Regist<UndeadCurse>();
            Regist<VirusCarrier>();
            Regist<UndeadSoldier>();
            Regist<UndeadLeader>();
            Regist<UndeadCommander>();
            Regist<UndeadShoot>();
            Regist<UndeadGuard>();

            // テーマ：自身がエリアAにいると強い
            Regist<KingSword>();
            Regist<KingArrow>();
            Regist<KingPressure>();
            Regist<KingShield>();

            // テーマ：エリアDやSafeなど、外側のエリアで威力が上がる
            Regist<BottleRocket>();
            Regist<ClubOfPoor>();
            Regist<StoneOfPoor>();
            Regist<ChopperOfPoor>();

            // テーマ：コイン「ドラゴン」を主軸にする。配置できれば強力だが、配置条件が厳しい
            Regist<Dragon>();
            Regist<DragonEgg>();
            Regist<PhantomSoldier>();
            Regist<DragonUndead>();
            Regist<FireLizard>();

            // テーマ：植物。コイン「肥料」の設置が必要。配置エリア指定なし
            Regist<Manure>();
            Regist<SpikeFlower>();
            Regist<PoisonFlower>();
            Regist<GravityFlower>();
            Regist<HardTree>();
            Regist<Planter>();
            Regist<GiantTree>();
            Regist<PlantNeedle>();
            Regist<PlantParty>();
            Regist<BigWorm>();

            // テーマ：コイン「狡猾な魔術師」を主軸にする。ArtfulMagician以外はArtfulMagicianの身代わりで死ぬ
            Regist<ArtfulMagician>();
            Regist<MagiciansStrawN>();
            Regist<MagiciansStrawP>();
            Regist<MagiciansStrawA>();

            // テーマ：コイン「女王アリ」を主軸にする。前述の女王蜂と似ているが、こちらのほうが女王含めてコインサイズが小さく、女王が登場すると強化
            Regist<QueenAnt>();
            Regist<KnightAnt>();
            Regist<SoldierAnt>();
            Regist<ChildcareAnt>();
            Regist<ExplorerAnt>();
            Regist<FlameAnt>();

            // テーマ：デーモンシリーズ。置くと他の部位も配置される
            Regist<DaemonHead>();
            Regist<DaemonBody>();
            Regist<DaemonRightArm>();
            Regist<DaemonLeftArm>();
            Regist<DaemonRightLeg>();
            Regist<DaemonLeftLeg>();

            // テーマ：決められたコインの所持が使用条件のコインを主体にする
            Regist<DimensionBeacon>();
            Regist<AlianHuman>();
            Regist<AlianMachine>();
            Regist<AlianDragon>();
            Regist<AlianBeast>();
            Regist<AlianBoundary>();
            Regist<AlienFlame>();

            // テーマ：時間と共に上位武器になるがトークンとして取得するので再度進化はしない。最上位は取得不可
            Regist<ShadowDagger>();
            Regist<ShadowSword>();
            Regist<ShadowLongSword>();
            Regist<ShadowClaymore>();

            // テーマ：レールガン。超射程のみかつ帯電が必要で使いづらいが威力は高い
            Regist<PrototRailgun>();
            Regist<MassProRailgun>();
            Regist<PortableRailgun>();

            // テーマ：しばらくすると溶けて無くなる
            Regist<IcicleSpear>();
            Regist<IciclePic>();
            Regist<IcicleClub>();
            Regist<IceGolem>();
            Regist<IceArmor>();

            // テーマ：設置が必要な直接攻撃コイン。サイズ1のみ。設置に一体のみが付いていないので割と使いやすい
            Regist<HeresyIdol>();
            Regist<HeresyBishop>();
            Regist<HeresyBlowgun>();
            Regist<HeresyClaw>();
            Regist<HeresyTrident>();

            // テーマ：順番に使う必要がある直接攻撃。ただし、一度成立すれば撃ち放題
            Regist<FirstArrow>();
            Regist<SecondArrow>();
            Regist<ThirdArrow>();

            // テーマ：忍者、忍術
            Regist<NinjaSword>();
            Regist<Ninja>();
            Regist<ChiefNinjaSword>();
            Regist<ChiefNinja>();
        }

        protected override string VersionName => "v1";

        public static CoinList Instance { get; } = new CoinList();

        public override bool IsMatch(Defines.CoinVersion coinVersion)
        {
            return coinVersion.HasFlag(Defines.CoinVersion.v1);
        }
    }
}
