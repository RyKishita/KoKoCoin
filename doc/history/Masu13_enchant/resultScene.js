function CreateResultScene(gameData) {
    var game = enchant.Game.instance;

    var scene = new SceneEx(gameData);
    var group = new Group();

    var backgroundSprite = new Sprite(backgroundWidth, backgroundWidth);
    backgroundSprite.image = enchant.Game.instance.assets[resultBackAssetName];
    group.addChild(backgroundSprite);

    var selectArrowIcon = CreateSelectSprite();
    group.addChild(selectArrowIcon);

    var winSprite = new Sprite(resultWidth, resultHeight);
    winSprite.image = enchant.Game.instance.assets[resultWinAssetName];
    group.addChild(winSprite);

    var loseSprite = new Sprite(resultWidth, resultHeight);
    loseSprite.image = enchant.Game.instance.assets[resultLoseAssetName];
    group.addChild(loseSprite);

    var coinListSprite = GetCoinListSprite();
    coinListSprite.x = 0;
    coinListSprite.y = 180;
    group.addChild(coinListSprite);

    var explainSprite = GetExplainBoardSprite();
    explainSprite.x = game.width - explainBoardWidth;
    explainSprite.y = coinListSprite.y - explainBoardHeight - 5;
    group.addChild(explainSprite);

    var explainBoardTextLeft = explainSprite.x + 10;
    var explainBoardTextTop = explainSprite.y + 10;
    var explainBoardLineSpace = 15;
    var explainBoardLabelColor = 'white';

    var coinExplainLabelNo = new Label('');
    coinExplainLabelNo.color = explainBoardLabelColor;
    coinExplainLabelNo.x = explainBoardTextLeft;
    coinExplainLabelNo.y = explainBoardTextTop;

    var coinExplainLabel1 = new Label('');
    coinExplainLabel1.color = explainBoardLabelColor;
    coinExplainLabel1.x = explainBoardTextLeft;
    coinExplainLabel1.y = coinExplainLabelNo.y + explainBoardLineSpace;

    var coinExplainLabel2 = new Label('');
    coinExplainLabel2.color = explainBoardLabelColor;
    coinExplainLabel2.x = explainBoardTextLeft;
    coinExplainLabel2.y = coinExplainLabel1.y + explainBoardLineSpace;

    var coinExplainLabel3 = new Label('');
    coinExplainLabel3.color = explainBoardLabelColor;
    coinExplainLabel3.x = explainBoardTextLeft;
    coinExplainLabel3.y = coinExplainLabel2.y + explainBoardLineSpace;

    var coinExplainLabels = new Array();
    coinExplainLabels.push(coinExplainLabel1);
    coinExplainLabels.push(coinExplainLabel2);
    coinExplainLabels.push(coinExplainLabel3);

    group.addChild(coinExplainLabelNo);
    group.addChild(coinExplainLabel1);
    group.addChild(coinExplainLabel2);
    group.addChild(coinExplainLabel3);

    var getCoinTextSprite = GetResultGetCoinTextSprite();
    getCoinTextSprite.x = 0;
    getCoinTextSprite.y = explainSprite.y + (explainSprite.height - getCoinTextSprite.height) / 2;
    group.addChild(getCoinTextSprite);

    var noCoinTextSprite = GetResultNoCoinTextSprite();
    noCoinTextSprite.x = 0;
    noCoinTextSprite.y = coinListSprite.y + (coinListSprite.height - noCoinTextSprite.height) / 2;
    group.addChild(noCoinTextSprite);

    var nextText = new Text(72, 280, 'Save & Next');
    nextText.addEventListener(enchant.Event.TOUCH_START, function (e) {
        scene.moveSceneTo(sceneMainMenu);
    });
    group.addChild(nextText);

    scene.addChild(group);

    var getCoins = new Array();
    var coinSprites = new Array();
    var selectedIndex = 0;
    function SetSelectCoin() {
        coinExplainLabelNo.visible = 0 <= selectedIndex && 0 < getCoins.length;
        for (var i in coinExplainLabels) {
            coinExplainLabels[i].visible = coinExplainLabelNo.visible;
        }

        if (coinExplainLabelNo.visible) {
            var no = getCoins[selectedIndex];
            var coinData = gameData.coinDatas[no];
            coinExplainLabelNo.text = 'No' + (no + 1);
            coinData.setExplain(coinExplainLabels);

            var sprite = coinSprites[selectedIndex];
            selectArrowIcon.x = sprite.x - selectArrowIcon.width;
            selectArrowIcon.y = sprite.y + sprite.height / 2;
        } else {
            selectArrowIcon.x = nextText.x - selectArrowIcon.width;
            selectArrowIcon.y = nextText.y;
        }
    }

    scene.addEventListener(Event_SceneExStarting, function (e) {
        winSprite.visible = gameData.result;
        loseSprite.visible = !gameData.result;

        var getCoinNum = 1;
        if (gameData.result) {
            if (gameData.targetData instanceof CharactorInfoUser) {
                if (gameData.targetData.isRanker) {
                    if (gameData.score <= gameData.targetData.score) {
                        gameData.score = gameData.targetData.score + 1;
                        getCoinNum = 3;
                    }
                } else {
                    getCoinNum = 2;
                }
            }
        } else {
            if (gameData.targetData instanceof CharactorInfoPractice) {
                getCoinNum = 0;
            }
        }

        getCoins.length = 0;
        for (var i = 0; i < getCoinNum; i++) {
            var coins = new Array();
            for (var j in gameData.hasCoins) {
                if (gameData.hasCoins[j] < coinHasMax) {
                    coins.push(parseInt(j));
                }
            }
            if (0 == coins.length) break;
            var getNo = coins[Math.floor(Math.random() * coins.length)];
            getCoins.push(getNo);
            gameData.hasCoins[getNo]++; /*この時点で取得コイン追加済み。保存はまだ*/
        }

        for (var i in coinSprites) {
            group.removeChild(coinSprites[i]);
        }
        coinSprites.length = 0;

        var coinSpace = 20;
        var x = (game.width - getCoins.length * (coinSize3 + coinSpace)) / 2;
        for (var i in getCoins) {
            var coinData = gameData.coinDatas[getCoins[i]];
            var sprite = coinData.toSprite();
            sprite.x = x + (coinSize3 - sprite.width) / 2;
            sprite.y = coinListSprite.y + 10;
            sprite.addEventListener(enchant.Event.TOUCH_START, function (e) {
                PlaySound(soundSelect);
                selectedIndex = coinSprites.indexOf(this);
                SetSelectCoin();
            });
            group.addChild(sprite);
            coinSprites.push(sprite);

            x += coinSize3 + coinSpace;
        }

        noCoinTextSprite.visible = (0 == getCoins.length);

        selectedIndex = (0 == getCoins.length) ? -1 : 0;
        SetSelectCoin();

        scene.opacity = 0;
        PlaySound(soundResult);
    });
    scene.addEventListener(enchant.Event.ENTER_FRAME, function (e) {
        if (scene.commonProcess()) return;

        if (game.input.up) {
            PlaySound(soundSelect);
            if (selectedIndex < 0) {
                selectedIndex = 0;
                SetSelectCoin();
            }
            scene.inputWait();
        } else if (game.input.down) {
            PlaySound(soundSelect);
            selectedIndex = -1;
            SetSelectCoin();
            scene.inputWait();
        } else if (game.input.left) {
            PlaySound(soundSelect);
            selectedIndex--;
            if (selectedIndex < 0) {
                selectedIndex = getCoins.length - 1;
            }
            SetSelectCoin();
            scene.inputWait();
        } else if (game.input.right) {
            PlaySound(soundSelect);
            selectedIndex++;
            if (getCoins.length <= selectedIndex) {
                selectedIndex = 0;
            }
            SetSelectCoin();
            scene.inputWait();
        } else if (game.input.a) {
            if (selectedIndex < 0) {
                PlaySound(soundOK);
                /*獲得コインはシーン開始時にgameData.hasConsに追加済み*/
                gameData.saveUserData();
                scene.moveSceneTo(sceneMainMenu);
            }
            scene.inputWait();
        } else if (game.input.b) {
            PlaySound(soundCancel);
            selectedIndex = -1;
            SetSelectCoin();
            scene.inputWait();
        }
    });

    gameData.scenes[sceneResult] = scene;
    return scene;
}
