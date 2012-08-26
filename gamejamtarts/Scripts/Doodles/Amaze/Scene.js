function Scene(stage, width, height) {
    this.Box = new Box(0, 0, width, height);
    this.Stage = stage;
    this.Blocks = [];
    this.Players = [];
    this.Pulses = [];
    
    var players = this.Players;
    var blocks = this.Blocks;
    
    // Load the amaze blocks
    var amaze_image = new Image();
    amaze_image.onload = function () {

        var amaze = new SpriteSheet({
            images: [amaze_image],
            frames: [// x, y, width, height, image index, regX, regY
                     [0, 0, 89, 110, 0, 44, 55],
                     [89, 0, 100, 110, 0, 44, 55],
                     [189, 0, 79, 110, 0, 44, 55],
                     [268, 0, 80, 110, 0, 44, 55],
                     [348, 0, 92, 110, 0, 44, 55]]
        });

        var x = 190, y = 140;
        for (var i = 0; i < 5; i++) {

            var frame = amaze.getFrame(i);
            var block = new Block(frame.image, x + frame.rect.x, y, frame.rect);
            blocks.push(block);
            stage.addChild(block);
        }
    };
    amaze_image.src = amaze_image_url;

    /*
    var player_image = new Image();
    player_image.onload = function () {
        var player = new Player(player_image, 250, 140);
        players.push(player);
        stage.addChild(player);
    };
    player_image.src = player_image_url;
    */

    var pulse = new Pulse(0, height - 20, 50, 25, 0, width);
    this.Pulses.push(pulse);
    stage.addChild(pulse);
}
Scene.prototype.draw = function () {
    var players = this.Players;
    var pulses = this.Pulses;
    var blocks = this.Blocks;
    
    $(pulses).each(function() {
        this.update();
    });

    $(blocks).each(function () {
        this.update();
    });

    /*
    $(players).each(function () {
        this.update();
        this.checkCollisions(pulses, blocks);
    });
    */
    this.Stage.update();
};

Pulse.prototype.Box = function () {
    return new Box(this.x, this.y, this.width, this.height).Invert();
};