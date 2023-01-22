function CreateEditBagScene(gameData) {
    var game = enchant.Game.instance;

    var scene = new SceneEx(gameData);

    /*---------------------*/
    var mainGroup = new Group();

    var backgroundSprite = new Sprite(backgroundWidth, backgroundWidth);
    backgroundSprite.image = enchant.Game.instance.assets[editBagBackAssetName];

    var buttonsMargin = 5;
    var scrollUpArrowIcon = CreateArrowSprite();
    scrollUpArrowIcon.x = buttonsMargin;
    scrollUpArrowIcon.y = buttonsMargin;

    var scrollDownArrowIcon = CreateArrowSprite();
    scrollDownArrowIcon.rotate(180);
    scrollDownArrowIcon.x = buttonsMargin;
    scrollDownArrowIcon.y = 320 - buttonsMargin - scrollDownArrowIcon.height;

    var filterTop = 60;
    var filterBackWidth = buttonsMargin * 2 + editBagButtonSize;
    var filterBackHeight = 145;
    var filterBackSurface = new Surface(filterBackWidth, filterBackHeight);
    filterBackSurface.context.fillStyle = 'gray';
    filterBackSurface.context.fillRect(0, 0, filterBackWidth, filterBackHeight);

    var filterBackSprite = new Sprite(filterBackWidth, filterBackHeight);
    filterBackSprite.image = filterBackSurface;
    filterBackSprite.x = 0;
    filterBackSprite.y = filterTop - buttonsMargin;
    filterBackSprite.visible = false;

    var buttonSpace = 30;
    var buttonALLSprite = CreateEditBagButtonSprite(0);
    buttonALLSprite.x = buttonsMargin;
    buttonALLSprite.y = filterTop;

    var buttonUseSprite = CreateEditBagButtonSprite(1);
    buttonUseSprite.x = buttonsMargin;
    buttonUseSprite.y = buttonALLSprite.y + buttonSpace;

    var buttonSetSprite = CreateEditBagButtonSprite(2);
    buttonSetSprite.x = buttonsMargin;
    buttonSetSprite.y = buttonUseSprite.y + buttonSpace;

    var buttonShotSprite = CreateEditBagButtonSprite(3);
    buttonShotSprite.x = buttonsMargin;
    buttonShotSprite.y = buttonSetSprite.y + buttonSpace;

    var buttonShieldSprite = CreateEditBagButtonSprite(4);
    buttonShieldSprite.x = buttonsMargin;
    buttonShieldSprite.y = buttonShotSprite.y + buttonSpace;

    var buttonEndSprite = CreateEditBagButtonSprite(5);
    buttonEndSprite.x = buttonsMargin;
    buttonEndSprite.y = buttonShieldSprite.y + buttonSpace * 2;

    mainGroup.addChild(backgroundSprite);
    mainGroup.addChild(scrollUpArrowIcon);
    mainGroup.addChild(scrollDownArrowIcon);
    mainGroup.addChild(filterBackSprite);
    mainGroup.addChild(buttonALLSprite);
    mainGroup.addChild(buttonUseSprite);
    mainGroup.addChild(buttonSetSprite);
    mainGroup.addChild(buttonShotSprite);
    mainGroup.addChild(buttonShieldSprite);
    mainGroup.addChild(buttonEndSprite);

    scene.addChild(mainGroup);
    /*---------------------*/
    var coinsGroup = new Group();

    var selectArrowIcon = CreateSelectSprite();

    coinsGroup.addChild(selectArrowIcon);

    scene.addChild(coinsGroup);
    /*---------------------*/

    var boardGroup = new Group();

    var boardSprite = CreateMenuBoardSprite();
    boardSprite.x = 40;
    boardSprite.y = 40;

    var boardTextLeft = boardSprite.x + 40;
    var boardTextTop = boardSprite.y + 40;
    var boardLineSpace = 20;
    var boardLabelColor = 'white';

    var coinExplainLabelNo = new Label('');
    coinExplainLabelNo.color = boardLabelColor;
    coinExplainLabelNo.x = boardTextLeft;
    coinExplainLabelNo.y = boardTextTop;

    var coinExplainLabel1 = new Label('');
    coinExplainLabel1.color = boardLabelColor;
    coinExplainLabel1.x = boardTextLeft;
    coinExplainLabel1.y = coinExplainLabelNo.y + boardLineSpace;

    var coinExplainLabel2 = new Label('');
    coinExplainLabel2.color = boardLabelColor;
    coinExplainLabel2.x = boardTextLeft;
    coinExplainLabel2.y = coinExplainLabel1.y + boardLineSpace;
    var coinExplainLabel3 = new Label('');
    coinExplainLabel3.color = boardLabelColor;
    coinExplainLabel3.x = boardTextLeft;
    coinExplainLabel3.y = coinExplainLabel2.y + boardLineSpace;

    var coinExplainLabels = new Array();
    coinExplainLabels.push(coinExplainLabel1);
    coinExplainLabels.push(coinExplainLabel2);
    coinExplainLabels.push(coinExplainLabel3);

    var hasCoinNumTitleLabel = new Label('所有コイン数');
    hasCoinNumTitleLabel.color = boardLabelColor;
    hasCoinNumTitleLabel.x = boardTextLeft;
    hasCoinNumTitleLabel.y = 180;

    var hasCoinNumLabel = new MutableText(0, 0, 32, '0');
    hasCoinNumLabel.color = boardLabelColor;
    hasCoinNumLabel.x = 180;
    hasCoinNumLabel.y = hasCoinNumTitleLabel.y;

    var useCoinTitleLabel = new Label('使用コイン数');
    useCoinTitleLabel.color = boardLabelColor;
    useCoinTitleLabel.x = boardTextLeft;
    useCoinTitleLabel.y = hasCoinNumTitleLabel.y + boardLineSpace;

    var useCoinNumText = new MutableText(0, 0, 32, '0');
    useCoinNumText.x = hasCoinNumLabel.x;
    useCoinNumText.y = useCoinTitleLabel.y;

    var useCoinNumDownIcon = CreateArrowSprite();
    useCoinNumDownIcon.rotate(270);
    MoveMenuSelectArrow(useCoinNumDownIcon, useCoinNumText);

    var useCoinNumUpIcon = CreateArrowSprite();
    useCoinNumUpIcon.rotate(90);
    useCoinNumUpIcon.x = useCoinNumText.x + 16;
    useCoinNumUpIcon.y = useCoinNumDownIcon.y;

    var boardSelectArrowIcon = CreateSelectSprite();
    boardSelectArrowIcon.x = boardTextLeft - 16;
    boardSelectArrowIcon.y = 240;

    var backText = new Text(0, 0, 'Back');
    backText.x = boardTextLeft;
    backText.y = boardSelectArrowIcon.y;

    boardGroup.addChild(boardSprite);
    boardGroup.addChild(coinExplainLabelNo);
    boardGroup.addChild(coinExplainLabel1);
    boardGroup.addChild(coinExplainLabel2);
    boardGroup.addChild(coinExplainLabel3);
    boardGroup.addChild(hasCoinNumTitleLabel);
    boardGroup.addChild(hasCoinNumLabel);
    boardGroup.addChild(useCoinTitleLabel);
    boardGroup.addChild(useCoinNumDownIcon);
    boardGroup.addChild(useCoinNumText);
    boardGroup.addChild(useCoinNumUpIcon);
    boardGroup.addChild(boardSelectArrowIcon);
    boardGroup.addChild(backText);

    scene.addChild(boardGroup);
    /*---------------------*/

    var editCoinNos = new Array();
    var editCoinSprites = new Array();
    var editCoinLabels = new Array();
    var columnNum = 3;
    var rowNum = 0;
    var showTopRow = 0;
    var columnIndex = 0;
    var rowIndex = 0;
    var columnWidth = coinSize3 + 28;
    var rowHeight = coinSize3 + 16;
    var filterList = 0;

    function AdjustSelected() {
        if (columnIndex < 0) {
            columnIndex = columnNum - 1;
        } else if (columnNum <= columnIndex) {
            columnIndex = 0;
        }
        if (rowIndex < 0) {
            rowIndex = 0;
        } else if (rowNum <= rowIndex) {
            rowIndex = rowNum - 1;
        }
        var index = rowIndex * columnNum + columnIndex;
        if (editCoinSprites.length <= index) {
            index = editCoinSprites.length - 1;
            rowIndex = rowNum - 1;
            columnIndex = index - rowIndex * columnNum;
        }
    }

    function MoveSelectArrow() {
        var index = rowIndex * columnNum + columnIndex;
        var visible = (0 <= index && index < editCoinSprites.length);
        if (visible) {
            var coinSprite = editCoinSprites[index];

            selectArrowIcon.x = coinSprite.x - selectArrowIcon.width;
            selectArrowIcon.y = coinSprite.y - (selectArrowIcon.height - coinSprite.height) / 2;
        }
        selectArrowIcon.visible = visible;
    }

    function ScrollList(up) {
        if (up) {
            if (showTopRow == 0) return;
            showTopRow--;
            coinsGroup.y += rowHeight;
        } else {
            if (rowIndex + 1 == rowNum) return;
            showTopRow++;
            coinsGroup.y -= rowHeight;
        }
    }

    function SetFilterList() {
        var opacity = 0.2;
        buttonALLSprite.opacity = (filterList == 0) ? 1 : opacity;
        buttonUseSprite.opacity = (filterList == 0 || filterList == 1) ? 1 : opacity;
        buttonSetSprite.opacity = (filterList == 0 || filterList == 2) ? 1 : opacity;
        buttonShotSprite.opacity = (filterList == 0 || filterList == 3) ? 1 : opacity;
        buttonShieldSprite.opacity = (filterList == 0 || filterList == 4) ? 1 : opacity;
    }

    function ShowBoard(show) {
        var opacity = show ? 0.1 : 1;
        selectArrowIcon.opacity = opacity;
        scrollUpArrowIcon.opacity = opacity;
        scrollDownArrowIcon.opacity = opacity;
        for (var i in editCoinSprites) {
            editCoinSprites[i].opacity = opacity;
        }
        for (var i in editCoinLabels) {
            editCoinLabels[i].opacity = opacity;
        }
        if (show) {
            buttonALLSprite.opacity = opacity;
            buttonUseSprite.opacity = opacity;
            buttonSetSprite.opacity = opacity;
            buttonShotSprite.opacity = opacity;
            buttonShieldSprite.opacity = opacity;
        } else {
            SetFilterList();
        }

        for (var n in boardGroup.childNodes) {
            boardGroup.childNodes[n].visible = show;
        }
    }

    function UpdateUseCoinNum(index) {
        var no = editCoinNos[index];
        var label = editCoinLabels[index];
        label.text = 'No' + (no + 1) + '(' + gameData.useCoins[no] + '/' + gameData.hasCoins[no] + ')';
    }

    function ChangeUseNumCurrentCoin(v) {
        var index = rowIndex * columnNum + columnIndex;
        var no = editCoinNos[index];

        var max = gameData.hasCoins[no];
        var num = gameData.useCoins[no] + v;
        if (num < 0) {
            num = max;
        } else if (max < num) {
            num = 0;
        }
        gameData.useCoins[no] = num;
        SetMenu();
        UpdateUseCoinNum(index);
    }

    function SetMenu() {
        var index = rowIndex * columnNum + columnIndex;
        if (index < 0 || editCoinNos.length <= index) return false;
        var no = editCoinNos[index];
        var coinData = gameData.coinDatas[no];

        coinExplainLabelNo.text = 'No' + (no + 1);
        coinData.setExplain(coinExplainLabels);
        hasCoinNumLabel.text = gameData.hasCoins[no].toString();
        useCoinNumText.text = gameData.useCoins[no].toString();
        return true;
    }

    var selectedBoardIndex = 0;
    function SetBoardSelected() {
        if (selectedBoardIndex < 0) {
            selectedBoardIndex = 1;
        } else if (1 < selectedBoardIndex) {
            selectedBoardIndex = 0;
        }
        switch (selectedBoardIndex) {
            case 0: MoveMenuSelectArrow(boardSelectArrowIcon, useCoinTitleLabel); break;
            case 1: MoveMenuSelectArrow(boardSelectArrowIcon, backText); break;
        }
    }

    backgroundSprite.addEventListener(enchant.Event.TOUCH_START, function (e) {
        if (boardSprite.visible) {
            PlaySound(soundCancel);
            ShowBoard(false);
        }
    });
    boardSprite.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundCancel);
        ShowBoard(false);
    });
    backText.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundCancel);
        ShowBoard(false);
    });
    useCoinNumDownIcon.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundOK);
        ChangeUseNumCurrentCoin(-1);
    });
    useCoinNumUpIcon.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundOK);
        ChangeUseNumCurrentCoin(1);
    });

    scrollUpArrowIcon.addEventListener(enchant.Event.TOUCH_START, function (e) {
        if (boardSprite.visible) return;
        PlaySound(soundSelect);
        rowIndex--;
        AdjustSelected();
        if (rowIndex < (showTopRow + 1)) {
            ScrollList(true);
        }
        MoveSelectArrow();
    });
    scrollDownArrowIcon.addEventListener(enchant.Event.TOUCH_START, function (e) {
        if (boardSprite.visible) return;
        PlaySound(soundSelect);
        rowIndex++;
        AdjustSelected();
        if ((showTopRow + 2) < rowIndex) {
            ScrollList(false);
        }
        MoveSelectArrow();
    });

    function MakeList() {
        editCoinNos.length = 0;
        for (var i = 0; i < coinNumMax; i++) {
            if (0 == gameData.hasCoins[i]) continue;

            var cd = gameData.coinDatas[i];
            switch (filterList) {
                case 0: break;
                case 1: if (0 < gameData.useCoins[i]) break; else continue;
                case 2: if (cd instanceof CoinInfoSet) break; else continue;
                case 3: if (cd instanceof CoinInfoShot) break; else continue;
                case 4: if (cd instanceof CoinInfoShield) break; else continue;
            }
            editCoinNos.push(i);
        }

        columnIndex = 0;
        rowIndex = 0;
        showTopRow = 0;
        coinsGroup.y = 0;

        /*前回分の削除*/
        for (var index in editCoinSprites) {
            coinsGroup.removeChild(editCoinSprites[index]);
        }
        editCoinSprites.length = 0;

        for (var index in editCoinLabels) {
            coinsGroup.removeChild(editCoinLabels[index]);
        }
        editCoinLabels.length = 0;

        var coinLeft = 60;
        var coinTop = 0;
        var column = 0;
        var row = 0;
        for (var index in editCoinNos) {
            var no = editCoinNos[index];

            var x = coinLeft + column * columnWidth;
            var y = coinTop + row * rowHeight;

            var sprite = gameData.coinDatas[no].toSprite();
            sprite.x = x + (coinSize3 - sprite.width) / 2;
            sprite.y = y + (coinSize3 - sprite.height) / 2;
            sprite.addEventListener(enchant.Event.TOUCH_START, function (e) {
                PlaySound(soundSelect);
                var i = editCoinSprites.indexOf(this);
                columnIndex = i % columnNum;
                rowIndex = Math.floor(i / columnNum);
                MoveSelectArrow();
                if (SetMenu()) ShowBoard(true);
            });
            editCoinSprites.push(sprite);
            coinsGroup.insertBefore(sprite, selectArrowIcon);

            var label = new Label('');
            label.x = x;
            label.y = y + 64;
            label.color = 'white';
            label.addEventListener(enchant.Event.TOUCH_START, function (e) {
                PlaySound(soundOK);
                var i = editCoinLabels.indexOf(this);
                columnIndex = i % columnNum;
                rowIndex = Math.floor(i / columnNum);
                MoveSelectArrow();
                ChangeUseNumCurrentCoin(1);
                UpdateUseCoinNum(i);
            });
            editCoinLabels.push(label);
            coinsGroup.insertBefore(label, selectArrowIcon);
            UpdateUseCoinNum(parseInt(index));

            column++;
            if (columnNum <= column) {
                column = 0;
                row++;
            }
        }
        rowNum = row;
        if (0 < column) {
            rowNum++;
        }
        MoveSelectArrow();
    }

    buttonALLSprite.addEventListener(enchant.Event.TOUCH_START, function (e) {
        if (boardSprite.visible) return;
        PlaySound(soundOK);
        filterList = 0;
        SetFilterList();
        MakeList();
    });
    buttonUseSprite.addEventListener(enchant.Event.TOUCH_START, function (e) {
        if (boardSprite.visible) return;
        PlaySound(soundOK);
        filterList = 1;
        SetFilterList();
        MakeList();
    });
    buttonSetSprite.addEventListener(enchant.Event.TOUCH_START, function (e) {
        if (boardSprite.visible) return;
        PlaySound(soundOK);
        filterList = 2;
        SetFilterList();
        MakeList();
    });
    buttonShotSprite.addEventListener(enchant.Event.TOUCH_START, function (e) {
        if (boardSprite.visible) return;
        PlaySound(soundOK);
        filterList = 3;
        SetFilterList();
        MakeList();
    });
    buttonShieldSprite.addEventListener(enchant.Event.TOUCH_START, function (e) {
        if (boardSprite.visible) return;
        PlaySound(soundOK);
        filterList = 4;
        SetFilterList();
        MakeList();
    });
    buttonEndSprite.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundSave);
        gameData.saveUserData();
        scene.moveSceneTo(sceneMainMenu);
    });

    scene.addEventListener(Event_SceneExStarting, function (e) {
        filterList = 0;
        filterBackSprite.visible = false;
        MakeList();
        ShowBoard(false);
        selectedBoardIndex = 1;
        SetBoardSelected();
        scene.opacity = 0;
    });
    scene.addEventListener(enchant.Event.ENTER_FRAME, function (e) {
        if (scene.commonProcess()) return;

        if (boardSprite.visible) {
            if (game.input.up) {
                PlaySound(soundSelect);
                selectedBoardIndex--;
                SetBoardSelected();
                scene.inputWait();
            } else if (game.input.down) {
                PlaySound(soundSelect);
                selectedBoardIndex++;
                SetBoardSelected();
                scene.inputWait();
            } else if (game.input.left) {
                PlaySound(soundOK);
                ChangeUseNumCurrentCoin(-1);
                selectedBoardIndex = 0;
                SetBoardSelected();
                scene.inputWait();
            } else if (game.input.right) {
                PlaySound(soundOK);
                ChangeUseNumCurrentCoin(1);
                selectedBoardIndex = 0;
                SetBoardSelected();
                scene.inputWait();
            } else if (game.input.a) {
                switch (selectedBoardIndex) {
                    case 0:
                        PlaySound(soundOK);
                        ChangeUseNumCurrentCoin(1);
                        break;
                    case 1:
                        PlaySound(soundCancel);
                        ShowBoard(false);
                        break;
                }
                scene.inputWait();
            } else if (game.input.b) {
                PlaySound(soundCancel);
                ShowBoard(false);
                scene.inputWait();
            }
        } else {
            if (game.input.up) {
                if (filterBackSprite.visible) {
                    PlaySound(soundOK);
                    filterList--;
                    if (filterList < 0) {
                        filterList = 4;
                    }
                    SetFilterList();
                    MakeList();
                } else {
                    PlaySound(soundSelect);
                    rowIndex--;
                    AdjustSelected();
                    if (rowIndex < (showTopRow + 1)) {
                        ScrollList(true);
                    }
                    MoveSelectArrow();
                }
                scene.inputWait();
            } else if (game.input.down) {
                if (filterBackSprite.visible) {
                    PlaySound(soundOK);
                    filterList++;
                    if (4 < filterList) {
                        filterList = 0;
                    }
                    SetFilterList();
                    MakeList();
                } else {
                    PlaySound(soundSelect);
                    rowIndex++;
                    AdjustSelected();
                    if ((showTopRow + 2) < rowIndex) {
                        ScrollList(false);
                    }
                    MoveSelectArrow();
                }
                scene.inputWait();
            } else if (game.input.left) {
                PlaySound(soundSelect);
                columnIndex--;
                if (columnIndex < 0 && !filterBackSprite.visible) {
                    filterBackSprite.visible = true;
                } else {
                    filterBackSprite.visible = false;
                    AdjustSelected();
                    MoveSelectArrow();
                }
                scene.inputWait();
            } else if (game.input.right) {
                PlaySound(soundSelect);
                columnIndex++;
                if (columnNum <= columnIndex && !filterBackSprite.visible) {
                    filterBackSprite.visible = true;
                } else {
                    filterBackSprite.visible = false;
                    AdjustSelected();
                    MoveSelectArrow();
                }
                scene.inputWait();
            } else if (game.input.a) {
                PlaySound(soundOK);
                if (SetMenu()) ShowBoard(true);
                scene.inputWait();
            } else if (game.input.b) {
                PlaySound(soundSave);
                gameData.saveUserData();
                scene.moveSceneTo(sceneMainMenu);
            }
        }
    });

    gameData.scenes[sceneEditBag] = scene;
    return scene;
};
