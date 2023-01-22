var sceneTitleOut = 'A';
var sceneMainMenu = 'B';
var sceneSetting = 'C';
var sceneTargetTypeSelect = 'D';
var sceneTargetSelect = 'E';
var sceneEditBag = 'F';
var sceneBattle = 'G';
var sceneResult = 'H';

var Event_SceneExStarting = 'SceneExStarting';

var GameData = Class.create(CharactorInfo, {
    initialize: function (coinDatas) {
        CharactorInfo.call(this);
        this.hasCoins = ReadyCoins();

        this.coinDatas = coinDatas;
        this.scenes = new Array();
        this.targetData = new CharactorInfo();
        this.result = false; /*true:プレイヤーが勝利 false:敵が勝利*/
    },
    load: function () {
        var player = enchant.Game.instance.memory.player;
        if (player.data && player.data.useCoins) {
            SetCoinListFromText(this.useCoins, player.data.useCoins);
            SetCoinListFromText(this.hasCoins, player.data.hasCoins);
            this.keepShieldNum = player.data.keepShieldNum;
            this.keepSize = player.data.keepSize;
            this.characterNo = player.data.characterNo;
        } else {
            SetFirstCoins(this.useCoins);
            SetFirstCoins(this.hasCoins);
        }
        this.score = Math.max(1, player.score);
    },
    saveUserData: function () {
        if (enchant.nineleap.memory.LocalStorage.DEBUG_MODE) return;
        var memory = enchant.Game.instance.memory;

        memory.player.data.useCoins = CoinListToString(this.useCoins);
        memory.player.data.hasCoins = CoinListToString(this.hasCoins);
        memory.player.data.keepShieldNum = this.keepShieldNum;
        memory.player.data.keepSize = this.keepSize;
        memory.player.data.characterNo = this.characterNo;

        memory.update();
    },
    saveEnd: function () {
        if (enchant.nineleap.memory.LocalStorage.DEBUG_MODE) return;
        this.saveUserData();

        var coinNum = 0;
        for (var no in this.hasCoins) {
            coinNum += this.hasCoins[no];
        }

        enchant.Game.instance.end(this.score, 'コイン保持数' + coinNum);
    },
    getTwiterData: function () {
        if (enchant.nineleap.memory.LocalStorage.DEBUG_MODE) return null;
        return enchant.Game.instance.twitterAssets['account/verify_credentials'][0];
    },
    toSprite: function () {
        if (enchant.nineleap.memory.LocalStorage.DEBUG_MODE) {
            /*character用画像の1フレーム目をアイコン化*/
            var icon = new Surface(characterSize, characterSize);
            icon.draw(this.toPlayerSurface(),
                0, 0, characterSize, characterSize,
                0, 0, characterSize, characterSize);

            var sprite = new Sprite(characterSize, characterSize);
            sprite.image = icon;
            return sprite;
        } else {
            return this.getTwiterData().toSprite();
        }
    },
    toString: function () {
        if (enchant.nineleap.memory.LocalStorage.DEBUG_MODE) {
            return 'player';
        } else {
            return enchant.Game.instance.memory.player.name;
        }
    }
});

var SceneEx = Class.create(Scene, {
    initialize: function (gameData) {
        Scene.call(this);
        this.gameData = gameData;
        this._opacity = 1;
        this.waitCount = 0;
        this.setFadeIn = false;
        this.setFadeOut = false;
        this.nextSceneName = '';
    },
    opacity: {
        get: function () {
            return this._opacity;
        },
        set: function (v) {
            this._opacity = v;

            for (var g in this.childNodes) {
                for (var n in this.childNodes[g].childNodes) {
                    this.childNodes[g].childNodes[n].opacity = v;
                }
            }
        }
    },
    fadeIn: function () {
        if (this.opacity < 0.99) {
            this.opacity += 0.1;
            return false;
        } else {
            this.opacity = 1;
            return true;
        }
    },
    fadeOut: function () {
        if (0.01 < this.opacity) {
            this.opacity -= 0.1;
            return false;
        } else {
            this.opacity = 0;
            return true;
        }
    },
    inputWait: function () {
        if (1 == arguments.length) {
            this.waitCount = arguments[0];
        } else {
            this.waitCount = 2;
        }
    },
    commonProcess: function () {
        if (this.setFadeIn) {
            if (!this.fadeIn()) return true;
            this.setFadeIn = false;
        }
        if (this.setFadeOut) {
            if (this.fadeOut()) {
                this.setFadeOut = false;

                var nextScene = this.gameData.scenes[this.nextSceneName];
                nextScene.setFadeIn = true;
                nextScene.dispatchEvent(new enchant.Event(Event_SceneExStarting));

                enchant.Game.instance.replaceScene(nextScene);
            }
            return true;
        }
        if (0 < this.waitCount) {
            this.waitCount--;
            return true;
        }
        return false;
    },
    moveSceneTo: function (sceneName) {
        this.nextSceneName = sceneName;
        this.setFadeOut = true;
    }
});
