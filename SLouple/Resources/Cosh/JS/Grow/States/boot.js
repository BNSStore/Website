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
        var Boot = (function (_super) {
            __extends(Boot, _super);
            function Boot() {
                _super.apply(this, arguments);
            }
            Boot.prototype.preload = function () {
                console.log('Boot');
                this.stage.setBackgroundColor('rgb(6,8,12)');
                this.input.maxPointers = 1;
                this.stage.disableVisibilityChange = true;
                this.scale.scaleMode = Phaser.ScaleManager.USER_SCALE;
                this.scale.fullScreenScaleMode = Phaser.ScaleManager.USER_SCALE;
                $(window).trigger('resize');
                this.load.image('Player', SLouple.Cosh.baseImageURL + 'Dandelion/Dandelion_100px/png');
            };
            Boot.prototype.create = function () {
                this.game.state.start('TitleScreen');
            };
            return Boot;
        })(Phaser.State);
        Cosh.Boot = Boot;
    })(Cosh = SLouple.Cosh || (SLouple.Cosh = {}));
})(SLouple || (SLouple = {}));
//# sourceMappingURL=Boot.js.map