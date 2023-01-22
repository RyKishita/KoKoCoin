function CreateTargetSelectScene(gameData) {
    var game = enchant.Game.instance;

    var scene = new SceneEx(gameData);
    var group = new Group();

    var backgroundSprite = new Sprite(backgroundWidth, backgroundWidth);
    backgroundSprite.image = enchant.Game.instance.assets[targetSelectBackAssetName];
    group.addChild(backgroundSprite);

    var selectArrowIcon = CreateSelectSprite();
    group.addChild(selectArrowIcon);

    var left = 50;
    var top = 10;
    var lineSpace = 20;

    var headerLabel = new Label('対戦相手の選択');
    headerLabel.x = 30;
    headerLabel.y = top;
    headerLabel.color = 'white';

    var pageNumText = new MutableText(0, 0, 48, '0/0');
    pageNumText.x = 200;
    pageNumText.y = top;

    var pageDownIcon = CreateArrowSprite();
    pageDownIcon.rotate(270);
    MoveMenuSelectArrow(pageDownIcon, pageNumText);

    var pageUpIcon = CreateArrowSprite();
    pageUpIcon.rotate(90);
    pageUpIcon.x = pageNumText.x + 16 * 3;
    pageUpIcon.y = pageDownIcon.y;

    group.addChild(headerLabel);
    group.addChild(pageNumText);
    group.addChild(pageDownIcon);
    group.addChild(pageUpIcon);
    top += lineSpace;

    var backText = new Text(left, 280, 'Back');
    backText.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundCancel);
        scene.moveSceneTo(sceneTargetTypeSelect);
    });
    group.addChild(backText);

    var selectedIndex = 0;
    var pageIndex = 0;
    var pageNum = 0;
    function SetSelectText() {
        if (selectedIndex == 10) {
            MoveMenuSelectArrow(selectArrowIcon, backText);
        } else {
            MoveMenuSelectArrow(selectArrowIcon, targetLabels[selectedIndex]);
        }
    }

    function MoveNext() {
        if (selectedIndex == 10) {
            PlaySound(soundCancel);
            scene.moveSceneTo(sceneTargetTypeSelect);
        } else {
            var index = pageIndex * 10 + selectedIndex;
            if (index < enemyInfos.length) {
                PlaySound(soundOK);
                gameData.targetData = enemyInfos[index];
                scene.moveSceneTo(sceneBattle);
            }
        }
    }

    var targetLabels = new Array(10);
    for (var i = 0; i < 10; i++) {
        var label = new Label('');
        label.x = left;
        label.y = top;
        label.color = 'white';
        label.addEventListener(enchant.Event.TOUCH_START, function (e) {
            selectedIndex = targetLabels.indexOf(this);
            MoveNext();
        });

        targetLabels[i] = label;
        group.addChild(label);

        top += lineSpace;
    }

    scene.addChild(group);

    /*--------------------------------*/

    var enemyInfos = new Array();

    function SetList() {
        for (var i = 0; i < 10; i++) {
            var label = targetLabels[i];
            var index = pageIndex * 10 + i;
            if (index < enemyInfos.length) {
                var enemyInfo = enemyInfos[index];
                label.text = enemyInfo.toString();
            } else {
                label.text = '----------';
            }
        }
        pageNumText.text = '' + (pageIndex + 1) + '/' + pageNum;
    }

    pageDownIcon.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundSelect);
        pageIndex--;
        if (pageIndex < 0) {
            pageIndex = pageNum - 1;
        }
        SetList();
        scene.inputWait();
    });
    pageUpIcon.addEventListener(enchant.Event.TOUCH_START, function (e) {
        PlaySound(soundSelect);
        pageIndex++;
        if (pageNum <= pageIndex) {
            pageIndex = 0;
        }
        SetList();
    });

    scene.addEventListener(Event_SceneExStarting, function (e) {
        enemyInfos.length = 0;
        switch (gameData.targetType) {
            case 0:
                enemyInfos.push(new CharactorInfoPractice(0));
                enemyInfos.push(new CharactorInfoPractice(1));
                enemyInfos.push(new CharactorInfoPractice(2));
                break;
            case 1:
                for (var i in game.memories.friends) {
                    var friend = game.memories.friends[i];
                    if (friend.data && friend.data.useCoins) {
                        enemyInfos.push(new CharactorInfoUser(friend, false));
                    }
                }
                break;
            case 2:
                for (var i in game.memories.ranking) {
                    var ranker = game.memories.ranking[i];
                    if (ranker.screen_name == game.memory.player.screen_name) continue;
                    if (ranker.data && ranker.data.useCoins) {
                        enemyInfos.push(new CharactorInfoUser(ranker, true));
                    }
                }
                break;
        }
        pageNum = Math.floor(enemyInfos.length / 10) + 1;

        if (pageNum < 2) {
            pageNumText.visible = false;
            pageDownIcon.visible = false;
            pageUpIcon.visible = false;
        } else {
            pageNumText.visible = true;
            pageDownIcon.visible = true;
            pageUpIcon.visible = true;
        }

        SetList();

        selectedIndex = 0;
        SetSelectText();
        scene.opacity = 0;
    });
    scene.addEventListener(enchant.Event.ENTER_FRAME, function (e) {
        if (scene.commonProcess()) return;

        if (game.input.up) {
            PlaySound(soundSelect);
            selectedIndex--;
            if (selectedIndex < 0) {
                selectedIndex = 10;
            }
            SetSelectText();
            scene.inputWait();
        } else if (game.input.down) {
            PlaySound(soundSelect);
            selectedIndex++;
            if (10 < selectedIndex) {
                selectedIndex = 0;
            }
            SetSelectText();
            scene.inputWait();
        } else if (game.input.left) {
            PlaySound(soundSelect);
            pageIndex--;
            if (pageIndex < 0) {
                pageIndex = pageNum - 1;
            }
            SetList();
            scene.inputWait();
        } else if (game.input.right) {
            PlaySound(soundSelect);
            pageIndex++;
            if (pageNum <= pageIndex) {
                pageIndex = 0;
            }
            SetList();
            scene.inputWait();
        } else if (game.input.a) {
            MoveNext();
        } else if (game.input.b) {
            PlaySound(soundCancel);
            scene.moveSceneTo(sceneTargetTypeSelect);
        }
    });

    gameData.scenes[sceneTargetSelect] = scene;
    return scene;
}
