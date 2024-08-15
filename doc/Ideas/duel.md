# デュエル

- 移動先が複数あり、どちらに移動しても敗北でデュエル終了となる場合に切断される可能性があるが、持ち時間の間待たなければならない
  - 相手の切断検出を考える
  - ダイス目のランダム表示値は飾りであり振る前に結果は出ているので、結果を確定してしまう。勝つ側は移動先をさっさと選んで終了処理へ進む
  - 出来るといいけど複雑になるので保留
- 不正プレイヤー対策
  - ストックコインとか手持ちコインを見えるようにする不正
    - コイン種類を渡さず、明らかでないコインはIDのみで管理
      - 明らかになる際に差し替えられる場合がある
- 上部の手持ちコインやリソースの変動にアニメーション
  - コインが無くなる時はフェードアウト。終わったら間を詰める。現状アイテムの使いまわしをしているが、やめて追加されたら末尾に追加
    - 一時的に幅を超えてしまう。間を詰めるのも同時
  - とりあえずリソースはライフ同様に出来る
  - 選択しているコインを使うと残りリソースはどれだけになるかの表示
  - ライフ減少のアニメーションに注目してほしいので保留
- デュエルで発射型のオブジェクトが盾と衝突する際、弾など小さいものだと盾に当たる前に消える
  - 許容内としておく
- アニメーション省略設定
  - デュエル時間の短縮につながり、高速に回せるとゲーム寿命に関わる
    - ある程度遊んだ人用
- あるゲームを参考にした見た目改良案
  - 値の変化がある項目を強調。ただし一瞬だけで強すぎないように注意
  - 各ボタンのうち、デュエルのメインとなるものは形状を変える。色は変えない
- 状態異常、エリアステータス、コインステータスの統合を検討
  - エリアステータスに状態異常は対応
  - コインに対するタグ追加もエリアに対するタグ追加。タグが付いたエリアに対する効果追加がセット
  - エリアタイプ変更はプレイヤーやコインについたらどうなるか
    - 統合はするが、項目ごとに対象を限定できるようにする。
      - だったら今のクラスのままでベースクラスなりインターフェイスで統合でいいのかも
        - データはデュエル中に相手との交換デシリアライズされる。統一されたデータは作れなそうだった
  - するなら似ている別クラスで追加
- デュエル後の再戦選択
  - デュエル画面のまま勝敗結果表示をして、合わせて選択
  - オンライン時は再戦可能回数は1とするなど
- 手持ちコインをプレイヤーの周りを回らせる
- コイン選択時に、選択→OKの2ステップではなく、コインの上フリックでも可にする
- コイン設置時の表現
  - コインに依存した表現。地中から出てくるとか半透明から実体化など
- 状態異常はキャラクターに常時アニメーションがついてほしい
  - または、キャラクター近くに表示。その場合は文字だと一杯になるので、状態異常毎にアイコンを付ける
  - 文字は出すようにした
- CoinStatus がクリアされるタイミングが不明
  - 手持ちコインの間に強化された設置攻撃コインが設置される際に維持する為
  - ラウンド内は捨てられようがストックに戻ろうが維持される事とする
- 直接攻撃コインのアニメーションをパーティクルで作ったほうがいいものは入れ替え
  - ダメージ属性によるエフェクトと統合したほうがいいのかも
  - 候補
    - DirectAttackAnimationNearFrame
      - 火炎放射器
    - DirectAttackAnimationNearFallen
      - 油壷
    - DirectAttackAnimationFarThrowsRandom
      - 貧者の石ころ
    - DirectAttackAnimationFarThrowsContinue
      - 群衆のイシツブテ
    - DirectAttackAnimationFarSlash
      - DimensionSlash
    - DirectAttackAnimationFarPushUps
      - PlantParty
- Resourceを消費する行動をする際には、消費量がリソース表示の場所で分かるようにする
  - コインを使用する際、選択した時点でリソース表示の〇に枠を付けるなど
- 敵コインを踏んだ時、敵の環境コインを維持するかどうか選択できるようにするかどうか。