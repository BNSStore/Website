module SLouple.Cosh {
    export class TitleScreen extends Phaser.State {
        background;
        backgroundStars;
        backgroundStarsOpacity: number;
        backgroundStarsSwitch: boolean;

        preload() {
            console.log('TitleScreen');
            this.load.image('TitleScreen', SLouple.Cosh.baseImageURL + 'TitleScreen/png');
            this.load.image('TitleScreenStars', SLouple.Cosh.baseImageURL + 'TitleScreenStars/png');
        }
        create() {
            this.background = this.game.add.sprite(0, 0, 'TitleScreen');
            this.backgroundStars = this.game.add.sprite(0, 0, 'TitleScreenStars');
            this.backgroundStarsOpacity = 0;
        }
        update() {
            this.background.height = this.game.height;
            this.background.width = this.game.width;
            this.backgroundStars.height = this.game.height;
            this.backgroundStars.width = this.game.width;
            if (this.backgroundStarsSwitch) {
                this.backgroundStarsOpacity += 0.01;
            } else {
                this.backgroundStarsOpacity -= 0.01;
            }
            if (this.backgroundStarsOpacity < 0) {
                this.backgroundStarsOpacity = 0;
                this.backgroundStarsSwitch = true;
            } else if (this.backgroundStarsOpacity > 1) {
                this.backgroundStarsOpacity = 1;
                this.backgroundStarsSwitch = false;
            }

            this.backgroundStars.alpha = this.backgroundStarsOpacity;
        }
    }
} 