function Pulse(x, y, width, height, left, right) {
    this.initialize(x, y, width, height, left, right);
}
Pulse.prototype = new Shape();
Pulse.prototype.base_initialize = Pulse.prototype.initialize;
Pulse.prototype.initialize = function (x, y, width, height, left, right) {
    this.x = x;
    this.y = y;
    this.left = left;
    this.right = right;
    this.vx = 3;
    this.width = width;
    this.height = height;
};

Pulse.prototype.update = function () {
    this.x += this.vx;

    if (this.x < this.left || this.x + this.width > this.right)
        this.vx *= -1;

    // Create 'path'
    var path = [{ x: this.x, y: this.y + 20 }];

    var dx = 0;
    while (dx < this.width) {
        dx += Math.random() * 15;
        dy = Math.random() * this.height;

        path.push({ x: dx, y: dy });
    }

    path.push({ x: dx, y: this.y });

    var g = this.graphics;
    g.clear();

    $([
        { colour: "rgba(81,193,204,0.2)", stroke: 8 },
        { colour: "rgba(81,193,204,0.4)", stroke: 5 },
        { colour: "rgba(81,193,204,1)", stroke: 2 }
    ]).each(function () {
        g.beginStroke(this.colour).setStrokeStyle(this.stroke);

        g.moveTo(path[0].x, path[0].y);

        for (var i = 1; i < path.length; i++) {
            g.lineTo(path[i].x, path[i].y);
        }
    });
};

Pulse.prototype.Box = function () {
    return new Box(this.x, this.y, this.width, this.height);
};