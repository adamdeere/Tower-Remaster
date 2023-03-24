namespace TowerRemaster.Components.Interfaces
{
    internal enum ComponentTypes
    {
        COMPONENT_NONE = 0,
        COMPONENT_TRANSFORM = 1 << 0,
        COMPONENT_MODEL = 1 << 1,
        COMPONENT_PHYSICS = 1 << 2,
        COMPONENT_COLISION = 1 << 3,
        COMPONENT_INPUT = 1 << 4,
        COMPONENT_MATERIAL = 1 << 5,
        COMPONENT_CAMERA = 1 << 6,
        COMPONENT_PBR_MATERIAL = 1 << 7,
    }

    internal interface IComponent
    {
        ComponentTypes ComponentType
        {
            get;
        }
    }
}