enchant();

/********************************/
enchant.nineleap.memory.LocalStorage.DEBUG_MODE = true;
//enchant.nineleap.memory.LocalStorage.GAME_ID = 710;
/********************************/

window.onload = function () {
    var game = new Game(320, 320);
    game.fps = 15;
    game.preload(GetAssetList());
    game.memory.player.preload();
    if (!enchant.nineleap.memory.LocalStorage.DEBUG_MODE) {
        game.twitterRequest('account/verify_credentials');
        game.memories.friends.preload();
        game.memories.ranking.preload();
    }
    game.onload = function () {
        /* スペースキーとAキーとXキーをAボタンとして割り当て */
        game.keybind(' '.charCodeAt(0), 'a');
        game.keybind('A'.charCodeAt(0), 'a');
        game.keybind('a'.charCodeAt(0), 'a');
        game.keybind('X'.charCodeAt(0), 'a');
        game.keybind('x'.charCodeAt(0), 'a');

        /* BキーとZキーをBボタンとして割り当て */
        game.keybind('B'.charCodeAt(0), 'b');
        game.keybind('b'.charCodeAt(0), 'b');
        game.keybind('Z'.charCodeAt(0), 'b');
        game.keybind('z'.charCodeAt(0), 'b');

        var coinDatas = GetCoinData();

        var gameData = new GameData(coinDatas);
        gameData.load();

        var titleoutScene = CreateTitleOutScene(gameData);
        CreateMainMenuScene(gameData);
        CreateSettingScene(gameData);
        CreateTargetTypeSelectScene(gameData);
        CreateTargetSelectScene(gameData);
        CreateEditBagScene(gameData);
        CreateBattleScene(gameData);
        CreateResultScene(gameData);

        game.pushScene(titleoutScene);
    };
    game.start();
};
