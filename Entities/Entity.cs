using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomProjectRPG.Core.Utilities;
using CustomProjectRPG.Interfaces;
using CustomProjectRPG.Core;


namespace CustomProjectRPG.Entities
{

    public abstract class Entity : ICollidable, IDraw, IUpdate
    {
        // Private fields for encapsulation.
        private string _name;
        private string _description;
        private Vector2D _position;
        private Hitbox _hitbox;

        // Constructor for the Entity class.
        // It initializes the name, description, position, and hitbox.
        protected Entity(string name, string description, Vector2D startPosition, Vector2D hitboxSize)
        {
            _name = name;
            _description = description;
            _position = startPosition;
            _hitbox = new Hitbox(startPosition, hitboxSize);
        }

        // Public properties for controlled access to the private fields.
        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public Vector2D Position
        {
            get { return _position; }
            set
            {
                _position = value;
                _hitbox.Position = value;
            }
        }

        public Hitbox Hitbox
        {
            get { return _hitbox; }
        }

        // Abstract methods to be implemented by derived classes.
        public virtual bool IsCollidingWith(ICollidable other)
        
        {
                return this.Hitbox.Intersects(other.Hitbox);
        }
        
        public abstract void OnCollision(ICollidable other);
        public abstract void Draw();
        public abstract void Update();


        // Moves the entity to a new position.

        public virtual void MoveTo(float x, float y)
        {
            this.Position = new Vector2D(x, y);
        }
    }

}
