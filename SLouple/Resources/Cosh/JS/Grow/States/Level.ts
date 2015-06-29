module SLouple.Cosh {
    export class Level extends Phaser.State {

        levelName: string;
        player: Player;

        constructor() {
            super();
        }

        preload() {
            console.log(this.levelName);
        }

        create() {
            this.game.world.setBounds(0, 0, 5000, 1080);
            this.player = new Player(this.game);
            this.game.add.existing(this.player);
            console.log("Added Player");

            this.game.camera.follow(this.player);
        }

        update() {
        }

        render() {
            this.game.debug.cameraInfo(this.game.camera, 32, 32);
            this.game.debug.spriteCoords(this.player, 32, 500);
        }
    }
}