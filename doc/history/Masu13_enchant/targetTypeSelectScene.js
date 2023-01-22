function CreateTargetTypeSelectScene(gameData) {
    var game = enchant.Game.instance;

    var scene = new SceneEx(gameData);
    var group = new Group();

    var backgroundSprite = new Sprite(backgroundWidth, backgroundWidth);
    backgroundSprite.image = enchant.Game.instance.assets[targetTypeSelectBackAssetName];

    var boardSprite = CreateMenuBoardSprite();

    var selectArrowIcon = CreateSelectSprite();
    var text0 = new Text(0, 0, 'Practice');
    var text1 = new Text(0, 0, 'Friends');
    var text2 = new Text(0, 0, 'Ranker');
    var text3 = new Text(0, 0, 'Back');

    backgroundSprite.x = 0;
    backgroundSprite.y = 0;

    boardSprite.x = 40;
    boardSprite.y = 40;

    var left = boardSprite.x + 30;
    var top = boardSprite.y + 30;
    var lineSpace = 40;
    text0.x = left;
    text0.y = top;
    text1.x = left;
    text1.y = text0.y + lineSpace;
    text2.x = left;
    text2.y = text1.y + lineSpace;
    text3.x = left;
    text3.y = top + 160;

    group.addChild(backgroundSprite);
    group.addChild(boardSprite);
    group.addChild(text0);
    group.addChild(text1);
    group.addChild(text2);
    group.addChild(text3);
    group.addChild(selectArrowIcon);

    scene.addChild(group);

    /*----↑ここまでUI初期化 -- ↓ここからゲーム処理 ----*/

    var selectedIndex = 0;

    function MoveNext() {
        if (selectedIndex == 3) {
            PlaySound(soundCancel);
            scene.moveSceneTo(sceneMainMenu);
        } else {
            PlaySound(soundOK);
            gameData.targetType = selectedIndex;
            scene.moveSceneTo(sceneTargetSelect);
        }
    }

    function SetSelectText() {
        var text;
        switch (selectedIndex) {
            case 0: text = text0; break;
            case 1: text = text1; break;
            case 2: text = text2; break;
            case 3: text = text3; break;
        }
        MoveMenuSelectArrow(selectArrowIcon, text);
    }
    SetSelectText();

    function TouchText(index) {
        selectedIndex = index;
        MoveNext();
    }

    text0.addEventListener(enchant.Event.TOUCH_START, function (e) { TouchText(0); });
    text1.addEventListener(enchant.Event.TOUCH_START, function (e) { TouchText(1); });
    text2.addEventListener(enchant.Event.TOUCH_START, function (e) { TouchText(2); });
    text3.addEventListener(enchant.Event.TOUCH_START, function (e) { TouchText(3); });

    scene.opacity = 0;

    scene.addEventListener(enchant.Event.ENTER_FRAME, function (e) {
        if (scene.commonProcess()) return;

        if (game.input.up) {
            PlaySound(soundSelect);
            selectedIndex--;
            if (selectedIndex < 0) {
                selectedIndex = 3;
            }
            SetSelectText();
            scene.inputWait();
        } else if (game.input.down) {
            PlaySound(soundSelect);
            selectedIndex++;
            if (3 < selectedIndex) {
                selectedIndex = 0;
            }
            SetSelectText();
            scene.inputWait();
        } else if (game.input.a) {
            MoveNext();
        } else if (game.input.b) {
            PlaySound(soundCancel);
            scene.moveSceneTo(sceneMainMenu);
        }
    });

    gameData.scenes[sceneTargetTypeSelect] = scene;
    return scene;
}
