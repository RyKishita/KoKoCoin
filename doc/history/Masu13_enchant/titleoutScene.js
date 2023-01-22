function CreateTitleOutScene(gameData) {
    var game = enchant.Game.instance;

    var scene = new SceneEx(gameData);
    var group = new Group();

    var sprite = new Sprite(320, 320);
    sprite.image = game.assets['start.png'];
    sprite.x = 0;
    sprite.y = 0;

    group.addChild(sprite);
    scene.addChild(group);

    var firstStep = true;

    scene.moveSceneTo(sceneMainMenu);
    scene.addEventListener(enchant.Event.ENTER_FRAME, function (e) {
        scene.commonProcess();
        if (firstStep) {
            firstStep = false;
            PlaySound(soundSave);
        }
    });

    gameData.scenes[sceneTitleOut] = scene;
    return scene;
}