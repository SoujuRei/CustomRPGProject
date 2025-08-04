using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomProjectRPG.Entities;
using CustomProjectRPG.Interfaces;
using CustomProjectRPG.Manager;
using CustomProjectRPG.Core.Utilities;
using CustomProjectRPG.UI;



namespace CustomProjectRPG.World
{
    public class Area
    {
        private readonly string _name;
        private readonly Vector2D _size; // Area dimensions (width, height)
        private readonly List<Entity> _entities;
        private readonly List<ICollidable> _staticAssets;
        private readonly Dictionary<string, Area> _subAreas;
        private readonly CollisionManager _collisionManager;
        private bool _isLocked;
        private string _unlockCondition; // E.g., knowledge clue ID

        public string Name => _name;
        public Vector2D Size => _size;
        public IReadOnlyList<Entity> Entities => _entities.AsReadOnly();
        public IReadOnlyList<ICollidable> StaticAssets => _staticAssets.AsReadOnly();
        public IReadOnlyDictionary<string, Area> SubAreas => _subAreas.AsReadOnly();
        public bool IsLocked => _isLocked;

        public Area(string name, Vector2D size, CollisionManager collisionManager)
        {
            _name = name;
            _size = size;
            _entities = new List<Entity>();
            _staticAssets = new List<ICollidable>();
            _subAreas = new Dictionary<string, Area>();
            _collisionManager = collisionManager;
            _isLocked = false;
            _unlockCondition = null;
        }

        public Area(string name, Vector2D size, CollisionManager collisionManager, string unlockCondition)
            : this(name, size, collisionManager)
        {
            _isLocked = true;
            _unlockCondition = unlockCondition;
        }

        public void AddEntity(Entity entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }
            if (!_entities.Contains(entity))
            {
                _entities.Add(entity);
                _collisionManager.AddCollidable(entity);
            }
        }

        public void RemoveEntity(Entity entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }
            if (_entities.Remove(entity))
            {
                _collisionManager.RemoveCollidable(entity);
            }
        }

        public void AddStaticAsset(ICollidable asset)
        {
            if (asset == null)
            {
                throw new System.ArgumentNullException(nameof(asset));
            }
            if (!_staticAssets.Contains(asset))
            {
                _staticAssets.Add(asset);
                _collisionManager.AddCollidable(asset);
            }
        }

        public void RemoveStaticAsset(ICollidable asset)
        {
            if (asset == null)
            {
                throw new System.ArgumentNullException(nameof(asset));
            }
            if (_staticAssets.Remove(asset))
            {
                _collisionManager.RemoveCollidable(asset);
            }
        }

        public void AddSubArea(string subAreaName, Area subArea)
        {
            if (subArea == null)
            {
                throw new System.ArgumentNullException(nameof(subArea));
            }
            if (!_subAreas.ContainsKey(subAreaName))
            {
                _subAreas.Add(subAreaName, subArea);
            }
        }

        public void RemoveSubArea(string subAreaName)
        {
            _subAreas.Remove(subAreaName);
        }

        public bool TryUnlock(string knowledgeClue)
        {
            if (_isLocked && _unlockCondition == knowledgeClue)
            {
                _isLocked = false;
                return true;
            }
            return false;
        }

        public void Update()
        {
            foreach (var entity in _entities)
            {
                entity.Update();
            }
            _collisionManager.CheckCollisions();
        }

        public void Draw()
        {
            foreach (var entity in _entities)
            {
                entity.Draw();
            }
            // Draw static assets (placeholder for SplashKit rendering)
            foreach (var asset in _staticAssets)
            {
                if (asset is StaticAsset staticAsset)
                {
                    staticAsset.Draw();
                }
            }
        }

        public bool IsWithinBounds(Vector2D position)
        {
            return position.X >= 0 && position.X < _size.X && position.Y >= 0 && position.Y < _size.Y;
        }
    }
}