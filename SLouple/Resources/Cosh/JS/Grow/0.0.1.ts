module SLouple.Cosh {
    declare var grow: Grow;
    export class Grow {
        game: Phaser.Game;

        player;
        platforms;
        cursors;
        jumpButton;

        constructor() {
            console.log("Started");
            this.game = new Phaser.Game(800, 600, Phaser.AUTO, 'grow-container', {
                preload: this.preload,
                create: this.create,
                update: this.update,
                render: this.render
            });
            
        }

        preload() {
            this.game.stage.backgroundColor = '#222222';

            this.game.load.baseURL = 'http://examples.phaser.io/assets/';
            this.game.load.crossOrigin = 'anonymous';

            this.game.load.image('player', 'sprites/phaser-dude.png');
            this.game.load.image('platform', 'sprites/platform.png');
        }

        create() {
            this.game.scale.scaleMode = Phaser.ScaleManager.USER_SCALE;
            this.player = this.game.add.sprite(100, 200, 'player');

            this.game.physics.arcade.enable(this.player);

            this.player.body.collideWorldBounds = true;
            this.player.body.gravity.y = 500;

            this.platforms = this.game.add.physicsGroup(0);

            this.platforms.create(500, 150, 'platform');
            this.platforms.create(-200, 300, 'platform');
            this.platforms.create(400, 450, 'platform');

            this.platforms.setAll('body.immovable', true);

            this.cursors = this.game.input.keyboard.createCursorKeys();
            this.jumpButton = this.game.input.keyboard.addKey(Phaser.Keyboard.SPACEBAR);

            
        }

        update() {
            if (this.game.scale.isFullScreen) {
                this.game.canvas.width = $("#grow-container").width();
                this.game.canvas.height = $("#grow-container").height();
            } else {
                var widthRatio;
                var heightRatio;
                widthRatio = $("#grow-container").width() / this.game.width;
                heightRatio = $("#grow-container").height() / this.game.height;
                console.log(widthRatio + " " + heightRatio);
                if (widthRatio < heightRatio) {
                    this.game.scale.setUserScale(widthRatio, widthRatio);
                } else {
                    console.log("height");
                    this.game.scale.setUserScale(heightRatio, heightRatio);
                }
            }
            

            this.game.physics.arcade.collide(this.player, this.platforms);

            this.player.body.velocity.x = 0;

            if (this.cursors.left.isDown) {
                this.player.body.velocity.x = -250;
            }
            else if (this.cursors.right.isDown) {
                this.player.body.velocity.x = 250;
            }

            if (this.jumpButton.isDown && (this.player.body.onFloor() || this.player.body.touching.down)) {
                this.player.body.velocity.y = -400;
            }
        }

        render() {
        }
    }

    window.onload = () => {
        grow = new Grow();
    };
}