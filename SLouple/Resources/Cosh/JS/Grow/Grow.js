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
        var Grow = (function (_super) {
            __extends(Grow, _super);
            function Grow() {
                //super(864, 486, Phaser.AUTO, 'grow-container');
                _super.call(this, 1920, 1080, Phaser.AUTO, 'grow-container');
                this.state.add('Boot', Cosh.Boot, false);
                this.state.add('TitleScreen', Cosh.TitleScreen, false);
                this.state.start('Boot');
                //this.scale.scaleMode = Phaser.ScaleManager.USER_SCALE;
            }
            Grow.prototype.render = function () {
                this.fps++;
            };
            Grow.prototype.reportFPS = function () {
                this.fps = 0;
            };
            Grow.prototype.resize = function () {
                console.log('Resizing');
                var widthRatio;
                var heightRatio;
                var ratio;
                widthRatio = $('#grow-container').width() / this.width;
                heightRatio = $('#grow-container').height() / this.height;
                if (widthRatio < heightRatio) {
                    ratio = widthRatio;
                }
                else {
                    ratio = heightRatio;
                }
                console.log(ratio);
                this.scale.setUserScale(ratio, ratio);
            };
            return Grow;
        })(Phaser.Game);
        Cosh.Grow = Grow;
        window.onload = function () {
            Cosh.baseURL = 'http://cosh.bnsstore.com/res/';
            Cosh.baseAudioURL = Cosh.baseURL + 'Audios/Grow/';
            Cosh.baseImageURL = Cosh.baseURL + 'Images/Grow/';
            Cosh.grow = new Grow();
            console.log('Grow Created');
            window.onresize = function () {
                Cosh.grow.resize();
            };
        };
    })(Cosh = SLouple.Cosh || (SLouple.Cosh = {}));
})(SLouple || (SLouple = {}));
//# sourceMappingURL=Grow.js.map