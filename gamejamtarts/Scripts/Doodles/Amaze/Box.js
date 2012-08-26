function Box(x, y, width, height) {
    this.x = x;
    this.y = y;
    this.Width = width;
    this.Height = height;
    this.Inverted = false;
}

Box.prototype.invert = function () { this.Invert = true; };
Box.prototype.intersects = function (other) {
    if (this.Inverted || other.Inverted) // Containing bounding box, collisions would be internal to it.
        return (this.X >= other.X + other.Width) || (this.Y >= other.Y + other.Height)
            || (this.X + this.Width <= other.X) || (this.Y + this.Height <= other.Y);

    // Regular, contained bounding box.
    return (this.X <= other.X + other.Width) && (this.Y <= other.Y + other.Height)
        && (this.X + this.Width >= other.X) && (this.Y + this.Height >= other.Y);
};

Box.prototype.checkCollision = function (other) {
    if (this.intersects(other)) {
        this.collide();
        other.collide();
    }
};

Box.prototype.checkAllCollisions = function (other) {
    var box = this;
    $(other).each(function() {
        this.Box().checkCollision(box);
    });
};