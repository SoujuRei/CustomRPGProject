using CustomProjectRPG.Entities;
using CustomProjectRPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProjectRPG.Manager
{
    public class CollisionManager
    {
        private readonly List<ICollidable> _collidables;

        public CollisionManager()
        {
            _collidables = new List<ICollidable>();
        }

        public void AddCollidable(ICollidable collidable)
        {
            if (collidable == null)
            {
                throw new System.ArgumentNullException(nameof(collidable));
            }
            if (!_collidables.Contains(collidable))
            {
                _collidables.Add(collidable);
            }
        }

        public void RemoveCollidable(ICollidable collidable)
        {
            if (collidable == null)
            {
                throw new System.ArgumentNullException(nameof(collidable));
            }
            _collidables.Remove(collidable);
        }

        public void CheckCollisions()
        {
            // Use a nested loop to check pairwise collisions
            for (int i = 0; i < _collidables.Count; i++)
            {
                for (int j = i + 1; j < _collidables.Count; j++)
                {
                    ICollidable first = _collidables[i];
                    ICollidable second = _collidables[j];

                    if (first.Hitbox.Intersects(second.Hitbox))
                    {
                        // Notify both entities of the collision
                        if (first is Entity entity1)
                        {
                            entity1.OnCollision(second);
                        }
                        if (second is Entity entity2)
                        {
                            entity2.OnCollision(first);
                        }
                    }
                }
            }
        }

        public void Clear()
        {
            _collidables.Clear();
        }
    }
}
