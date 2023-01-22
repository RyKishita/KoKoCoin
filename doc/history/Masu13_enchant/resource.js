var iconAssetName = 'img/icon0.gif';
var iconTileWidth = 16;
var iconTileHeight = 16;
var iconColumnCount = 16;

var backgroundWidth = 320;
var backgroundHeight = 320;
var mainMenuBackAssetName = 'img/mainMenuBack.png';
var targetTypeSelectBackAssetName = 'img/targetTypeSelectBack.png';
var targetSelectBackAssetName = 'img/targetSelectBack.png';
var editBagBackAssetName = 'img/editBagBack.png';
var settingBackAssetName = 'img/settingBack.png';

var menuBoardWidth = 240;
var menuBoardHeight = 240;
var menuBoardAssetName = 'img/menuBoard.png';

var editBagButtonALLAssetName = 'img/buttonALL.png';
var editBagButtonUseAssetName = 'img/buttonUse.png';
var editBagButtonSetAssetName = 'img/buttonSet.png';
var editBagButtonShotAssetName = 'img/buttonShot.png';
var editBagButtonShieldAssetName = 'img/buttonShield.png';
var editBagButtonEndAssetName = 'img/buttonEnd.png';
var editBagButtonSize = 16;

var coinNumMax = 0;
var coinSize1 = 32;
var coinSize2 = 48;
var coinSize3 = 64;
var coinHasMax = 5;

var coinProjectileWidth = 64;
var coinProjectileHeight = 24;
var coinShootAssetName = 'img/coinShoot.png';
var coinReflectAssetName = 'img/coinReflect.png';

var coinShieldingWidth = 24;
var coinShieldingHeight = 64;
var coinShieldingAssetName = 'img/coinShielding.png';

var characterSize = 32;
var characterScale = 2;
var character1AssetName = 'img/chara0.gif';
var character3AssetName = 'img/chara5.gif';
var character4AssetName = 'img/chara6.gif';
var character5AssetName = 'img/chara7.gif';
var characterDirectUp = 3;
var characterDirectDown = 0;
var characterDirectLeft = 1;
var characterDirectRight = 2;
var characterPosY = 175;

var battleSceneWidth = 1280;
var battleSceneHeight = 320;
var battleBackAssetName = 'img/BattleBackground.png';
var battleSetWidth = 1280;
var battleSetHeight = 170;
var battleSetTop = 120;
var battleLifeWidth = 100;
var battleLifeHeight = 16;
var battleTurnSize = 80;
var battleTurnAssetName = 'img/BattleTurn.png';
var battleIconSize = 48;

var battleShowUpWidth = 300;
var battleShowUpHeight = 96;
var battleShowUpAssetName = 'img/BattleShowUp.png';

var coinListWidth = 320;
var coinListHeight = 84;
var coinListAssetName = 'img/CoinList.png';

var explainBoardWidth = 180;
var explainBoardHeight = 80;
var explainBoardAssetName = 'img/explainBoard.png';

var hasCoinSizeMax = 7;

var setEffectWidth = 16;
var setEffectHeight = 16;
var setEffectAssetName = 'img/effect0.gif';

var battleProgressTextWidth = 140;
var battleProgressTextHeight = 30;
var battleProgressTextAssetName = 'img/battleProgress.png';

var resultBackAssetName = 'img/resultBack.png';

var resultWidth = 320;
var resultHeight = 80;
var resultWinAssetName = 'img/resultWin.png';
var resultLoseAssetName = 'img/resultLose.png';

var resultGetCoinTextWidth = 140;
var resultGetCoinTextHeight = 30;
var resultGetCoinTextAssetName = 'img/resultGetCoin.png';

var resultNoCoinTextWidth = 320;
var resultNoCoinTextHeight = 30;
var resultNoCoinTextAssetName = 'img/resultNoCoin.png';

var soundOK = 'sounds/lock4.wav';
var soundCancel = 'sounds/lock4.wav';
var soundSelect = 'sounds/lock3.wav';
var soundSave = 'sounds/se6.wav';
var soundBomb = 'sounds/bomb1.wav';
var soundHit = 'sounds/se9.wav';
var soundReflect = 'sounds/se3.wav';
var soundResult = 'sounds/jingle05.wav';
var soundBattleBGM = 'sounds/bgm07.wav';

function GetAssetList() {
    return new Array(
        iconAssetName,
        mainMenuBackAssetName,
        menuBoardAssetName,
        targetTypeSelectBackAssetName,
        targetSelectBackAssetName,
        settingBackAssetName,
        editBagBackAssetName,
        'img/coin1Set.png',
        'img/coin2Set.png',
        'img/coin3Set.png',
        'img/coin1Shot.png',
        'img/coin2Shot.png',
        'img/coin3Shot.png',
        'img/coin1Shield.png',
        'img/coin2Shield.png',
        'img/coin3Shield.png',
        coinShootAssetName,
        coinReflectAssetName,
        coinShieldingAssetName,
        editBagButtonALLAssetName,
        editBagButtonUseAssetName,
        editBagButtonSetAssetName,
        editBagButtonShotAssetName,
        editBagButtonShieldAssetName,
        editBagButtonEndAssetName,
        character1AssetName,
        character3AssetName,
        character4AssetName,
        character5AssetName,
        battleBackAssetName,
        'img/BattleSetB1.png',
        'img/BattleSetB2.png',
        'img/BattleSetB3.png',
        'img/BattleSetB4.png',
        'img/BattleSetB5.png',
        'img/BattleSetB6.png',
        'img/BattleSetB7.png',
        'img/BattleSetB8.png',
        'img/BattleSetB9.png',
        'img/BattleSetB10.png',
        'img/BattleSetB11.png',
        'img/BattleSetR1.png',
        'img/BattleSetR2.png',
        'img/BattleSetR3.png',
        'img/BattleSetR4.png',
        'img/BattleSetR5.png',
        'img/BattleSetR6.png',
        'img/BattleSetR7.png',
        'img/BattleSetR8.png',
        'img/BattleSetR9.png',
        'img/BattleSetR10.png',
        'img/BattleSetR11.png',
        battleTurnAssetName,
        battleShowUpAssetName,
        coinListAssetName,
        explainBoardAssetName,
        setEffectAssetName,
        battleProgressTextAssetName,
        resultBackAssetName,
        resultWinAssetName,
        resultLoseAssetName,
        resultGetCoinTextAssetName,
        resultNoCoinTextAssetName,
        soundOK,
        soundCancel,
        soundSelect,
        soundSave,
        soundBomb,
        soundHit,
        soundReflect,
        soundResult,
        soundBattleBGM);
}

function SetIconSprite(icon, index, count) {
    var image = new Surface(iconTileWidth * count, iconTileHeight);
    image.draw(enchant.Game.instance.assets[iconAssetName],
        (index % iconColumnCount) * iconTileWidth,
        Math.floor(index / iconColumnCount) * iconTileHeight,
        iconTileWidth * count,
        iconTileHeight,
        0,
        0,
        iconTileWidth * count,
        iconTileHeight);
    icon.image = image;
}

function MoveMenuSelectArrow(arrowIcon, text) {
    arrowIcon.x = text.x - arrowIcon.width - 2;
    arrowIcon.y = text.y - 2;
}

function CreateCoinSprite(typeName, size) {
    var imageSize;
    switch (size) {
        case 1: imageSize = coinSize1; break;
        case 2: imageSize = coinSize2; break;
        case 3: imageSize = coinSize3; break;
    }
    var assetName = 'img/coin' + size + typeName + '.png';

    var sprite = new Sprite(imageSize, imageSize);
    sprite.image = enchant.Game.instance.assets[assetName];
    return sprite;
}

var CoinInfo = Class.create({
    initialize: function (size) {
        this.size = size;
    },
    setExplain: function (labels) {
        return;
    }
});

var CoinInfoSet = Class.create(CoinInfo, {
    initialize: function (size, damage, placeA, placeB, placeC, placeD) {
        CoinInfo.call(this, size);
        this.damage = damage;
        this.placeA = placeA;
        this.placeB = placeB;
        this.placeC = placeC;
        this.placeD = placeD;
    },
    setExplain: function (labels) {
        labels[0].text = '設置型 サイズ' + this.size;
        labels[1].text = 'ダメージ ' + this.damage;
        var text = '配置可能位置';
        if (this.placeA) text += ' A';
        if (this.placeB) text += ' B';
        if (this.placeC) text += ' C';
        if (this.placeD) text += ' D';
        labels[2].text = text;
    },
    toSprite: function () {
        return CreateCoinSprite('Set', this.size);
    },
    canSet: function (areaNo) {
        switch (areaNo) {
            case 0: return false;
            case 1: return this.placeD;
            case 2: return this.placeD;
            case 3: return this.placeC;
            case 4: return this.placeC;
            case 5: return this.placeB;
            case 6: return this.placeA;
            case 7: return this.placeB;
            case 8: return this.placeC;
            case 9: return this.placeC;
            case 10: return this.placeD;
            case 11: return this.placeD;
            case 12: return false;
        }
        return false;
    }
});

var CoinInfoShot = Class.create(CoinInfo, {
    initialize: function (size, damage, range) {
        CoinInfo.call(this, size);
        this.damage = damage;
        this.range = range;
    },
    setExplain: function (labels) {
        labels[0].text = '射撃型 サイズ' + this.size;
        labels[1].text = 'ダメージ ' + this.damage;
        labels[2].text = '射程 ' + this.range.join(' ');
    },
    toSprite: function () {
        return CreateCoinSprite('Shot', this.size);
    },
    canShot: function (areaNo, targetNo) {
        if (targetNo == 0 || targetNo == 12) return false;
        var distance = Math.abs(areaNo - targetNo);
        return (0 <= this.range.indexOf(distance));
    }
});

var CoinInfoShield = Class.create(CoinInfo, {
    initialize: function (size, cutDamage, counterDamage) {
        CoinInfo.call(this, size);
        this.cutDamage = cutDamage;
        this.counterDamage = counterDamage;
    },
    setExplain: function (labels) {
        labels[0].text = '防御型 サイズ' + this.size;
        labels[1].text = 'ダメージ軽減 ' + this.cutDamage;
        labels[2].text = 'ダメージ反射 ' + this.counterDamage;
    },
    toSprite: function () {
        return CreateCoinSprite('Shield', this.size);
    }
});

function GetCoinData() {
    var coinData = new Array();

    var range;

    coinData.push(new CoinInfoSet(1, 100, true, true, true, true));
    coinData.push(new CoinInfoSet(1, 125, true, true, true, false));
    coinData.push(new CoinInfoSet(1, 125, true, true, false, true));
    coinData.push(new CoinInfoSet(1, 125, true, false, true, true));
    coinData.push(new CoinInfoSet(1, 125, false, true, true, true));
    coinData.push(new CoinInfoSet(1, 150, true, true, false, false));
    coinData.push(new CoinInfoSet(1, 150, true, false, true, false));
    coinData.push(new CoinInfoSet(1, 150, true, false, false, true));
    coinData.push(new CoinInfoSet(1, 150, false, true, true, false));
    coinData.push(new CoinInfoSet(1, 150, false, true, false, true));
    coinData.push(new CoinInfoSet(1, 150, false, false, true, true));
    coinData.push(new CoinInfoSet(1, 200, true, false, false, false));
    coinData.push(new CoinInfoSet(1, 200, false, true, false, false));
    coinData.push(new CoinInfoSet(1, 200, false, false, true, false));
    coinData.push(new CoinInfoSet(1, 200, false, false, false, true));
    coinData.push(new CoinInfoSet(2, 200, true, true, true, true));
    coinData.push(new CoinInfoSet(2, 250, true, true, true, false));
    coinData.push(new CoinInfoSet(2, 250, true, true, false, true));
    coinData.push(new CoinInfoSet(2, 250, true, false, true, true));
    coinData.push(new CoinInfoSet(2, 250, false, true, true, true));
    coinData.push(new CoinInfoSet(2, 300, true, true, false, false));
    coinData.push(new CoinInfoSet(2, 300, true, false, true, false));
    coinData.push(new CoinInfoSet(2, 300, true, false, false, true));
    coinData.push(new CoinInfoSet(2, 300, false, true, true, false));
    coinData.push(new CoinInfoSet(2, 300, false, true, false, true));
    coinData.push(new CoinInfoSet(2, 300, false, false, true, true));
    coinData.push(new CoinInfoSet(2, 400, true, false, false, false));
    coinData.push(new CoinInfoSet(2, 400, false, true, false, false));
    coinData.push(new CoinInfoSet(2, 400, false, false, true, false));
    coinData.push(new CoinInfoSet(2, 400, false, false, false, true));
    coinData.push(new CoinInfoSet(3, 300, true, true, true, true));
    coinData.push(new CoinInfoSet(3, 375, true, true, true, false));
    coinData.push(new CoinInfoSet(3, 375, true, true, false, true));
    coinData.push(new CoinInfoSet(3, 375, true, false, true, true));
    coinData.push(new CoinInfoSet(3, 375, false, true, true, true));
    coinData.push(new CoinInfoSet(3, 450, true, true, false, false));
    coinData.push(new CoinInfoSet(3, 450, true, false, true, false));
    coinData.push(new CoinInfoSet(3, 450, true, false, false, true));
    coinData.push(new CoinInfoSet(3, 450, false, true, true, false));
    coinData.push(new CoinInfoSet(3, 450, false, true, false, true));
    coinData.push(new CoinInfoSet(3, 450, false, false, true, true));
    coinData.push(new CoinInfoSet(3, 600, true, false, false, false));
    coinData.push(new CoinInfoSet(3, 600, false, true, false, false));
    coinData.push(new CoinInfoSet(3, 600, false, false, true, false));
    coinData.push(new CoinInfoSet(3, 600, false, false, false, true));
    range = new Array(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
    coinData.push(new CoinInfoShot(1, 100, range));
    range = new Array(3, 4, 5, 6, 7, 8, 9, 10, 11);
    coinData.push(new CoinInfoShot(1, 125, range));
    range = new Array(0, 1, 2, 4, 5, 7, 8, 10, 11);
    coinData.push(new CoinInfoShot(1, 125, range));
    range = new Array(0, 1, 2, 3, 4, 8, 9, 10, 11);
    coinData.push(new CoinInfoShot(1, 125, range));
    range = new Array(1, 2, 3, 4, 5, 6, 7, 8);
    coinData.push(new CoinInfoShot(1, 125, range));
    range = new Array(1, 3, 5, 7, 9, 11);
    coinData.push(new CoinInfoShot(1, 150, range));
    range = new Array(0, 2, 4, 6, 8, 10);
    coinData.push(new CoinInfoShot(1, 150, range));
    range = new Array(0, 1, 2, 3, 4);
    coinData.push(new CoinInfoShot(1, 150, range));
    range = new Array(3, 4, 6, 7, 9, 10);
    coinData.push(new CoinInfoShot(1, 150, range));
    range = new Array(5, 6, 7, 8, 9, 10, 11);
    coinData.push(new CoinInfoShot(1, 150, range));
    range = new Array(0, 1, 7, 8, 9, 10, 11);
    coinData.push(new CoinInfoShot(1, 150, range));
    range = new Array(0, 1, 2);
    coinData.push(new CoinInfoShot(1, 200, range));
    range = new Array(3, 6, 9);
    coinData.push(new CoinInfoShot(1, 200, range));
    range = new Array(5, 6, 7);
    coinData.push(new CoinInfoShot(1, 200, range));
    range = new Array(0, 9, 10, 11);
    coinData.push(new CoinInfoShot(1, 200, range));
    range = new Array(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
    coinData.push(new CoinInfoShot(2, 200, range));
    range = new Array(3, 4, 5, 6, 7, 8, 9, 10, 11);
    coinData.push(new CoinInfoShot(2, 250, range));
    range = new Array(0, 1, 2, 4, 5, 7, 8, 10, 11);
    coinData.push(new CoinInfoShot(2, 250, range));
    range = new Array(0, 1, 2, 3, 4, 8, 9, 10, 11);
    coinData.push(new CoinInfoShot(2, 250, range));
    range = new Array(1, 2, 3, 4, 5, 6, 7, 8);
    coinData.push(new CoinInfoShot(2, 250, range));
    range = new Array(1, 3, 5, 7, 9, 11);
    coinData.push(new CoinInfoShot(2, 300, range));
    range = new Array(0, 2, 4, 6, 8, 10);
    coinData.push(new CoinInfoShot(2, 300, range));
    range = new Array(0, 1, 2, 3, 4);
    coinData.push(new CoinInfoShot(2, 300, range));
    range = new Array(3, 4, 6, 7, 9, 10);
    coinData.push(new CoinInfoShot(2, 300, range));
    range = new Array(5, 6, 7, 8, 9, 10, 11);
    coinData.push(new CoinInfoShot(2, 300, range));
    range = new Array(0, 1, 7, 8, 9, 10, 11);
    coinData.push(new CoinInfoShot(2, 300, range));
    range = new Array(0, 1, 2);
    coinData.push(new CoinInfoShot(2, 400, range));
    range = new Array(3, 6, 9);
    coinData.push(new CoinInfoShot(2, 400, range));
    range = new Array(5, 6, 7);
    coinData.push(new CoinInfoShot(2, 400, range));
    range = new Array(0, 9, 10, 11);
    coinData.push(new CoinInfoShot(2, 400, range));
    range = new Array(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
    coinData.push(new CoinInfoShot(3, 300, range));
    range = new Array(3, 4, 5, 6, 7, 8, 9, 10, 11);
    coinData.push(new CoinInfoShot(3, 375, range));
    range = new Array(0, 1, 2, 4, 5, 7, 8, 10, 11);
    coinData.push(new CoinInfoShot(3, 375, range));
    range = new Array(0, 1, 2, 3, 4, 8, 9, 10, 11);
    coinData.push(new CoinInfoShot(3, 375, range));
    range = new Array(1, 2, 3, 4, 5, 6, 7, 8);
    coinData.push(new CoinInfoShot(3, 375, range));
    range = new Array(1, 3, 5, 7, 9, 11);
    coinData.push(new CoinInfoShot(3, 450, range));
    range = new Array(0, 2, 4, 6, 8, 10);
    coinData.push(new CoinInfoShot(3, 450, range));
    range = new Array(0, 1, 2, 3, 4);
    coinData.push(new CoinInfoShot(3, 450, range));
    range = new Array(3, 4, 6, 7, 9, 10);
    coinData.push(new CoinInfoShot(3, 450, range));
    range = new Array(5, 6, 7, 8, 9, 10, 11);
    coinData.push(new CoinInfoShot(3, 450, range));
    range = new Array(0, 1, 7, 8, 9, 10, 11);
    coinData.push(new CoinInfoShot(3, 450, range));
    range = new Array(0, 1, 2);
    coinData.push(new CoinInfoShot(3, 600, range));
    range = new Array(3, 6, 9);
    coinData.push(new CoinInfoShot(3, 600, range));
    range = new Array(5, 6, 7);
    coinData.push(new CoinInfoShot(3, 600, range));
    range = new Array(0, 9, 10, 11);
    coinData.push(new CoinInfoShot(3, 600, range));
    coinData.push(new CoinInfoShield(1, 400, 0));
    coinData.push(new CoinInfoShield(1, 350, 50));
    coinData.push(new CoinInfoShield(1, 300, 100));
    coinData.push(new CoinInfoShield(1, 250, 150));
    coinData.push(new CoinInfoShield(1, 200, 200));
    coinData.push(new CoinInfoShield(2, 500, 0));
    coinData.push(new CoinInfoShield(2, 450, 50));
    coinData.push(new CoinInfoShield(2, 400, 100));
    coinData.push(new CoinInfoShield(2, 350, 150));
    coinData.push(new CoinInfoShield(2, 300, 200));
    coinData.push(new CoinInfoShield(3, 600, 200));
    coinData.push(new CoinInfoShield(3, 550, 250));
    coinData.push(new CoinInfoShield(3, 500, 300));
    coinData.push(new CoinInfoShield(3, 450, 350));
    coinData.push(new CoinInfoShield(3, 400, 400));

    coinNumMax = coinData.length;

    return coinData;
}

function ReadyCoins() {
    var coins = new Array(coinNumMax);
    for (var i = 0; i < coinNumMax; i++) {
        coins[i] = 0;
    }
    return coins;
}

function SetFirstCoins(coins) {
    coins[0] = 5;
    coins[5] = 5;
    coins[45] = 5;
    coins[52] = 5;
    coins[90] = 3;
}

function CoinListToString(coins) {
    var result = '';
    for (var no in coins) {
        var num = coins[no];
        if (0 == num) continue;

        if (0 < result.length) {
            result += ',';
        }
        result += String(num) + String(no);
    }
    return result;
}

function SetCoinListFromText(coins, text) {
    var parts = text.split(',');
    for (var i in parts) {
        var s = parts[i];
        var no = parseInt(s.substring(1))
        var num = parseInt(s[0]);
        coins[no] = num;
    }
}

function CreateSelectSprite() {
    var sprite = new Sprite(iconTileWidth, iconTileHeight);
    SetIconSprite(sprite, 42, 1);
    return sprite;
}

function CreateArrowSprite() {
    var sprite = new Sprite(iconTileWidth, iconTileHeight);
    SetIconSprite(sprite, 43, 1);
    return sprite;
}

function CreateMenuBoardSprite() {
    var sprite = new Sprite(menuBoardWidth, menuBoardHeight);
    sprite.image = enchant.Game.instance.assets[menuBoardAssetName];
    return sprite;
}

function CreateEditBagButtonSprite(type) {
    var assetName;
    switch (type) {
        case 0: assetName = editBagButtonALLAssetName; break;
        case 1: assetName = editBagButtonUseAssetName; break;
        case 2: assetName = editBagButtonSetAssetName; break;
        case 3: assetName = editBagButtonShotAssetName; break;
        case 4: assetName = editBagButtonShieldAssetName; break;
        case 5: assetName = editBagButtonEndAssetName; break;
    }
    var sprite = new Sprite(editBagButtonSize, editBagButtonSize);
    sprite.image = enchant.Game.instance.assets[assetName];
    return sprite;
}

function CreatePlayerSurface(no) {
    var assetName;
    var x;
    switch (no) {
        case 0:
            assetName = character1AssetName;
            x = 0;
            break;
        case 1:
            assetName = character1AssetName;
            x = 96;
            break;
        case 2:
            assetName = character1AssetName;
            x = 192;
            break;
        case 3:
            assetName = character3AssetName;
            x = 0;
            break;
        case 4:
            assetName = character4AssetName;
            x = 0;
            break;
        case 5:
            assetName = character4AssetName;
            x = 96;
            break;
        case 6:
            assetName = character5AssetName;
            x = 0;
            break;
    }

    var surface = new Surface(96, 128);
    surface.draw(
        enchant.Game.instance.assets[assetName],
        x, 0, 96, 128, 0, 0, 96, 128);
    return surface;
}

function SetEnemyCoins(coins, no) {
    switch (no) {
        case 0:
            coins[0] = 7;
            coins[45] = 7;
            break;
        case 1:
            coins[34] = 8;
            coins[94] = 4;
            break;
        case 2:
            coins[19] = 4;
            coins[65] = 4;
            coins[88] = 1;
            coins[92] = 4;
            break;
    }
}

function ShuffleCoins(coins) {
    var result = new Array();
    while (0 < coins.length) {
        var index = Math.floor(Math.random() * coins.length);
        result.push(coins.splice(index, 1)[0]);
    }
    return result;
}

var CharactorInfo = Class.create({
    initialize: function () {
        this.useCoins = ReadyCoins();
        this.keepShieldNum = 1;
        this.keepSize = 0;
        this.characterNo = 0;
        this.score = 0;
    },
    toPlayerSurface: function () {
        return CreatePlayerSurface(this.characterNo);
    },
    toSprite: function () {
        return new Sprite(characterSize, characterSize);
    },
    toString: function () {
        return 'エラー';
    },
    makeStackCoin: function () {
        var coins = new Array();
        for (var index in this.useCoins) {
            var n = this.useCoins[index];
            for (var i = 0; i < n; i++) {
                coins.push(parseInt(index));
            }
        }

        return ShuffleCoins(coins);
    }
});

var CharactorInfoPractice = Class.create(CharactorInfo, {
    initialize: function (no) {
        CharactorInfo.call(this);
        SetEnemyCoins(this.useCoins, no);
        this.no = no;
        switch (no) {
            case 0: this.characterNo = 4; break;
            case 1: this.characterNo = 5; break;
            case 2: this.characterNo = 6; break;
        }
    },
    toSprite: function () {
        /*character用画像の1フレーム目をアイコン化*/
        var icon = new Surface(characterSize, characterSize);
        icon.draw(this.toPlayerSurface(),
            0, 0, characterSize, characterSize,
            0, 0, characterSize, characterSize);

        var sprite = new Sprite(characterSize, characterSize);
        sprite.image = icon;
        return sprite;
    },
    toString: function () {
        switch (this.no) {
            case 0: return '[練習]よわい';
            case 1: return '[練習]ふつう';
            case 2: return '[練習]つよい';
        }
        return 'エラー';
    }
});

var CharactorInfoUser = Class.create(CharactorInfo, {
    initialize: function (userData, isRanker) {
        CharactorInfo.call(this);

        this.userData = userData;
        this.isRanker = isRanker;

        SetCoinListFromText(this.useCoins, userData.data.useCoins);
        this.keepShieldNum = userData.data.keepShieldNum;
        this.keepSize = userData.data.keepSize;
        this.characterNo = userData.data.characterNo;
        this.score = userData.score;
    },
    toSprite: function () {
        return this.userData.toSprite();
    },
    toString: function () {
        return this.userData.name + '(@' + this.userData.screen_name + ')';
    }
});

var BattleSetSprite = Class.create(Sprite, {
    initialize: function (color, areaNo) {
        Sprite.call(this, battleSetWidth, battleSetHeight);
        this.coinNo = -1;
        var assetName = 'img/BattleSet' + color + areaNo + '.png';
        this.image = enchant.Game.instance.assets[assetName];
    }
});

function GetScrollPos(no) {
    return no * 80;
}

function GetBattleAreaX(areaNo) {
    return 80 + 90 * areaNo;
}

function GetBattleTurnSprite() {
    var sprite = new Sprite(battleTurnSize, battleTurnSize);
    sprite.image = enchant.Game.instance.assets[battleTurnAssetName];
    return sprite;
}

var CharacterSprite = Class.create(Sprite, {
    initialize: function () {
        Sprite.call(this, characterSize, characterSize);
        this.scale(characterScale);
        this.y = characterPosY;

        this.isMoving = false;
        this.direction = characterDirectDown;
        this.walk = 1;
        this.vx = 0;
        this.areaNo = 0;
    },
    setPos: function (no) {
        this.areaNo = no;
        this.x = GetBattleAreaX(no);
    },
    moveStart: function (no) {
        var targetX = GetBattleAreaX(no);
        this.vx = Math.floor((targetX - this.x) / 30);
        this.isMoving = (this.vx != 0);
        if (this.isMoving) {
            if (no < this.areaNo) {
                this.direction = characterDirectLeft;
                if (-6 < this.vx) {
                    this.vx = -6;
                }
            } else {
                this.direction = characterDirectRight;
                if (this.vx < 6) {
                    this.vx = 6;
                }
            }
        } else {
            this.x = targetX;
        }
        this.areaNo = no;
    },
    updateMove: function () {
        if (!this.isMoving) return;
        this.walk++;
        this.walk %= 3;
        this.updateFrame();

        this.x += this.vx;
        var targetX = GetBattleAreaX(this.areaNo);
        if (this.vx < 0) {
            if (targetX < this.x) return;
        } else {
            if (this.x < targetX) return;
        }

        this.x = targetX;
        this.isMoving = false;
    },
    updateFrame: function () {
        this.frame = this.direction * 3 + this.walk;
    },
    turnAround: function (othreAreaNo) {
        if (this.areaNo < othreAreaNo) {
            this.direction = characterDirectRight;
        } else if (othreAreaNo < this.areaNo) {
            this.direction = characterDirectLeft;
        } else {
            this.direction = characterDirectDown;
        }
        this.updateFrame();
    }
});

function GetBattleShowUpSprite() {
    var sprite = new Sprite(battleShowUpWidth, battleShowUpHeight);
    sprite.image = enchant.Game.instance.assets[battleShowUpAssetName];
    return sprite;
}

function GetCoinListSprite() {
    var sprite = new Sprite(coinListWidth, coinListHeight);
    sprite.image = enchant.Game.instance.assets[coinListAssetName];
    return sprite;
}

function GetExplainBoardSprite() {
    var sprite = new Sprite(explainBoardWidth, explainBoardHeight);
    sprite.image = enchant.Game.instance.assets[explainBoardAssetName];
    return sprite;
}

function CreateDiceSprite() {
    var sprite = new Sprite(iconTileWidth, iconTileHeight);
    sprite.scale(3.0);
    SetIconSprite(sprite, 35, 7);
    sprite.setDice = function () { this.frame = 1 + Math.floor(Math.random() * 6); };
    return sprite;
}

function GetResultGetCoinTextSprite() {
    var sprite = new Sprite(resultGetCoinTextWidth, resultGetCoinTextHeight);
    sprite.image = enchant.Game.instance.assets[resultGetCoinTextAssetName];
    return sprite;
}

function GetResultNoCoinTextSprite() {
    var sprite = new Sprite(resultNoCoinTextWidth, resultNoCoinTextHeight);
    sprite.image = enchant.Game.instance.assets[resultNoCoinTextAssetName];
    return sprite;
}

var CoinProjectileSprite = Class.create(Sprite, {
    initialize: function (type) {
        Sprite.call(this, coinProjectileWidth, coinProjectileHeight);
        this.y = characterPosY + characterSize / 2;
        switch (type) {
            case 0:
                this.image = enchant.Game.instance.assets[coinShootAssetName];
                break;
            case 1:
                this.image = enchant.Game.instance.assets[coinReflectAssetName];
                break;
        }

        this.visible = false;
        this.isMoving = false;
        this.vx = 0;
        this.targetX = 0;
    },
    moveStart: function (shooter, target) {
        this.x = shooter.x;
        this.targetX = target.x;
        if (shooter.x < target.x) {
            this.targetX -= this.width;
        }
        if (target.x < shooter.x) {
            this.targetX += target.width;
        }
        this.isMoving = this.visible = (this.x != this.targetX);
        if (this.isMoving) {
            this.vx = Math.floor((this.targetX - this.x) / 20);

            var minVX = 12;
            if (this.vx < 0) {
                if (-minVX < this.vx) this.vx = -minVX;
            } else {
                if (this.vx < minVX) this.vx = minVX;
            }
        } else {
            this.vx = 0;
            this.x = this.targetX;
        }
    },
    updateMove: function () {
        if (!this.isMoving) return;

        this.x += this.vx;
        if (this.vx < 0) {
            if (this.targetX < this.x) return;
        } else {
            if (this.x < this.targetX) return;
        }

        this.x = this.targetX;
        this.isMoving = false;
        this.visible = false;
    }
});

var CoinShieldingSprite = Class.create(Sprite, {
    initialize: function () {
        Sprite.call(this, coinShieldingWidth, coinShieldingHeight);
        this.y = characterPosY + (characterSize - coinShieldingHeight) / 2;
        this.image = enchant.Game.instance.assets[coinShieldingAssetName];

        this.visible = false;
    },
    show: function (currentX, otherX) {
        if (currentX < otherX){
            this.x = currentX + characterSize;
            this.frame = 1;
        } else {
            this.x = currentX;
            this.frame = 0;
        }
        this.visible = true;
    }
});

var SetEffectSprite = Class.create(Sprite, {
    initialize: function () {
        Sprite.call(this, setEffectWidth, setEffectHeight);
        this.image = enchant.Game.instance.assets[setEffectAssetName];

        this.y = characterPosY + characterSize - setEffectHeight/2;
        this.scale(2.0);
    },
    show: function (no) {
        this.x = GetBattleAreaX(no) + setEffectWidth/2;
        this.frame = 0;
        this.visible = true;
    },
    updateFrame: function () {
        if (!this.visible) return;
        if (this.frame == 4) {
            this.visible = false;
        } else {
            this.frame++;
        }
    }
});

var BattleProgressSprite = Class.create(Sprite, {
    initialize: function () {
        Sprite.call(this, battleProgressTextWidth, battleProgressTextHeight);
        this.image = enchant.Game.instance.assets[battleProgressTextAssetName];
    }
});

function PlaySound(name) {
    enchant.Game.instance.assets[name].clone().play();
}
