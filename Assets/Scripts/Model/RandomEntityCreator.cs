﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhacAMole.Infrastructure;
using Random = UnityEngine.Random;

namespace WhacAMole.Model
{
    public class RandomEntityCreator : MonoBehaviour
    {
        [SerializeField] private List<EntitySpawnFrequency> _entityTemplates;
        private Pool<Entity> _entityPool;

        public void Init()
        {
            CalculateAbsoluteSpawnFrequencies();
            _entityPool = new Pool<Entity>(transform);
        }

        public Entity GetRandomEntity(Transform parent)
        {
            Entity entity = null;
            float spawn = Random.Range(0f, 1f);
            float sumSpawnFrequencies = 0f;

            foreach (var entityTemplate in _entityTemplates)
            {
                sumSpawnFrequencies += entityTemplate.AbsoluteFrequency;

                if ((spawn <= sumSpawnFrequencies))
                {
                    entity = entityTemplate.Entity;
                    break;
                }
            }

            return _entityPool.Get(entity, parent);
        }

        public void RemoveEntity(Entity entity) => _entityPool.Return(entity);

        private void CalculateAbsoluteSpawnFrequencies()
        {
            float sumRelativeFrequencies = _entityTemplates.Select(entityTemplate => entityTemplate.RelativeFrequency).Sum();
            _entityTemplates.ForEach(entityTemplate => entityTemplate.Init(entityTemplate.RelativeFrequency / sumRelativeFrequencies));
        }

        [Serializable]
        private class EntitySpawnFrequency
        {
            [SerializeField] private Entity _entity;
            [Range(0f, 1f)]
            [SerializeField] private float _relativeFrequency;
            private float _absoluteFrequency;

            public Entity Entity => _entity;
            public float RelativeFrequency => _relativeFrequency;
            public float AbsoluteFrequency => _absoluteFrequency;

            public void Init(float absoluteFrequency) => _absoluteFrequency = absoluteFrequency;
        }
    }
}
