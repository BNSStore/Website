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
                this.game.state.start('TitleScreen');
                this.stage.disableVisibilityChange = true;
                this.scale.scaleMode = Phaser.ScaleManager.USER_SCALE;
                $(window).trigger('resize');
            };
            return Boot;
        })(Phaser.State);
        Cosh.Boot = Boot;
    })(Cosh = SLouple.Cosh || (SLouple.Cosh = {}));
})(SLouple || (SLouple = {}));
//# sourceMappingURL=boot.js.map