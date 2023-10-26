using System.Collections.Generic;
using UnityEngine;

namespace UndoSystem {
    public class PlayerMovementAction : PlayerAction {
        private readonly Vector2Int positionBeforeMovement;
        private readonly IReadOnlyList<Vector2Int> boxesPositionBeforeMovement;

        public PlayerMovementAction(Vector2Int positionBeforeMovement,
            IReadOnlyList<Vector2Int> boxesPositionBeforeMovement) {
            this.positionBeforeMovement = positionBeforeMovement;
            this.boxesPositionBeforeMovement = boxesPositionBeforeMovement;
        }
        
        public override void Undo() {
            BoardManager.instance.UpdateEntitiesPositions(positionBeforeMovement, boxesPositionBeforeMovement);
        }
    }
}