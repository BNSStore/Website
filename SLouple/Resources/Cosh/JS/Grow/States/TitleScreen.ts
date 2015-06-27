module SLouple.Cosh {
    export class TitleScreen extends Phaser.State {
        sky: Phaser.Sprite;
        stars: Phaser.Sprite;
        tree: Phaser.Sprite;
        title: Phaser.Sprite;
        offset: number;
        offsetSwitch: boolean;

        starsSwitch: boolean;

        cover: Phaser.Sprite;
        coverSwitch: boolean;

        beginSwitch: boolean;
        bgm: Phaser.Sound;

        preload() {
            console.log('TitleScreen');
            this.load.image('TitleScreen_Sky', SLouple.Cosh.baseImageURL + 'TitleScreen/Sky/png');
            this.load.image('TitleScreen_Stars', SLouple.Cosh.baseImageURL + 'TitleScreen/Stars/png');
            this.load.image('TitleScreen_Tree', SLouple.Cosh.baseImageURL + 'TitleScreen/Tree/png');
            this.load.image('TitleScreen_Title', SLouple.Cosh.baseImageURL + 'TitleScreen/Title/png');
            this.load.audio('TitleScreenBGM', [
                SLouple.Cosh.baseAudioURL + 'TitleScreenBGM/ogg',
                SLouple.Cosh.baseAudioURL + 'TitleScreenBGM/mp3'
            ]);
        }

        create() {
            this.sky = this.game.add.sprite(0, 0, 'TitleScreen_Sky');
            this.sky.inputEnabled = true;
            this.sky.events.onInputDown.add(this.begin, this);

            this.stars = this.game.add.sprite(0, 0, 'TitleScreen_Stars');

            this.tree = this.game.add.sprite(0, 0, 'TitleScreen_Tree');

            this.title = this.game.add.sprite(0, 0, 'TitleScreen_Title');

            this.bgm = this.game.add.audio('TitleScreenBGM');
            this.bgm.loop = true;
            this.bgm.volume = 1;
            this.bgm.play();

            var bmd = this.add.bitmapData(1920, 1080);

            bmd.ctx.beginPath();
            bmd.ctx.rect(0, 0, 1920, 1080);
            bmd.ctx.fillStyle = "rgb(6,8,12)";
            bmd.ctx.fill();
            this.cover = this.add.sprite(0, 0, bmd);
            this.cover.anchor.setTo(0, 0);
            this.sky.height *= 1.05;
            this.sky.width *= 1.05;
            this.stars.height *= 1.05;
            this.stars.width *= 1.05;
            this.tree.height *= 1.05;
            this.tree.width *= 1.05;

            this.offset = 0;
        }

        update() {
            if (this.starsSwitch) {
                this.stars.alpha += 0.01;
            } else {
                this.stars.alpha -= 0.01;
            }
            if (this.stars.alpha < 0) {
                this.stars.alpha = 0;
                this.starsSwitch = true;
            } else if (this.stars.alpha > 1) {
                this.stars.alpha = 1;
                this.starsSwitch = false;
            }

            if (this.coverSwitch ) {
                this.cover.alpha += 0.01;
            } else if (!this.coverSwitch && this.bgm.isPlaying) {
                this.cover.alpha -= 0.01;
            }
            if (this.cover.alpha < 0) {
                this.cover.alpha = 0;
            } else if (this.cover.alpha > 1) {
                this.cover.alpha = 1;
            }
            if (this.bgm.volume < 0) {
                this.bgm.volume = 0;
            } else if (this.bgm.volume > 1) {
                this.bgm.volume = 1;
            }

            
            this.title.alpha =  1 - (this.stars.alpha / 3 * 2);

            if (this.beginSwitch) {
                var speed = 5;
                this.tree.y -= speed;
                this.title.y -= speed;
                this.sky.y -= speed / 2;
                this.sky.height -= speed / 2;
                this.stars.y -= speed / 2;
                this.stars.height -= speed / 2;
                this.title.y -= speed / 4;
                this.bgm.volume -= speed / 1000;

                if (this.tree.y < -1200) {
                    this.bgm.stop();
                    this.game.state.start("Level1");
                }
            } else {
                if (this.offsetSwitch) {
                    this.offset += 0.5;
                } else {
                    this.offset -= 0.5;
                }
                if (this.offset > 0) {
                    this.offset = 0;
                    this.offsetSwitch = false;
                } else if (this.offset < -100) {
                    this.offset = -100;
                    this.offsetSwitch = true;
                }
                var realOffset = (-1.0 / 2500.0 * Math.pow(this.offset + 50.0, 2)) + 1.0;

                if (!this.offsetSwitch) {
                    realOffset = -realOffset
                }
                this.sky.x += realOffset / 2;
                this.stars.x = this.sky.x;
                this.tree.x += realOffset / 4;
            }
        }

        begin() {
            if (this.bgm.isPlaying) {
                this.beginSwitch = true;
            }
        }
    }
} 