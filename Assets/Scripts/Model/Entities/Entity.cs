﻿using UnityEngine;

namespace WhacAMole.Model
{
    public class Entity : MonoBehaviour, INameable
    {
        [SerializeField] private EntityDeltasSet _deltasSet;
        [SerializeField] private Type _type;
        private GameState _gameState;
        private RandomEntityCreator _entityCreator;

        private enum Type { Mole, AntiMole, Leprechaun }

        public string Name => _type.ToString();

        public void Init(RandomEntityCreator entityCreator, GameState gameState)
        {
            _gameState = gameState;
            _entityCreator = entityCreator;
        }

        public void Hit()
        {
            _gameState.Change(_deltasSet.OnHitDeltas);
            _entityCreator.RemoveEntity(this);
        }

        public void Hiding()
        {
            _gameState.Change(_deltasSet.OnHidingDeltas);
            _entityCreator.RemoveEntity(this);
        }
    }
}
