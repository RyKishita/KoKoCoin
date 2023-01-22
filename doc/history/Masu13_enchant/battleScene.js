function CreateBattleScene(gameData) {
    var game = enchant.Game.instance;

    var scene = new SceneEx(gameData);

    var soundBGM = enchant.Game.instance.assets[soundBattleBGM];

    /*--------------------------------*/
    var backGroup = new Group();

    var backgroundSprite = new Sprite(battleSceneWidth, battleSceneHeight);
    backgroundSprite.image = enchant.Game.instance.assets[battleBackAssetName];
    backgroundSprite.touchEnabled = false;
    backGroup.addChild(backgroundSprite);

    var setSprites = new Array();
    for (var i = 1; i <= 11; i++) {
        var b = new BattleSetSprite('B', i);
        b.y = battleSetTop;
        setSprites['B' + i] = b;
        backGroup.addChild(b);

        var r = new BattleSetSprite('R', i);
        r.y = battleSetTop;
        setSprites['R' + i] = r;
        backGroup.addChild(r);
    }

    scene.addChild(backGroup);
    /*--------------------------------*/
    var moveObjectGroup = new Group();

    var player = new CharacterSprite();
    moveObjectGroup.addChild(player);

    var enemy = new CharacterSprite();
    moveObjectGroup.addChild(enemy);

    var playerMoveDirectLeftArrowIcon = CreateArrowSprite();
    playerMoveDirectLeftArrowIcon.rotate(270);
    playerMoveDirectLeftArrowIcon.y = characterPosY + characterSize/2;
    moveObjectGroup.addChild(playerMoveDirectLeftArrowIcon);

    var playerMoveDirectRightArrowIcon = CreateArrowSprite();
    playerMoveDirectRightArrowIcon.rotate(90);
    playerMoveDirectRightArrowIcon.y = characterPosY + characterSize/2;
    moveObjectGroup.addChild(playerMoveDirectRightArrowIcon);

    var coinShootSprite = new CoinProjectileSprite(0);
    moveObjectGroup.addChild(coinShootSprite);

    var coinReflectSprite = new CoinProjectileSprite(1);
    moveObjectGroup.addChild(coinReflectSprite);

    var coinShieldingSprite = new CoinShieldingSprite();
    moveObjectGroup.addChild(coinShieldingSprite);

    var setEffectSprite = new SetEffectSprite();
    setEffectSprite.visible = false;
    moveObjectGroup.addChild(setEffectSprite);

    scene.addChild(moveObjectGroup);
    /*--------------------------------*/
    var statusGroup = new Group();

    var statusMargin = 10;

    var playerLifeSurface = new Surface(battleLifeWidth, battleLifeHeight);
    playerLifeSurface.context.fillStyle = 'blue';
    playerLifeSurface.context.fillRect(0, 0, battleLifeWidth, battleLifeHeight);

    var playerLifeSprite = new Sprite(battleLifeWidth, battleLifeHeight);
    playerLifeSprite.image = playerLifeSurface;
    playerLifeSprite.x = statusMargin;
    playerLifeSprite.y = statusMargin;
    playerLifeSprite.backgroundColor = 'black';
    statusGroup.addChild(playerLifeSprite);

    var playerNameLabel = new Label('');
    playerNameLabel.color = 'white';
    playerNameLabel.x = statusMargin;
    playerNameLabel.y = statusMargin;
    statusGroup.addChild(playerNameLabel);

    var turnSprite = GetBattleTurnSprite();
    turnSprite.x = 120;
    statusGroup.addChild(turnSprite);

    var enemyLifeSurface = new Surface(battleLifeWidth, battleLifeHeight);
    enemyLifeSurface.context.fillStyle = 'red';
    enemyLifeSurface.context.fillRect(0, 0, battleLifeWidth, battleLifeHeight);

    var enemyLifeSprite = new Sprite(battleLifeWidth, battleLifeHeight);
    enemyLifeSprite.image = enemyLifeSurface;
    enemyLifeSprite.x = statusMargin + battleLifeWidth + statusMargin + battleTurnSize + statusMargin;
    enemyLifeSprite.y = statusMargin;
    enemyLifeSprite.backgroundColor = 'black';
    statusGroup.addChild(enemyLifeSprite);

    var enemyNameLabel = new Label('');
    enemyNameLabel.color = 'white';
    enemyNameLabel.x = statusMargin + battleLifeWidth + statusMargin + battleTurnSize + statusMargin;
    enemyNameLabel.y = statusMargin;
    statusGroup.addChild(enemyNameLabel);

    var progressTextSprite = new BattleProgressSprite();
    progressTextSprite.x = (game.width - progressTextSprite.width) / 2;
    progressTextSprite.y = turnSprite.y + turnSprite.height;
    progressTextSprite.visible = false;
    statusGroup.addChild(progressTextSprite);

    scene.addChild(statusGroup);
    /*--------------------------------*/
    var coinListGroup = new Group();

    var coinListSprite = GetCoinListSprite();
    coinListSprite.x = (game.width - coinListWidth) / 2;
    coinListSprite.y = game.height - coinListHeight - 24;
    coinListGroup.addChild(coinListSprite);

    var selectArrowIcon = CreateSelectSprite();
    coinListGroup.addChild(selectArrowIcon);

    /*scene.addChild(coinListGroup);*/
    /*--------------------------------*/
    var coinExlainGroup = new Group();

    var explainSprite = GetExplainBoardSprite();
    explainSprite.x = game.width - explainBoardWidth;
    explainSprite.y = coinListSprite.y - explainBoardHeight - 5;
    coinExlainGroup.addChild(explainSprite);

    var explainBoardTextLeft = explainSprite.x + 10;
    var explainBoardTextTop = explainSprite.y + 10;
    var explainBoardLineSpace = 15;
    var explainBoardLabelColor = 'white';

    var coinExplainLabelNo = new Label('');
    coinExplainLabelNo.color = explainBoardLabelColor;
    coinExplainLabelNo.x = explainBoardTextLeft;
    coinExplainLabelNo.y = explainBoardTextTop;

    var coinExplainLabelUseOK = new Label('');
    coinExplainLabelUseOK.x = explainBoardTextLeft + 40;
    coinExplainLabelUseOK.y = explainBoardTextTop;

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

    coinExlainGroup.addChild(coinExplainLabelNo);
    coinExlainGroup.addChild(coinExplainLabelUseOK);
    coinExlainGroup.addChild(coinExplainLabel1);
    coinExlainGroup.addChild(coinExplainLabel2);
    coinExlainGroup.addChild(coinExplainLabel3);

    coinExlainGroup.visible = false;
    //scene.addChild(coinExlainGroup);
    /*--------------------------------*/
    var topGroup = new Group();

    var firstDicePosX = 104;
    var moveDicePosX = 144;

    var playerDiceSprite = CreateDiceSprite();
    playerDiceSprite.x = firstDicePosX;
    playerDiceSprite.y = 144;
    topGroup.addChild(playerDiceSprite);

    var enemyDiceSprite = CreateDiceSprite();
    enemyDiceSprite.x = 204;
    enemyDiceSprite.y = 144;
    topGroup.addChild(enemyDiceSprite);

    var showUpSprite = GetBattleShowUpSprite();
    showUpSprite.x = (game.width - showUpSprite.width) / 2;
    showUpSprite.y = (game.height - showUpSprite.height) / 2;
    topGroup.addChild(showUpSprite);

    var skipText = new Text(128, 300, 'Skip');
    topGroup.addChild(skipText);

    scene.addChild(topGroup);
    /*--------------------------------*/

    var progress = -10;
    var playerIcon = null;
    var enemyIcon = null;
    var playerLife = 1000;
    var enemyLife = 1000;
    var playerHasCoins = new Array();
    var enemyHasCoins = new Array();
    var playerStackCoins = new Array();
    var enemyStackCoins = new Array();
    var playerTrashCoins = new Array();
    var enemyTrashCoins = new Array();
    var usingEnemyCoinIndex = -1;

    var vScroll = 0;
    var scrollRightLimit = battleSceneWidth - game.width;
    var shiftScrollX = 0;
    var targetScrollX = 0;
    var scrollModify = false;

    function ScrollObjects() {
        if (!scrollModify) return;
        scrollModify = false;

        backGroup.x = -vScroll;
        moveObjectGroup.x = -vScroll;
    }

    function ShiftScrollPos(shift) {
        if (0 == shift) return false;
        if (shift < 0 && vScroll < Math.abs(shift)) {
            if (vScroll == 0) return false;
            vScroll = 0;
        } else if (0 < shift && scrollRightLimit < vScroll + shift) {
            if (vScroll == scrollRightLimit) return false;
            vScroll = scrollRightLimit;
        } else {
            vScroll += shift;
        }

        scrollModify = true;
        return true;
    }

    function UpdateScroll() {
        if (0 == shiftScrollX) return false;
        if (ShiftScrollPos(shiftScrollX)) {
            if (shiftScrollX < 0) {
                if (targetScrollX < vScroll) return true;
            } else {
                if (vScroll < targetScrollX) return true;
            }
        }
        shiftScrollX = 0;
        return true;
    }

    function SetLife() {
        var width = Math.floor(battleLifeWidth * playerLife / 1000.0);
        playerLifeSurface.context.clearRect(0, 0, battleLifeWidth, battleLifeHeight);
        playerLifeSurface.context.fillRect(battleLifeWidth - width, 0, width, battleLifeHeight);

        width = Math.floor(battleLifeWidth * enemyLife / 1000.0);
        enemyLifeSurface.context.clearRect(0, 0, battleLifeWidth, battleLifeHeight);
        enemyLifeSurface.context.fillRect(0, 0, width, battleLifeHeight);
    }

    var touchCoin = 0;
    var nextProgressFromTouchCoin = 0;
    function TouchedCoin() {
        if (progress != 2 && !canUseCoin) return;
        touchCoin = playerHasCoins[selectedCoinIndex];
        progress = nextProgressFromTouchCoin;
        HideCoinList();
    }

    var coinSprites = new Array();
    var canSelectCoin = false;
    function SetupCoinList(showCoinType) {
        selectedCoinIndex = -1;
        canSelectCoin = false;

        for (var i in coinSprites) {
            coinListGroup.removeChild(coinSprites[i]);
        }
        coinSprites.length = 0;

        var x = coinListSprite.x + 10;
        var y = coinListSprite.y + 10;
        for (var i in playerHasCoins) {
            var coinNo = playerHasCoins[i];
            var coinData = gameData.coinDatas[coinNo];
            var show = (showCoinType < 0 ||
			0 == showCoinType && (coinData instanceof CoinInfoSet) ||
			1 == showCoinType && (coinData instanceof CoinInfoShot) ||
			2 == showCoinType && (coinData instanceof CoinInfoShield));

            var sprite = coinData.toSprite();
            sprite.x = x;
            sprite.y = y;
            if (show) {
                canSelectCoin = true;
                sprite.coinNo = coinNo;
                sprite.touchEnabled = true;
                sprite.addEventListener(enchant.Event.TOUCH_START, function (e) {
                    var newSelectedCoinIndex = coinSprites.indexOf(this);
                    if (selectedCoinIndex == newSelectedCoinIndex) {
                        PlaySound(soundOK);
                        TouchedCoin();
                    } else {
                        PlaySound(soundSelect);
                        selectedCoinIndex = newSelectedCoinIndex;
                        SetSelectCoin();
                    }
                });
                if (selectedCoinIndex < 0) {
                    selectedCoinIndex = parseInt(i);
                }
            } else {
                sprite.touchEnabled = false;
                sprite.opacity = 0.1;
            }
            coinListGroup.insertBefore(sprite, selectArrowIcon);
            coinSprites.push(sprite);

            x += sprite.width;
        }
    }

    function HideCoinExplain() {
        if (coinExlainGroup.visible) {
            coinExlainGroup.visible = false;
            scene.removeChild(coinExlainGroup);
        }
    }

    function HideCoinList() {
        skipText.visible = false;
        HideCoinExplain();
        scene.removeChild(coinListGroup);
    }

    var selectedCoinIndex = -1;
    var canUseCoin = false;
    function SetSelectCoin() {
        if (selectedCoinIndex < 0) {
            MoveMenuSelectArrow(selectArrowIcon, skipText);
        } else {
            var coinSprite = coinSprites[selectedCoinIndex];
            selectArrowIcon.x = coinSprite.x + coinSprite.width / 2 - selectArrowIcon.width;
            selectArrowIcon.y = coinSprite.y + coinSprite.height / 2;
        }
        if (selectedCoinIndex < 0) {
            canUseCoin = false;
            HideCoinExplain();
        } else {
            var no = playerHasCoins[selectedCoinIndex];
            var coinData = gameData.coinDatas[no];

            var msg = '';
            if (coinData instanceof CoinInfoSet) {
                var existSet = 0 != player.areaNo && 12 != player.areaNo && setSprites['B' + player.areaNo].visible;
                canUseCoin = !existSet && coinData.canSet(player.areaNo);
                if (canUseCoin) {
                    msg = '使用可能';
                } else {
                    if (existSet) {
                        msg = '配置済み';
                    } else if (player.areaNo == 0 || player.areaNo == 12) {
                        msg = 'Safeには配置できない';
                    } else {
                        msg = '配置不可';
                    }
                }
            } else if (coinData instanceof CoinInfoShot) {
                canUseCoin = coinData.canShot(player.areaNo, enemy.areaNo);
                if (canUseCoin) {
                    msg = '使用可能';
                } else {
                    if (enemy.areaNo == 0 || enemy.areaNo == 12) {
                        msg = 'Safeには攻撃できない';
                    } else {
                        msg = '射程外';
                    }
                }
            } else if (coinData instanceof CoinInfoShield) {
                canUseCoin = true;
                msg = '使用可能';
            } else {
                canUseCoin = false;
                msg = 'error';
            }
            coinExplainLabelUseOK.text = msg;
            coinExplainLabelUseOK.color = canUseCoin ? 'white' : 'red';

            coinExplainLabelNo.text = 'No' + (no + 1);
            coinData.setExplain(coinExplainLabels);
            if (!coinExlainGroup.visible) {
                coinExlainGroup.visible = true;
                scene.addChild(coinExlainGroup);
            }
        }
    }
    function ShiftSelectCoin(direct) {
        if (canSelectCoin) {
            do {
                if (direct) {
                    selectedCoinIndex++;
                    if (playerHasCoins.length <= selectedCoinIndex) {
                        selectedCoinIndex = 0;
                    }
                } else {
                    selectedCoinIndex--;
                    if (selectedCoinIndex < 0) {
                        selectedCoinIndex = playerHasCoins.length - 1;
                    }
                }
            } while (0 <= selectedCoinIndex && selectedCoinIndex < coinSprites.length && !coinSprites[selectedCoinIndex].touchEnabled);
        }
        SetSelectCoin();
    }

    var backTouchX = -1;
    backgroundSprite.addEventListener(enchant.Event.TOUCH_START, function (e) {
        backTouchX = e.x;
    });

    backgroundSprite.addEventListener(enchant.Event.TOUCH_MOVE, function (e) {
        if (backTouchX != e.x) {
            ShiftScrollPos(backTouchX - e.x);
            backTouchX = e.x;
        }
    });

    skipText.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundCancel);
        switch (progress) {
            case 11:
                HideCoinList();
                progress = 20;
                break;
            case 31:
                HideCoinList();
                progress = 40;
                break;
            case 111:
                HideCoinList();
                progress = 112;
                break;
        }
    });

    function StartMoveScroll(moveAreaNo) {
        targetScrollX = GetScrollPos(moveAreaNo);
        shiftScrollX = (targetScrollX - vScroll) / 20;
        if (shiftScrollX < 0) {
            if (-6 < shiftScrollX) {
                shiftScrollX = -6;
            }
        } else if (0 < shiftScrollX) {
            if (shiftScrollX < 6) {
                shiftScrollX = 6;
            }
        }
    }

    function StopFirstChoiceDice() {
        if (playerDiceSprite.frame == enemyDiceSprite.frame) return;
        player.moveStart(0);
        enemy.moveStart(12);
        StartMoveScroll(playerDiceSprite.frame < enemyDiceSprite.frame ? 12 : 0);
        progress = -3;
    }

    playerDiceSprite.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundOK);
        switch (progress) {
            case -10: StopFirstChoiceDice(); break;
            case 21: progress = 22; break;
        }
    });

    enemyDiceSprite.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundOK);
        StopFirstChoiceDice();
    });

    function SetFirstHasCoin(stackCoins, hasCoins) {
        hasCoins.length = 0;
        var totalSize = 0;
        while (0 < stackCoins.length) {
            var coinNo = stackCoins[0];
            var size = gameData.coinDatas[coinNo].size;
            if (hasCoinSizeMax < (totalSize + size)) {
                break;
            }
            hasCoins.push(coinNo);
            totalSize += size;
            stackCoins.splice(0, 1);
        }
    }

    function CalcTotalCoinSize(coins){
        var totalSize = 0;
        for(var i in coins){
            totalSize += gameData.coinDatas[coins[i]].size;
        }
        return totalSize;
    }

    function GetEnemyUseCoinIndex(useType) {
        var valueMaxIndex = -1;
        var valueMax = -1;
        for (var i in enemyHasCoins) {
            var hNo = enemyHasCoins[i];
            var coinData = gameData.coinDatas[hNo];
            switch (useType) {
                case 0:
                    if (!(coinData instanceof CoinInfoSet)) break;
                    if (!coinData.canSet(enemy.areaNo)) break;
                    if (setSprites['R' + enemy.areaNo].visible) break;
                    if (coinData.damage <= valueMax) break;
                    valueMax = coinData.damage;
                    valueMaxIndex = parseInt(i);
                    break;
                case 1:
                    if (!(coinData instanceof CoinInfoShot)) break;
                    if (!coinData.canShot(enemy.areaNo, player.areaNo)) break;
                    if (coinData.damage <= valueMax) break;
                    valueMax = coinData.damage;
                    valueMaxIndex = parseInt(i);
                    break;
                case 2:
                    if (!(coinData instanceof CoinInfoShield)) break;
                    if (coinData.cutDamage <= valueMax) break;
                    valueMax = coinData.cutDamage;
                    valueMaxIndex = parseInt(i);
                    break;
            }
        }
        return valueMaxIndex;
    }

    var playerSelectedMoveDirect = -1;
    var canSelectLeft = false;
    var canSelectRight = false;
    function ChoiceMoveDirectArrow() {
        var v = (0 == playerSelectedMoveDirect) ? 2 : 1;
        playerMoveDirectLeftArrowIcon.scaleX = v;
        playerMoveDirectLeftArrowIcon.scaleY = v;

        v = (1 == playerSelectedMoveDirect) ? 2 : 1;
        playerMoveDirectRightArrowIcon.scaleX = v;
        playerMoveDirectRightArrowIcon.scaleY = v;
    }

    playerMoveDirectLeftArrowIcon.addEventListener(enchant.Event.TOUCH_START, function (e) {
        if (!canSelectLeft) return;
        PlaySound(soundOK);
        playerSelectedMoveDirect = 0;
        progress = 24;
    });
    playerMoveDirectRightArrowIcon.addEventListener(enchant.Event.TOUCH_START, function (e) {
        if (!canSelectRight) return;
        PlaySound(soundOK);
        playerSelectedMoveDirect = 1;
        progress = 24;
    });

    scene.addEventListener(Event_SceneExStarting, function (e) {
        if (null != playerIcon) {
            statusGroup.removeChild(playerIcon);
        }
        playerIcon = gameData.toSprite();
        playerIcon.x = statusMargin;
        playerIcon.y = statusMargin + battleLifeHeight + statusMargin;
        statusGroup.addChild(playerIcon);
        playerNameLabel.text = gameData.toString();
        player.image = gameData.toPlayerSurface();
        player.direction = characterDirectDown;

        if (null != enemyIcon) {
            statusGroup.removeChild(enemyIcon);
        }
        enemyIcon = gameData.targetData.toSprite();
        enemyIcon.x = game.width - battleIconSize - statusMargin;
        enemyIcon.y = statusMargin + battleLifeHeight + statusMargin;
        statusGroup.addChild(enemyIcon);
        enemyNameLabel.text = gameData.targetData.toString();
        enemy.image = gameData.targetData.toPlayerSurface();
        enemy.direction = characterDirectDown;

        progress = -10;
        turnSprite.frame = 0;

        vScroll = GetScrollPos(6);
        shiftScrollX = 0;
        player.setPos(5);
        enemy.setPos(7);
        player.turnAround(enemy.areaNo);
        enemy.turnAround(player.areaNo);
        scrollModify = true;
        ScrollObjects();

        playerLife = 1000;
        enemyLife = 1000;
        SetLife();

        playerStackCoins = gameData.makeStackCoin();
        enemyStackCoins = gameData.targetData.makeStackCoin();
        SetFirstHasCoin(playerStackCoins, playerHasCoins);
        SetFirstHasCoin(enemyStackCoins, enemyHasCoins);
        playerTrashCoins.length = 0;
        enemyTrashCoins.length = 0;

        playerDiceSprite.x = firstDicePosX;
        playerDiceSprite.visible = true;
        playerDiceSprite.frame = 0;
        enemyDiceSprite.visible = true;
        enemyDiceSprite.frame = 0;
        skipText.visible = false;
        showUpSprite.visible = false;
        showUpSprite.frame = 0;
        playerMoveDirectLeftArrowIcon.visible = false;
        playerMoveDirectLeftArrowIcon.scaleX = 1;
        playerMoveDirectLeftArrowIcon.scaleY = 1;
        canSelectLeft = false;
        playerMoveDirectRightArrowIcon.visible = false;
        playerMoveDirectRightArrowIcon.scaleX = 1;
        playerMoveDirectRightArrowIcon.scaleY = 1;
        canSelectRight = false;
        for (var i in setSprites) {
            setSprites[i].visible = false;
            setSprites[i].coinNo = -1;
        }
        progressTextSprite.visible = false;

        scene.opacity = 0;

        soundBGM.volume = 1;
        soundBGM.play();
    });

    var pCoinNo = -1;
    var eCoinNo = -1;
    var pCoin = null;
    var eCoin = null;
    var checkAreaName = null;
    var calcDamage = 0;

    scene.addEventListener(enchant.Event.ENTER_FRAME, function (e) {
        if (scene.commonProcess()) return;

        player.updateMove();
        enemy.updateMove();
        if (UpdateScroll()) scrollModify = true;
        ScrollObjects();

        if (soundBGM.duration - soundBGM.currentTime <= 5) {
            soundBGM.volume = (soundBGM.duration - soundBGM.currentTime) / 5.0;
            if (soundBGM.volume < 0.01) {
                soundBGM.currentTime = 0;
                soundBGM.volume = 1;
            }
        }

        switch (progress) {
            case -10: /*先攻後攻*/
                playerDiceSprite.setDice();
                enemyDiceSprite.setDice();
                if (game.input.a) {
                    PlaySound(soundOK);
                    StopFirstChoiceDice();
                    scene.inputWait(20);
                }
                break;
            case -3: /*キャラクタの移動と先攻キャラにスクロール*/
                if (player.isMoving || enemy.isMoving) break;

                playerDiceSprite.visible = false;
                enemyDiceSprite.visible = false;
                showUpSprite.visible = true;

                player.direction = characterDirectRight;
                player.walk = 1;
                player.updateFrame();
                enemy.direction = characterDirectLeft;
                enemy.walk = 1;
                enemy.updateFrame();

                scene.inputWait(20);
                progress = -2;
                break;
            case -2: /*showup0*/
                scene.inputWait(20);
                showUpSprite.frame = 1;
                progress = -1;
                break;
            case -1: /*showup1*/
                showUpSprite.visible = false;
                progressTextSprite.visible = true;
                progress = playerDiceSprite.frame < enemyDiceSprite.frame ? 110 : 10;
                break;
            case 0: /*コイン取得(自分)*/
                progressTextSprite.frame = 0;
                /*コインスタックが無かったら、ごみ箱から戻す*/
                if (0 == playerStackCoins.length) {
                    playerStackCoins = ShuffleCoins(playerTrashCoins);
                }
                if (0 < playerStackCoins.length) {
                    progressTextSprite.frame = 2;
                    pCoinNo = playerStackCoins.shift();
                    playerHasCoins.push(pCoinNo);
                    var pTotalCoinSize = CalcTotalCoinSize(playerHasCoins);
                    if (hasCoinSizeMax < pTotalCoinSize) {
                        progress = 1;
                    } else {
                        progress = 10;
                    }
                } else {
                    progress = 10;
                }
                break;
            case 1: /*取捨選択準備*/
                progressTextSprite.frame = 1;
                nextProgressFromTouchCoin = 3;
                backgroundSprite.touchEnabled = true;
                skipText.visible = false;
                SetupCoinList(-1);
                SetSelectCoin();
                scene.addChild(coinListGroup);
                progress = 2;
                break;
            case 2: /*取捨選択(自分)*/
                if (game.input.left) {
                    PlaySound(soundSelect);
                    ShiftSelectCoin(false);
                    scene.inputWait();
                } else if (game.input.right) {
                    PlaySound(soundSelect);
                    ShiftSelectCoin(true);
                    scene.inputWait();
                } else if (game.input.a) {
                    PlaySound(soundOK);
                    TouchedCoin();
                    scene.inputWait();
                }
                break;
            case 3:
                playerHasCoins.splice(selectedCoinIndex, 1);
                if (hasCoinSizeMax < CalcTotalCoinSize(playerHasCoins)) {
                    progress = 1;
                } else {
                    progress = 10;
                }
                break;
            case 10: /*射撃コイン準備(自分)*/
                progressTextSprite.frame = 6;
                SetupCoinList(1);
                SetSelectCoin();
                progress = 11;
                nextProgressFromTouchCoin = 12;
                backgroundSprite.touchEnabled = true;
                skipText.visible = true;
                scene.addChild(coinListGroup);
                break;
            case 11: /*射撃コイン選択中(自分)*/
                if (game.input.up) {
                    if (selectedCoinIndex < 0 && canSelectCoin) {
                        PlaySound(soundSelect);
                        selectedCoinIndex = 0;
                        ShiftSelectCoin(true);
                    }
                    scene.inputWait();
                } else if (game.input.down) {
                    PlaySound(soundSelect);
                    selectedCoinIndex = -1;
                    SetSelectCoin();
                    scene.inputWait();
                } else if (game.input.left) {
                    PlaySound(soundSelect);
                    ShiftSelectCoin(false);
                    scene.inputWait();
                } else if (game.input.right) {
                    PlaySound(soundSelect);
                    ShiftSelectCoin(true);
                    scene.inputWait();
                } else if (game.input.a) {
                    PlaySound(soundOK);
                    if (selectedCoinIndex < 0) {
                        progress = 20;
                        HideCoinList();
                    } else {
                        TouchedCoin();
                    }
                    scene.inputWait();
                } else if (game.input.b) {
                    PlaySound(soundCancel);
                    selectedCoinIndex = -1;
                    SetSelectCoin();
                    scene.inputWait();
                }
                break;
            case 12: /*射撃コイン実行*/
                progressTextSprite.frame = 8;
                coinShootSprite.moveStart(player, enemy);
                StartMoveScroll(enemy.areaNo);
                usingEnemyCoinIndex = GetEnemyUseCoinIndex(2);
                if (0 <= usingEnemyCoinIndex) {
                    coinShieldingSprite.show(enemy.x, player.x);
                }
                progress = 13;
                break;
            case 13: /*射撃コイン飛行中*/
                coinShootSprite.updateMove();
                if (coinShootSprite.isMoving) break;
                coinShieldingSprite.visible = false;

                if (0 <= usingEnemyCoinIndex) {
                    pCoin = gameData.coinDatas[playerHasCoins[selectedCoinIndex]];
                    eCoin = gameData.coinDatas[enemyHasCoins[usingEnemyCoinIndex]];

                    if (pCoin.damage <= eCoin.cutDamage && 0 != eCoin.counterDamage) {
                        PlaySound(soundReflect);
                        coinReflectSprite.moveStart(enemy, player);
                        StartMoveScroll(player.areaNo);
                        progress = 14;
                    } else {
                        progress = 18;
                    }
                } else {
                    progress = 18;
                }
                break;
            case 14: /*射撃コイン反射*/
                coinReflectSprite.updateMove();
                if (coinReflectSprite.isMoving) break;
                progress = 18;
                break;
            case 18: /*射撃コイン終了*/
                pCoinNo = playerHasCoins.splice(selectedCoinIndex, 1)[0];
                pCoin = gameData.coinDatas[pCoinNo];

                if (usingEnemyCoinIndex < 0) {
                    PlaySound(soundHit);
                    enemyLife -= pCoin.damage;
                } else {
                    eCoinNo = enemyHasCoins.splice(usingEnemyCoinIndex, 1)[0];
                    eCoin = gameData.coinDatas[eCoinNo];

                    if (eCoin.cutDamage < pCoin.damage) {
                        calcDamage = pCoin.damage - eCoin.cutDamage;
                        enemyLife -= calcDamage;
                    } else {
                        calcDamage = Math.min(pCoin.damage, eCoin.counterDamage);
                        playerLife -= calcDamage;
                    }
                    if (0 < calcDamage) {
                        PlaySound(soundHit);
                    } else {
                        PlaySound(soundReflect);
                    }

                    enemyTrashCoins.push(eCoinNo);
                }

                playerTrashCoins.push(pCoinNo);
                SetLife()
                progress = 20;
                break;
            case 20: /*ダイス準備*/
                progressTextSprite.frame = 3;
                playerDiceSprite.x = moveDicePosX;
                playerDiceSprite.visible = true;
                backgroundSprite.touchEnabled = true;
                StartMoveScroll(player.areaNo);
                progress = 21;
                break;
            case 21: /*ダイス*/
                playerDiceSprite.setDice();
                if (game.input.a) {
                    PlaySound(soundOK);
                    progress = 22;
                }
                break;
            case 22: /*移動先選択準備*/
                if (0 != shiftScrollX) break;
                progressTextSprite.frame = 4;

                canSelectLeft = (0 <= player.areaNo - playerDiceSprite.frame);
                playerMoveDirectLeftArrowIcon.x = player.x - playerMoveDirectLeftArrowIcon.width * 1.5;
                playerMoveDirectLeftArrowIcon.opacity = canSelectLeft ? 1 : 0.1;
                playerMoveDirectLeftArrowIcon.visible = true;

                canSelectRight = (player.areaNo + playerDiceSprite.frame <= 12);
                playerMoveDirectRightArrowIcon.x = player.x + player.width + playerMoveDirectRightArrowIcon.width / 2;
                playerMoveDirectRightArrowIcon.opacity = canSelectRight ? 1 : 0.1;
                playerMoveDirectRightArrowIcon.visible = true;

                playerSelectedMoveDirect = canSelectLeft == canSelectRight ? -1 : canSelectLeft < canSelectRight ? 1 : 0;
                ChoiceMoveDirectArrow();
                backgroundSprite.touchEnabled = true;
                progress = 23;
                break;
            case 23: /*移動先選択*/
                if (game.input.left) {
                    PlaySound(soundSelect);
                    ShiftScrollPos(-20);
                    if (canSelectLeft) {
                        playerSelectedMoveDirect = 0;
                        ChoiceMoveDirectArrow();
                    }
                } else if (game.input.right) {
                    PlaySound(soundSelect);
                    ShiftScrollPos(20);
                    if (canSelectRight) {
                        playerSelectedMoveDirect = 1;
                        ChoiceMoveDirectArrow();
                    }
                } else if (game.input.a) {
                    if (playerSelectedMoveDirect != -1) {
                        PlaySound(soundOK);
                        progress = 24;
                    }
                }
                break;
            case 24: /*移動*/
                progressTextSprite.frame = 5;
                playerDiceSprite.visible = false;
                playerMoveDirectLeftArrowIcon.visible = false;
                playerMoveDirectRightArrowIcon.visible = false;
                backgroundSprite.touchEnabled = false;

                var pNextArea = player.areaNo;
                switch (playerSelectedMoveDirect) {
                    case 0: pNextArea -= playerDiceSprite.frame; break;
                    case 1: pNextArea += playerDiceSprite.frame; break;
                }
                player.moveStart(pNextArea);
                StartMoveScroll(pNextArea);
                progress = 25;
                break;
            case 25: /*移動終了*/
                if (player.isMoving) break;
                if (0 != shiftScrollX) break;

                if (0 != player.areaNo && 12 != player.areaNo) {
                    checkAreaName = 'R' + player.areaNo;
                    if (setSprites[checkAreaName].visible) {
                        PlaySound(soundBomb);
                        setEffectSprite.show(player.areaNo);
                        progress = 26;
                    } else {
                        progress = 28;
                    }
                } else {
                    progress = 28;
                }

                break;
            case 26: /*移動終了*/
                setEffectSprite.updateFrame();
                scene.inputWait();
                if (setEffectSprite.visible) break;
                progress = 28;
                break;
            case 28: /*移動終了*/
                player.turnAround(enemy.areaNo);
                enemy.turnAround(player.areaNo);

                if (0 != player.areaNo && 12 != player.areaNo) {
                    checkAreaName = 'R' + player.areaNo;
                    if (setSprites[checkAreaName].visible) {
                        eCoinNo = setSprites[checkAreaName].coinNo;
                        playerLife -= gameData.coinDatas[eCoinNo].damage;
                        enemyTrashCoins.push(eCoinNo);
                        setSprites[checkAreaName].visible = false;
                        SetLife();
                    }
                }
                progress = 30;
                break;
            case 30: /*設置コイン準備*/
                progressTextSprite.frame = 9;
                SetupCoinList(0);
                SetSelectCoin();
                progress = 31;
                nextProgressFromTouchCoin = 32;
                backgroundSprite.touchEnabled = true;
                skipText.visible = true;
                scene.addChild(coinListGroup);
                break;
            case 31: /*設置コイン選択*/
                if (game.input.up) {
                    if (selectedCoinIndex < 0 && canSelectCoin) {
                        PlaySound(soundSelect);
                        selectedCoinIndex = 0;
                        ShiftSelectCoin(true);
                    }
                    scene.inputWait();
                } else if (game.input.down) {
                    PlaySound(soundSelect);
                    selectedCoinIndex = -1;
                    SetSelectCoin();
                    scene.inputWait();
                } else if (game.input.left) {
                    PlaySound(soundSelect);
                    ShiftSelectCoin(false);
                    scene.inputWait();
                } else if (game.input.right) {
                    PlaySound(soundSelect);
                    ShiftSelectCoin(true);
                    scene.inputWait();
                } else if (game.input.a) {
                    PlaySound(soundOK);
                    if (selectedCoinIndex < 0) {
                        progress = 40;
                        HideCoinList();
                    } else {
                        TouchedCoin();
                    }
                    scene.inputWait();
                } else if (game.input.b) {
                    PlaySound(soundCancel);
                    selectedCoinIndex = -1;
                    SetSelectCoin();
                    scene.inputWait();
                }
                break;
            case 32: /*コイン設置*/
                progressTextSprite.frame = 10;
                var pSprite = setSprites['B' + player.areaNo];
                pSprite.visible = true;
                pSprite.coinNo = playerHasCoins.splice(selectedCoinIndex, 1)[0];
                progress = 40;
                break;
            case 40: /*設置コイン操作後始末*/
                turnSprite.frame++;
                progress = 100;
                break;
            case 100: /*敵ターン開始→敵位置にスクロール*/
                progressTextSprite.frame = 11;
                StartMoveScroll(enemy.areaNo);
                progress = 101;
                break;
            case 101: /*スクロール終了→コイン取得*/
                if (0 != shiftScrollX) break;
                progressTextSprite.frame = 0;
                /*コインスタックが無かったら、ごみ箱から戻す*/
                if (0 == enemyStackCoins.length) {
                    enemyStackCoins = ShuffleCoins(enemyTrashCoins);
                }
                if (0 < enemyStackCoins.length) {
                    eCoinNo = enemyStackCoins.shift();
                    eCoin = gameData.coinDatas[eCoinNo];
                    var getCoin = true;

                    var currentTotalSize = CalcTotalCoinSize(enemyHasCoins);
                    if (hasCoinSizeMax < currentTotalSize + eCoin.size) {
                        getCoin = false;

                        var keepIndeies = new Array();
                        if (0 < gameData.targetData.keepShieldNum) {
                            /*保持する防御コイン設定の処理*/
                            for (var i in enemyHasCoins) {
                                if (gameData.coinDatas[enemyHasCoins[i]] instanceof CoinInfoShield) {
                                    keepIndeies.push(parseInt(i));
                                }
                            }
                            /*設定数より超えているか、手持ちが全て防御コインなら設定を無視*/
                            if (gameData.targetData.keepShieldNum < keepIndeies.length ||
                                keepIndeies.length == enemyHasCoins.length) {
                                keepIndeies.length = 0;
                            }
                        }

                        /*優先して保持するサイズ設定の処理*/
                        for (var i in enemyHasCoins) {
                            if (0 <= keepIndeies.indexOf(parseInt(i))) continue;
                            if (gameData.coinDatas[enemyHasCoins[i]].size == gameData.targetData.keepSize) {
                                keepIndeies.push(parseInt(i));
                            }
                        }

                        if (keepIndeies.length < enemyHasCoins.length) {
                            /*保持コインがちょうどサイズ7になるように捨てる*/
                            /*ちょうどがダメだったら大きめに捨てる*/
                            for (var j = 0; j < 2; j++) {
                                for (var i = 0; i < enemyHasCoins.length; i++) {
                                    if (0 <= keepIndeies.indexOf(i)) continue;
                                    var eCoin2 = gameData.coinDatas[enemyHasCoins[i]];
                                    var makedSpace = hasCoinSizeMax - currentTotalSize + eCoin2.size;
                                    if (j == 0 && makedSpace == eCoin.size ||
                                        j == 1 && makedSpace > eCoin.size) {
                                        enemyTrashCoins.push(enemyHasCoins.splice(i, 1)[0]);
                                        getCoin = true;
                                        break;
                                    }
                                }
                                if (getCoin) break;
                            }
                        }
                    }

                    if (getCoin) {
                        enemyHasCoins.push(eCoinNo);
                    }
                }
                progress = 110;
                break;
            case 110: /*敵射撃コイン選択→プレイヤー防御コイン選択準備*/
                progressTextSprite.frame = 6;
                usingEnemyCoinIndex = GetEnemyUseCoinIndex(1);
                if (usingEnemyCoinIndex < 0) {
                    progress = 120;
                } else {
                    progressTextSprite.frame = 7;
                    SetupCoinList(2);
                    SetSelectCoin();
                    progress = 111;
                    nextProgressFromTouchCoin = 112;
                    backgroundSprite.touchEnabled = true;
                    skipText.visible = true;
                    scene.addChild(coinListGroup);
                }
                break;
            case 111: /*防御コイン選択*/
                if (game.input.up) {
                    if (selectedCoinIndex < 0 && canSelectCoin) {
                        PlaySound(soundSelect);
                        selectedCoinIndex = 0;
                        ShiftSelectCoin(true);
                    }
                    scene.inputWait();
                } else if (game.input.down) {
                    PlaySound(soundSelect);
                    selectedCoinIndex = -1;
                    SetSelectCoin();
                    scene.inputWait();
                } else if (game.input.left) {
                    PlaySound(soundSelect);
                    ShiftSelectCoin(false);
                    scene.inputWait();
                } else if (game.input.right) {
                    PlaySound(soundSelect);
                    ShiftSelectCoin(true);
                    scene.inputWait();
                } else if (game.input.a) {
                    PlaySound(soundOK);
                    if (selectedCoinIndex < 0) {
                        progress = 112;
                        HideCoinList();
                    } else {
                        TouchedCoin();
                    }
                    scene.inputWait();
                } else if (game.input.b) {
                    PlaySound(soundCancel);
                    selectedCoinIndex = -1;
                    SetSelectCoin();
                    scene.inputWait();
                }
                break;
            case 112: /*敵射撃コイン*/
                progressTextSprite.frame = 8;
                coinShootSprite.moveStart(enemy, player);
                StartMoveScroll(player.areaNo);
                if (0 <= selectedCoinIndex) {
                    coinShieldingSprite.show(player.x, enemy.x);
                }
                progress = 113;
                break;
            case 113: /*敵射撃コインダメージ処理*/
                coinShootSprite.updateMove();
                if (coinShootSprite.isMoving) break;
                coinShieldingSprite.visible = false;

                if (0 <= selectedCoinIndex) {
                    pCoin = gameData.coinDatas[playerHasCoins[selectedCoinIndex]];
                    eCoin = gameData.coinDatas[enemyHasCoins[usingEnemyCoinIndex]];

                    if (eCoin.damage <= pCoin.cutDamage && 0 != pCoin.counterDamage) {
                        PlaySound(soundReflect);
                        coinReflectSprite.moveStart(player, enemy);
                        StartMoveScroll(enemy.areaNo);
                        progress = 114;
                    } else {
                        progress = 118;
                    }
                } else {
                    progress = 118;
                }
                break;
            case 114: /*射撃コイン反射*/
                coinReflectSprite.updateMove();
                if (coinReflectSprite.isMoving) break;
                progress = 118;
                break;
            case 118: /*敵射撃コインダメージ処理*/
                eCoinNo = enemyHasCoins.splice(usingEnemyCoinIndex, 1)[0];
                eCoin = gameData.coinDatas[eCoinNo];
                if (selectedCoinIndex < 0) {
                    PlaySound(soundHit);
                    playerLife -= eCoin.damage;
                } else {
                    pCoinNo = playerHasCoins.splice(selectedCoinIndex, 1)[0];
                    pCoin = gameData.coinDatas[pCoinNo];

                    if (pCoin.cutDamage < eCoin.damage) {
                        calcDamage = eCoin.damage - pCoin.cutDamage;
                        playerLife -= calcDamage;
                    } else {
                        calcDamage = Math.min(eCoin.damage, pCoin.counterDamage);
                        enemyLife -= calcDamage;
                    }
                    if (0 < calcDamage) {
                        PlaySound(soundHit);
                    } else {
                        PlaySound(soundReflect);
                    }

                    playerTrashCoins.push(pCoinNo);
                }
                enemyTrashCoins.push(eCoinNo);
                SetLife();

                progress = 120;
                break;
            case 120: /*敵移動先決定→移動*/
                progressTextSprite.frame = 5;
                var diceNum = Math.floor(Math.random() * 6) + 1;
                var leftAreaNo = enemy.areaNo - diceNum;
                var rightAreaNo = enemy.areaNo + diceNum;

                var eNextArea = 0;
                if (leftAreaNo < 0) {
                    /*左には行けないので右へ*/
                    eNextArea = rightAreaNo;
                } else if (12 < rightAreaNo) {
                    /*右には行けないので左へ*/
                    eNextArea = leftAreaNo;
                } else {
                    /*判断*/
                    var existLeftAreaSet = 0 != leftAreaNo && setSprites['B' + leftAreaNo].visible;
                    var existRightAreaSet = 12 != rightAreaNo && setSprites['B' + rightAreaNo].visible;
                    if (existLeftAreaSet == existRightAreaSet) {
                        /*より中央に近い方へ*/
                        var distanceLeft = Math.abs(leftAreaNo - 6);
                        var distanceRight = Math.abs(rightAreaNo - 6);
                        if (distanceLeft == distanceRight) {
                            /*同じならランダム*/
                            eNextArea = (0 == Math.floor(Math.random() * 2)) ? leftAreaNo : rightAreaNo;
                        } else {
                            eNextArea = (distanceLeft < distanceRight) ? leftAreaNo : rightAreaNo;
                        }
                    } else if (existLeftAreaSet) {
                        eNextArea = rightAreaNo;
                    } else if (existRightAreaSet) {
                        eNextArea = leftAreaNo;
                    }
                }
                enemy.moveStart(eNextArea);
                StartMoveScroll(eNextArea);
                progress = 121;
                break;
            case 121: /*敵移動中→ダメージ処理*/
                if (enemy.isMoving) break;
                if (0 != shiftScrollX) break;
                if (0 != enemy.areaNo && 12 != enemy.areaNo) {
                    checkAreaName = 'B' + enemy.areaNo;
                    if (setSprites[checkAreaName].visible) {
                        PlaySound(soundBomb);
                        setEffectSprite.show(enemy.areaNo);
                        progress = 122;
                    } else {
                        progress = 128;
                    }
                } else {
                    progress = 128;
                }
                break;
            case 122:
                setEffectSprite.updateFrame();
                scene.inputWait();
                if (setEffectSprite.visible) break;
                progress = 128;
                break;
            case 128: /*設置選択→設置*/
                progressTextSprite.frame = 10;
                player.turnAround(enemy.areaNo);
                enemy.turnAround(player.areaNo);
                if (0 != enemy.areaNo && 12 != enemy.areaNo) {
                    checkAreaName = 'B' + enemy.areaNo;
                    if (setSprites[checkAreaName].visible) {
                        pCoinNo = setSprites[checkAreaName].coinNo;
                        enemyLife -= gameData.coinDatas[pCoinNo].damage;
                        playerTrashCoins.push(pCoinNo);
                        setSprites[checkAreaName].visible = false;

                        SetLife();
                    }
                }

                usingEnemyCoinIndex = GetEnemyUseCoinIndex(0);
                if (0 <= usingEnemyCoinIndex) {
                    var eSprite = setSprites['R' + enemy.areaNo];
                    eSprite.visible = true;
                    eSprite.coinNo = enemyHasCoins.splice(usingEnemyCoinIndex, 1)[0];
                }

                turnSprite.frame++;
                StartMoveScroll(player.areaNo);
                progressTextSprite.frame = 11;
                progress = 130;
                break;
            case 130:
                if (0 != shiftScrollX) break;
                progress = 0;
                break;
        }

        switch (progress) {
            case 999:
                soundBGM.stop();
                //同じライフなら敵の勝ち
                gameData.result = (enemyLife < playerLife);
                scene.moveSceneTo(sceneResult);
                break;
            default:
                if (playerLife < 1 || enemyLife < 1 || turnSprite.frame == 15) {
                    shiftScrollX = 0;
                    showUpSprite.frame = (turnSprite.frame == 15) ? 3 : 2;
                    showUpSprite.visible = true;
                    progressTextSprite.visible = false;
                    progress = 999;
                    scene.inputWait(40);
                }
                break;
        }
    });

    gameData.scenes[sceneBattle] = scene;
    return scene;
}
