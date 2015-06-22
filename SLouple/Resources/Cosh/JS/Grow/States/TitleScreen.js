var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var SLouple;
(function (SLouple) {
    var Cosh;
    (function (Cosh) {
        var TitleScreen = (function (_super) {
            __extends(TitleScreen, _super);
            function TitleScreen() {
                _super.apply(this, arguments);
            }
            TitleScreen.prototype.preload = function () {
                console.log('TitleScreen');
                this.load.image('TitleScreen', SLouple.Cosh.baseImageURL + 'TitleScreen/png');
                this.load.image('TitleScreenStars', SLouple.Cosh.baseImageURL + 'TitleScreenStars/png');
                this.load.audio('TitleScreenBGM', [
                    SLouple.Cosh.baseAudioURL + 'TitleScreenBGM/ogg',
                    SLouple.Cosh.baseAudioURL + 'TitleScreenBGM/mp3'
                ]);
            };
            TitleScreen.prototype.create = function () {
                this.background = this.game.add.sprite(0, 0, 'TitleScreen');
                this.backgroundStars = this.game.add.sprite(0, 0, 'TitleScreenStars');
                this.backgroundStarsOpacity = 0;
                this.bgm = this.game.add.audio('TitleScreenBGM');
                this.bgm.loop = true;
                this.bgm.play();
            };
            TitleScreen.prototype.update = function () {
                this.background.height = this.game.height;
                this.background.width = this.game.width;
                this.backgroundStars.height = this.game.height;
                this.backgroundStars.width = this.game.width;
                if (this.backgroundStarsSwitch) {
                    this.backgroundStarsOpacity += 0.01;
                }
                else {
                    this.backgroundStarsOpacity -= 0.01;
                }
                if (this.backgroundStarsOpacity < 0) {
                    this.backgroundStarsOpacity = 0;
                    this.backgroundStarsSwitch = true;
                }
                else if (this.backgroundStarsOpacity > 1) {
                    this.backgroundStarsOpacity = 1;
                    this.backgroundStarsSwitch = false;
                }
                this.backgroundStars.alpha = this.backgroundStarsOpacity;
            };
            return TitleScreen;
        })(Phaser.State);
        Cosh.TitleScreen = TitleScreen;
    })(Cosh = SLouple.Cosh || (SLouple.Cosh = {}));
})(SLouple || (SLouple = {}));
//# sourceMappingURL=TitleScreen.js.map