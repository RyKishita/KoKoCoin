function CreateSettingScene(gameData) {
    var game = enchant.Game.instance;

    var scene = new SceneEx(gameData);
    var group = new Group();

    var backgroundSprite = new Sprite(backgroundWidth, backgroundWidth);
    backgroundSprite.image = enchant.Game.instance.assets[settingBackAssetName];
    var boardSprite = CreateMenuBoardSprite();
    var selectArrowIcon = CreateSelectSprite();

    var textHeader = new Label('自動戦闘時の思考パターン');
    textHeader.color = 'white';

    var text0 = new Label('1.防御コイン保持数');
    text0.color = 'white';
    var text0_0 = new MutableText(0, 0, 32, '1');
    var keepShieldNumDownIcon = CreateArrowSprite();
    keepShieldNumDownIcon.rotate(270);
    var keepShieldNumUpIcon = CreateArrowSprite();
    keepShieldNumUpIcon.rotate(90);

    var text1 = new Label('2.優先して保持するコインのサイズ');
    text1.color = 'white';
    var keepSizeIcon = CreateArrowSprite();
    keepSizeIcon.rotate(90);
    var text1_0 = new Text(0, 0, 'Auto');
    var text1_1 = new Text(0, 0, '1');
    var text1_2 = new Text(0, 0, '2');
    var text1_3 = new Text(0, 0, '3');

    var text2 = new Label('キャラクターグラフィック');
    text2.color = 'white';
    var characterSprite = new Sprite(characterSize, characterSize);
    var characterNoDownIcon = CreateArrowSprite();
    characterNoDownIcon.rotate(270);
    var characterNoUpIcon = CreateArrowSprite();
    characterNoUpIcon.rotate(90);

    var textBack = new Text(0, 0, 'Back');


    backgroundSprite.x = 0;
    backgroundSprite.y = 0;

    boardSprite.x = 40;
    boardSprite.y = 40;

    var left = boardSprite.x + 30;
    var top = boardSprite.y + 30;
    var lineSpace = 20;

    textHeader.x = left;
    textHeader.y = boardSprite.y + 10;

    text0.x = left;
    text0.y = textHeader.y + lineSpace;
    text0_0.x = left + 20;
    text0_0.y = text0.y + lineSpace;
    MoveMenuSelectArrow(keepShieldNumDownIcon, text0_0);
    keepShieldNumUpIcon.x = text0_0.x + 16;
    keepShieldNumUpIcon.y = keepShieldNumDownIcon.y;

    text1.x = left;
    text1.y = text0.y + 40;
    keepSizeIcon.x = left;
    keepSizeIcon.y = text1.y + lineSpace;
    text1_0.x = keepSizeIcon.x + 20;
    text1_0.y = text1.y + lineSpace;
    text1_1.x = text1_0.x + 80;
    text1_1.y = text1_0.y;
    text1_2.x = text1_1.x + 40;
    text1_2.y = text1_0.y;
    text1_3.x = text1_2.x + 40;
    text1_3.y = text1_0.y;

    text2.x = left;
    text2.y = text1.y + 60;

    characterNoDownIcon.x = left;
    characterNoDownIcon.y = text2.y + 30;
    characterSprite.x = characterNoDownIcon.x + characterNoDownIcon.width + 5;
    characterSprite.y = text2.y + 20;
    characterNoUpIcon.x = characterSprite.x + characterSprite.width + 5;
    characterNoUpIcon.y = characterNoDownIcon.y;

    textBack.x = left;
    textBack.y = top + 180;

    group.addChild(backgroundSprite);
    group.addChild(boardSprite);
    group.addChild(textHeader);
    group.addChild(text0);
    group.addChild(text0_0);
    group.addChild(keepShieldNumDownIcon);
    group.addChild(keepShieldNumUpIcon);
    group.addChild(text1);
    group.addChild(keepSizeIcon);
    group.addChild(text1_0);
    group.addChild(text1_1);
    group.addChild(text1_2);
    group.addChild(text1_3);
    group.addChild(text2);
    group.addChild(textBack);
    group.addChild(characterNoDownIcon);
    group.addChild(characterSprite);
    group.addChild(characterNoUpIcon);
    group.addChild(selectArrowIcon);

    scene.addChild(group);

    /*----↑ここまでUI初期化 -- ↓ここからゲーム処理 ----*/

    var selectedIndex = 0;

    function MoveNext() {
        switch (selectedIndex) {
            case 3:
                PlaySound(soundSave);
                gameData.saveUserData();
                scene.moveSceneTo(sceneMainMenu);
                break;
        }
    }

    function SetSelectText() {
        if (selectedIndex < 0) {
            selectedIndex = 3;
        } else if (3 < selectedIndex) {
            selectedIndex = 0;
        }

        var text;
        switch (selectedIndex) {
            case 0: text = text0; break;
            case 1: text = text1; break;
            case 2: text = text2; break;
            case 3: text = textBack; break;
        }
        MoveMenuSelectArrow(selectArrowIcon, text);
    }

    function SetKeepShieldNum() {
        if (gameData.keepShieldNum < 0) {
            gameData.keepShieldNum = 6;
        } else if (6 < gameData.keepShieldNum) {
            gameData.keepShieldNum = 0;
        }
        text0_0.text = gameData.keepShieldNum.toString();
    }

    function SetSelectKeepSize() {
        if (gameData.keepSize < 0) {
            gameData.keepSize = 3;
        } else if (3 < gameData.keepSize) {
            gameData.keepSize = 0;
        }
        var text;
        switch (gameData.keepSize) {
            case 0: text = text1_0; break;
            case 1: text = text1_1; break;
            case 2: text = text1_2; break;
            case 3: text = text1_3; break;
        }
        MoveMenuSelectArrow(keepSizeIcon, text);
    }

    keepShieldNumDownIcon.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundOK);
        gameData.keepShieldNum--;
        SetKeepShieldNum();
        selectedIndex = 0;
        SetSelectText();
    });
    keepShieldNumUpIcon.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundOK);
        gameData.keepShieldNum++;
        SetKeepShieldNum();
        selectedIndex = 0;
        SetSelectText();
    });
    text1_0.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundOK);
        gameData.keepSize = 0;
        SetSelectKeepSize();
        selectedIndex = 1;
        SetSelectText();
    });
    text1_1.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundOK);
        gameData.keepSize = 1;
        SetSelectKeepSize();
        selectedIndex = 1;
        SetSelectText();
    });
    text1_2.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundOK);
        gameData.keepSize = 2;
        SetSelectKeepSize();
        selectedIndex = 1;
        SetSelectText();
    });
    text1_3.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundOK);
        gameData.keepSize = 3;
        SetSelectKeepSize();
        selectedIndex = 1;
        SetSelectText();
    });
    characterNoDownIcon.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundOK);
        gameData.characterNo--;
        SetCharacter();
        selectedIndex = 2;
        SetSelectText();
    });
    characterNoUpIcon.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundOK);
        gameData.characterNo++;
        SetCharacter();
        selectedIndex = 2;
        SetSelectText();
    });
    textBack.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundSave);
        gameData.saveUserData();
        scene.moveSceneTo(sceneMainMenu);
    });

    function SetCharacter() {
        if (gameData.characterNo < 0) {
            gameData.characterNo = 6;
        } else if (6 < gameData.characterNo) {
            gameData.characterNo = 0;
        }
        characterSprite.image = CreatePlayerSurface(gameData.characterNo);
    }

    scene.addEventListener(Event_SceneExStarting, function (e) {
        selectedIndex = 0;
        SetSelectText();
        SetKeepShieldNum();
        SetSelectKeepSize();
        SetCharacter();

        scene.opacity = 0;
    });

    scene.addEventListener(enchant.Event.ENTER_FRAME, function (e) {
        if (scene.commonProcess()) return;

        if (game.input.up) {
            PlaySound(soundSelect);
            selectedIndex--;
            SetSelectText();
            scene.inputWait();
        } else if (game.input.down) {
            PlaySound(soundSelect);
            selectedIndex++;
            SetSelectText();
            scene.inputWait();
        } else if (game.input.left) {
            switch (selectedIndex) {
                case 0:
                    PlaySound(soundOK);
                    gameData.keepShieldNum--;
                    SetKeepShieldNum();
                    break;
                case 1:
                    PlaySound(soundOK);
                    gameData.keepSize--;
                    SetSelectKeepSize();
                    break;
                case 2:
                    PlaySound(soundOK);
                    gameData.characterNo--;
                    SetCharacter();
                    break;
            }
            scene.inputWait();
        } else if (game.input.right) {
            switch (selectedIndex) {
                case 0:
                    PlaySound(soundOK);
                    gameData.keepShieldNum++;
                    SetKeepShieldNum();
                    break;
                case 1:
                    PlaySound(soundOK);
                    gameData.keepSize++;
                    SetSelectKeepSize();
                    break;
                case 2:
                    PlaySound(soundOK);
                    gameData.characterNo++;
                    SetCharacter();
                    break;
            }
            scene.inputWait();
        } else if (game.input.a) {
            MoveNext();
        } else if (game.input.b) {
            PlaySound(soundSave);
            gameData.saveUserData();
            scene.moveSceneTo(sceneMainMenu);
        }
    });

    gameData.scenes[sceneSetting] = scene;
    return scene;
}
