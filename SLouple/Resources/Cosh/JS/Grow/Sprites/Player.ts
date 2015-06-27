module SLouple.Cosh {
    export class Player extends Phaser.Sprite {
        xSpeed: number;
        ySpeed: number;
        maxSpeed: number;
        maxSpeedRadius: number;

        maxAngle: number;
        
        constructor(game) {
            this.maxSpeed = 10;
            this.maxSpeedRadius = 900;
            this.maxAngle = 45;
            super(game, 0, 0, 'Player');
            this.anchor.set(0.5, 0.5);
        }



        update() {
            var xDistance = this.game.input.x - this.x;
            var yDistance = this.game.input.y - this.y;

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
        }
    }
}