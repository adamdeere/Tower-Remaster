﻿using OpenTK.Windowing.GraphicsLibraryFramework;
using TowerRemaster.Managers;
using TowerRemaster.Systems.Interfaces;

namespace TowerRemaster.Systems.InputSystems
{
    internal class SystemInput : IInputSystems
    {
        public string Name => "SystemInput";

        public void OnAction(EntityManager entityManager, KeyboardState state)
        {
        }
    }
}