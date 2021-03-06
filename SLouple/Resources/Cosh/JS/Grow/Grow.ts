﻿
module SLouple.Cosh {
    export declare var grow: Grow;
    export declare var baseURL: string;
    export declare var baseAudioURL: string;
    export declare var baseImageURL: string;
    export class Grow extends Phaser.Game {
        fps: number;


        constructor() {
            //super(864, 486, Phaser.AUTO, 'grow-container');
            super(1920, 1080, Phaser.AUTO, 'grow-container');
            this.state.add('Boot', Boot, false);
            this.state.add('TitleScreen', TitleScreen, false);
            this.state.add('Level1', Level1, false);
            this.state.start('Boot');
        }
        
        render() {
            this.fps++;
        }
        reportFPS() {
            this.fps = 0;
        }
        resize() {
            console.log('Resizing');
            var widthRatio: number;
            var heightRatio: number;
            var ratio: number;
           
            var container = $('#grow-container');
            if (container.parent().height() < 600) {
                container.css("height", "100%");
                container.css("top", "0");
            } else {
                container.css("height", "80%");
                container.css("top", "10%");
            }
            widthRatio = container.width() / this.width;
            heightRatio = container.height() / this.height;
            if (widthRatio < heightRatio) {
                ratio = widthRatio;
            } else {
                ratio = heightRatio;
            }
            console.log(ratio);
            this.scale.setUserScale(ratio, ratio);
        }
    }
    window.onload = () => {
        baseURL = 'http://cosh.bnsstore.com/res/';
        baseAudioURL = baseURL + 'Audios/Grow/';
        baseImageURL = baseURL + 'Images/Grow/';
        grow = new Grow();
        console.log('Grow Created');
        window.onresize = () => {
            grow.resize();
        };

        /*
        $('#grow-container').click(function () {
            var canvas = $("#grow-container").get()[0];
            canvas.requestPointerLock = canvas.requestPointerLock ||
            canvas.mozRequestPointerLock ||
            canvas.webkitRequestPointerLock;

            canvas.requestPointerLock();
        });
        */

    }
}