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
        var Player = (function (_super) {
            __extends(Player, _super);
            function Player(game) {
                this.maxSpeed = 10;
                this.maxSpeedRadius = 900;
                this.maxAngle = 45;
                _super.call(this, game, 0, 0, 'Player');
                this.anchor.set(0.5, 0.5);
            }
            Player.prototype.update = function () {
                var xDistance = this.game.input.worldX - this.x;
                var yDistance = this.game.input.worldY - this.y;
                var distance = Math.sqrt(Math.pow(xDistance, 2) + Math.pow(yDistance, 2));
                var percentage = this.maxSpeed / distance;
                this.xSpeed = xDistance * percentage;
                this.ySpeed = yDistance * percentage;
                if (distance < this.maxSpeedRadius) {
                    var factor = distance / this.maxSpeedRadius;
                    this.xSpeed *= factor;
                    this.ySpeed *= factor;
                }
                this.x += this.xSpeed;
                this.y += this.ySpeed;
                this.angle = this.xSpeed / this.maxSpeed * this.maxAngle;
            };
            return Player;
        })(Phaser.Sprite);
        Cosh.Player = Player;
    })(Cosh = SLouple.Cosh || (SLouple.Cosh = {}));
})(SLouple || (SLouple = {}));
//# sourceMappingURL=Player.js.map