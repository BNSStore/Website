﻿module SLouple.Cosh {
    export class Boot extends Phaser.State {
        preload() {
            console.log('Boot');
            this.game.state.start('TitleScreen');
            this.scale.scaleMode = Phaser.ScaleManager.USER_SCALE;
            $(window).trigger('resize');
        }
    }
}  