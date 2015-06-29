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
        var Level = (function (_super) {
            __extends(Level, _super);
            function Level() {
                _super.call(this);
            }
            Level.prototype.preload = function () {
                console.log(this.levelName);
            };
            Level.prototype.create = function () {
                this.game.world.setBounds(0, 0, 5000, 1080);
                this.player = new Cosh.Player(this.game);
                this.game.add.existing(this.player);
                console.log("Added Player");
                this.game.camera.follow(this.player);
            };
            Level.prototype.update = function () {
            };
            Level.prototype.render = function () {
                this.game.debug.cameraInfo(this.game.camera, 32, 32);
                this.game.debug.spriteCoords(this.player, 32, 500);
            };
            return Level;
        })(Phaser.State);
        Cosh.Level = Level;
    })(Cosh = SLouple.Cosh || (SLouple.Cosh = {}));
})(SLouple || (SLouple = {}));
//# sourceMappingURL=Level.js.map