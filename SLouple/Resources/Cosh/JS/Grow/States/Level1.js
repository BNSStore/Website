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
        var Level1 = (function (_super) {
            __extends(Level1, _super);
            function Level1() {
                this.levelName = "Level 1";
                _super.call(this);
            }
            Level1.prototype.preload = function () {
                _super.prototype.preload.call(this);
            };
            Level1.prototype.create = function () {
                _super.prototype.create.call(this);
                var bmd = this.add.bitmapData(1920, 1080);
                bmd.ctx.beginPath();
                bmd.ctx.rect(0, 0, 200, 200);
                bmd.ctx.fillStyle = "rgb(255,255,255)";
                bmd.ctx.fill();
                this.add.sprite(500, 300, bmd);
                console.log("Added block");
            };
            Level1.prototype.update = function () {
                _super.prototype.update.call(this);
            };
            return Level1;
        })(Cosh.Level);
        Cosh.Level1 = Level1;
    })(Cosh = SLouple.Cosh || (SLouple.Cosh = {}));
})(SLouple || (SLouple = {}));
//# sourceMappingURL=Level1.js.map