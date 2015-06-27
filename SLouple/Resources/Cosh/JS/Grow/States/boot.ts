module SLouple.Cosh {
    export class Boot extends Phaser.State {
        preload() {
            console.log('Boot');

            this.stage.setBackgroundColor('rgb(6,8,12)');
            
            this.input.maxPointers = 1;
            this.stage.disableVisibilityChange = true;

            this.scale.scaleMode = Phaser.ScaleManager.USER_SCALE;
            this.scale.fullScreenScaleMode = Phaser.ScaleManager.USER_SCALE;
            $(window).trigger('resize');


            this.load.image('Player', SLouple.Cosh.baseImageURL + 'Dandelion/Dandelion_100px/png');
        }

        create() {
            this.game.state.start('TitleScreen');
        }
    }
}  