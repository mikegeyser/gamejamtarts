function Player(image, x, y) {
    this.initialize(image, x, y);
}

Player.prototype = new BitmapAnimation();
Player.prototype.base_initialize = Player.prototype.initialize;

Player.prototype.initialize = function (image, x, y) {
    this.x = x;
    this.y = y;
    this.vy = 10;
    this.direction = 0; // 1 = right & -1 = left & 0 = idle
    
    var localSpriteSheet = new SpriteSheet({
        images: [image], //image to use
        frames: { width: 64, height: 64, regX: 32, regY: 64 },
        animations: {
            walk: [0, 9, "walk", 4],
            die: [10, 21, false, 4],
            jump: [22, 32, "walk", 4],
            celebrate: [33, 43, false, 4],
            idle: [44, 44]
        }
    });

    SpriteSheetUtils.addFlippedFrames(localSpriteSheet, true, false, false);

    this.base_initialize(localSpriteSheet);
    
    this.jump_time = 0;
    
    this.regX = this.spriteSheet.frameWidth / 2 | 0;
    this.regY = this.spriteSheet.frameHeight / 2 | 0;

    // starting directly at the first frame of the walk_right sequence
    this.currentFrame = 66;

    this.Reset(x, y);
};


Player.prototype.Reset = function (x, y) {
    this.x = x;
    this.y = y;
    this.IsAlive = true;
    this.gotoAndPlay("idle");
};

Player.prototype.update = function () {
    // Animation changes
    if (InputJump && !this.isJumping) {
        this.isJumping = true;
        this.vy = -10;
        switch (this.direction) {
            case 1:
                this.gotoAndPlay("jump_h");
                break;
            case -1:
                this.gotoAndPlay("jump");
                break;
        }
    } else if (this.direction != InputDirection) {
        this.direction = InputDirection;
        
        switch (this.direction) {
            case 1:
                this.gotoAndPlay("walk_h");
                break;
            case -1:
                this.gotoAndPlay("walk");
                break;
            default:
                this.gotoAndPlay("idle");
                break;
        }
    }

    
    this.x += this.direction * 5;
    this.y += this.vy;

    if (this.isJumping) {
        
        //if (this.vy++ >= 10) {
        //    this.isJumping = false;
        //}

     
    }
};

Player.prototype.Box = function () {
    return new Box(this.x, this.y, 64, 64);
};

Player.prototype.checkCollisions = function(pulses, boxes) {
    for (var i = 0; i < pulses.length; i++) {
        if (this.Box().intersects(pulses[i].Box()))
            this.IsAlive = false;       
    }

    for (var i = 0; i < boxes.length; i++) {
        var box = boxes[i];
        if (this.Box().intersects(box.Box())) {
            if (this.y > box.y)
                this.y = box.y + box.height;
            else {
                
            }
        }
    }

    this.Box().checkAllCollisions(pulses);
};

/// <summary>
/// Handles input, performs physics, and animates the player sprite.
/// </summary>
/// <remarks>
/// We pass in all of the input states so that our game is only polling the hardware
/// once per frame. We also pass the game's orientation because when using the accelerometer,
/// we need to reverse our motion when the orientation is in the LandscapeRight orientation.
/// </remarks>
Player.prototype.tick = function () {
    // It not possible to have a predictable tick/update time
    // requestAnimationFrame could help but is currently not widely and properly supported by browsers
    // this.elapsed = (Ticker.getTime() - this.lastUpdate) / 1000;
    // We're then forcing/simulating a perfect world
    this.elapsed = globalTargetFPS / 1000;

    this.ApplyPhysics();

    if (this.IsAlive && this.IsOnGround && !this.HasReachedExit) {
        if (Math.abs(this.velocity.x) - 0.02 > 0) {
            // Checking if we're not already playing the animation
            if (this.currentAnimation.indexOf("walk") === -1 && this.direction === -1) {
                this.gotoAndPlay("walk");
            }
            if (this.currentAnimation.indexOf("walk_h") === -1 && this.direction === 1) {
                this.gotoAndPlay("walk_h");
            }
        }
        else {
            if (this.currentAnimation.indexOf("idle") === -1 && this.direction === 0) {
                this.gotoAndPlay("idle");
            }
        }
    }

    // Clear input.
    this.isJumping = false;
};

/// <summary>
/// Updates the player's velocity and position based on input, gravity, etc.
/// </summary>
Player.prototype.ApplyPhysics = function () {
    if (this.IsAlive && !this.HasReachedExit) {
        var previousPosition = new Point(this.x, this.y);

        // Base velocity is a combination of horizontal movement control and
        // acceleration downward due to gravity.
        this.velocity.x += this.direction * MoveAcceleration * this.elapsed;
        this.velocity.y = Math.clamp(this.velocity.y + GravityAcceleration * this.elapsed, -MaxFallSpeed, MaxFallSpeed);

        this.velocity.y = this.DoJump(this.velocity.y);

        // Apply pseudo-drag horizontally.
        if (this.IsOnGround) {
            this.velocity.x *= GroundDragFactor;
        }
        else {
            this.velocity.x *= AirDragFactor;
        }

        // Prevent the player from running faster than his top speed.
        this.velocity.x = Math.clamp(this.velocity.x, -MaxMoveSpeed, MaxMoveSpeed);

        this.x += this.velocity.x * this.elapsed;
        this.y += this.velocity.y * this.elapsed;
        this.x = Math.round(this.x);
        this.y = Math.round(this.y);

        // If the player is now colliding with the level, separate them.
        this.HandleCollisions();

        // If the collision stopped us from moving, reset the velocity to zero.
        if (this.x === previousPosition.x) {
            this.velocity.x = 0;
        }

        if (this.y === previousPosition.y) {
            this.velocity.y = 0;
        }
    }
};

/// <summary>
/// Calculates the Y velocity accounting for jumping and
/// animates accordingly.
/// </summary>
/// <remarks>
/// During the accent of a jump, the Y velocity is completely
/// overridden by a power curve. During the decent, gravity takes
/// over. The jump velocity is controlled by the jumpTime field
/// which measures time into the accent of the current jump.
/// </remarks>
/// <param name="velocityY">
/// The player's current velocity along the Y axis.
/// </param>
/// <returns>
/// A new Y velocity if beginning or continuing a jump.
/// Otherwise, the existing Y velocity.
/// </returns>
Player.prototype.DoJump = function (velocityY) {
    // If the player wants to jump
    if (this.isJumping) {
        // Begin or continue a jump
        if ((!this.wasJumping && this.IsOnGround) || this.jumpTime > 0.0) {
            if (this.jumpTime == 0.0) {
                this.level.levelContentManager.playerJump.play();
            }

            this.jumpTime += this.elapsed;
            // Playing the proper animation based on
            // the current direction of our hero
            if (this.direction == 1) {
                this.gotoAndPlay("jump_h");
            }
            else {
                this.gotoAndPlay("jump");
            }
        }

        // If we are in the ascent of the jump
        if (0.0 < this.jumpTime && this.jumpTime <= MaxJumpTime) {
            // Fully override the vertical velocity with a power curve that gives players more control over the top of the jump
            velocityY = JumpLaunchVelocity * (1.0 - Math.pow(this.jumpTime / MaxJumpTime, JumpControlPower));
        }
        else {
            // Reached the apex of the jump
            this.jumpTime = 0.0;
        }
    }
    else {
        // Continues not jumping or cancels a jump in progress
        this.jumpTime = 0.0;
    }
    this.wasJumping = this.isJumping;

    return velocityY;
};

/// <summary>
/// Detects and resolves all collisions between the player and his neighboring
/// tiles. When a collision is detected, the player is pushed away along one
/// axis to prevent overlapping. There is some special logic for the Y axis to
/// handle platforms which behave differently depending on direction of movement.
/// </summary>
Player.prototype.HandleCollisions = function () {
    var bounds = this.BoundingRectangle();
    var leftTile = Math.floor(bounds.Left() / StaticTile.Width);
    var rightTile = Math.ceil((bounds.Right() / StaticTile.Width)) - 1;
    var topTile = Math.floor(bounds.Top() / StaticTile.Height);
    var bottomTile = Math.ceil((bounds.Bottom() / StaticTile.Height)) - 1;

    // Reset flag to search for ground collision.
    this.IsOnGround = false;

    // For each potentially colliding tile,
    for (var y = topTile; y <= bottomTile; ++y) {
        for (var x = leftTile; x <= rightTile; ++x) {
            // If this tile is collidable,
            var collision = this.level.GetCollision(x, y);
            if (collision !== Enum.TileCollision.Passable) {
                // Determine collision depth (with direction) and magnitude.
                var tileBounds = this.level.GetBounds(x, y);
                var depth = bounds.GetIntersectionDepth(tileBounds);
                if (depth.x !== 0 && depth.y !== 0) {
                    var absDepthX = Math.abs(depth.x);
                    var absDepthY = Math.abs(depth.y);

                    // Resolve the collision along the shallow axis.
                    if (absDepthY < absDepthX || collision == Enum.TileCollision.Platform) {
                        // If we crossed the top of a tile, we are on the ground.
                        if (this.previousBottom <= tileBounds.Top()) {
                            this.IsOnGround = true;
                        }

                        // Ignore platforms, unless we are on the ground.
                        if (collision == Enum.TileCollision.Impassable || this.IsOnGround) {
                            // Resolve the collision along the Y axis.
                            this.y = this.y + depth.y;

                            // Perform further collisions with the new bounds.
                            bounds = this.BoundingRectangle();
                        }
                    }
                    else if (collision == Enum.TileCollision.Impassable) // Ignore platforms.
                    {
                        // Resolve the collision along the X axis.
                        this.x = this.x + depth.x;

                        // Perform further collisions with the new bounds.
                        bounds = this.BoundingRectangle();
                    }
                }
            }
        }
    }

    // Save the new bounds bottom.
    this.previousBottom = bounds.Bottom();
};

/// <summary>
/// Called when the player has been killed.
/// </summary>
/// <param name="killedBy">
/// The enemy who killed the player. This parameter is null if the player was
/// not killed by an enemy (fell into a hole).
/// </param>
Player.prototype.OnKilled = function (killedBy) {
    this.IsAlive = false;
    this.velocity = new Point(0, 0);

    // Playing the proper animation based on
    // the current direction of our hero
    if (this.direction === 1) {
        this.gotoAndPlay("die_h");
    }
    else {
        this.gotoAndPlay("die");
    }

    if (killedBy !== null && killedBy !== undefined) {
        this.level.levelContentManager.playerKilled.play();
    }
    else {
        this.level.levelContentManager.playerFall.play();
    }
};

/// <summary>
/// Called when this player reaches the level's exit.
/// </summary>
Player.prototype.OnReachedExit = function () {
    this.HasReachedExit = true;
    this.level.levelContentManager.exitReached.play();

    // Playing the proper animation based on
    // the current direction of our hero
    if (this.direction === 1) {
        this.gotoAndPlay("celebrate_h");
    }
    else {
        this.gotoAndPlay("celebrate");
    }
};
