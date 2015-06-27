module SLouple.Cosh {
    export class Level1 extends Level {

        constructor() {
            this.levelName = "Level 1";
            super();
        }

        preload() {
            super.preload();
        }

        create() {
            super.create();

            var bmd = this.add.bitmapData(1920, 1080);

            bmd.ctx.beginPath();
            bmd.ctx.rect(0, 0, 200, 200);
            bmd.ctx.fillStyle = "rgb(255,255,255)";
            bmd.ctx.fill();
            this.add.sprite(500, 300, bmd);
            console.log("Added block");
        }

        update() {
            super.update();
        }
    }
} 