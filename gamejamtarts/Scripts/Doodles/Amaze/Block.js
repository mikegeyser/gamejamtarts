function Block(image, x, y, rect) {
    this.initialize(image, x, y, rect);
}
Block.prototype = new Bitmap();
Block.prototype.base_initialize = Block.prototype.initialize;
Block.prototype.initialize = function (image, x, y, rect) {
    this.base_initialize(image);
    this.x = x;
    this.y = y;
    this.sourceRect = rect;
};
Block.prototype.update = function () {

};
Block.prototype.Box = function () {
    return new Box(this.x, this.y, this.sourceRect.width, this.sourceRect.height);
};