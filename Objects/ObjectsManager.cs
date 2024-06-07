using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Thriple.Objects
{
    public class ObjectsManager : IGameObject
    {
        private double TimeStep {  get; set; }

        private List<IGameObject> _objects;
        private List<IGameObject> _objectsToAdd;
        private List<IGameObject> _objectsToDel;

        private double _currentTime = 0;
        public IEnumerable<IGameObject> Objects => new ReadOnlyCollection<IGameObject>(_objects);

        public ObjectsManager() 
        {
            TimeStep = 1;
            _objects = new List<IGameObject>();
            _objectsToAdd = new List<IGameObject>();
            _objectsToDel = new List<IGameObject>();
        }

        public int DrawOrder => 10;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var obj in _objects.OrderBy(obj => obj.DrawOrder))
            {
                obj.Draw(spriteBatch, gameTime);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_currentTime > TimeStep)
            {
                _currentTime -= TimeStep;
                if (TimeStep > 1/3d)
                    TimeStep -= 1 / 200d;
                foreach (var obj in _objects)
                {
                    obj.Update(gameTime);
                }
            }
            else
            {
                foreach (var obj in _objects.Where(obj => obj.GetType().GetInterface("IInstantObject") != null))
                {
                    ((IInstantObject)obj).InstantUpdate(gameTime);
                }
            }
            foreach (var obj in _objectsToAdd)
            {
                _objects.Add(obj);
            }
            foreach (var obj in _objectsToDel)
            {
                _objects.Remove(obj);
            }
            _objectsToAdd.Clear();
            _objectsToDel.Clear();
            _currentTime += gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void AddObject(params IGameObject[] objects)
        {
            foreach (var obj in objects)
            {
                if (obj is null)
                    throw new NullReferenceException(nameof(obj));
                _objectsToAdd.Add(obj);
            }
        }

        public void DeleteObject(IGameObject obj)
        {
            _objectsToDel.Add(obj);
        }

        public void Clear()
        {
            _objects.Clear();
            _objectsToAdd.Clear();
            _objectsToDel.Clear();
        }
    }
}