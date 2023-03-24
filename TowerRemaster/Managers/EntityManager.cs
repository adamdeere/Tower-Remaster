using Assimp;
using TowerRemaster.Components.Interfaces;
using TowerRemaster.Components;
using TowerRemaster.GameObjects;
using TowerRemaster.GameObjects.Lights;
using TowerRemaster.Utility;

namespace TowerRemaster.Managers
{
    internal class EntityManager
    {
        private readonly List<Entity> m_EntityList;
        private readonly List<ILightObject> m_LightsList;
      

        public EntityManager()
        {
            m_LightsList = new List<ILightObject>();
            m_EntityList = new List<Entity>();
        }

        public void AddEntity(Entity entity)
        {
            //Entity result = FindEntity(entity.Name);
            // Debug.Assert(result != null, "Entity '" + entity.Name + "' already exists");
            m_EntityList.Add(entity);
        }

        public List<Entity> Entities()
        {
            return m_EntityList;
        }

        public Entity FindEntity(string name)
        {
            Entity? entity = m_EntityList.Find(delegate (Entity e)
            {
                return e.Name == name;
            });
            return entity;
        }

        public void AddLight(ILightObject light)
        {
            m_LightsList.Add(light);
        }

        public List<ILightObject> Lights()
        {
            return m_LightsList;
        }

        public void OnLightsAction(Shader shader)
        {
            Entity cameraEnt = FindEntity("MainCam");

            if (cameraEnt != null)
            {
                if (cameraEnt.FindComponent(ComponentTypes.COMPONENT_CAMERA) is ComponentCamera cam)
                {
                    CameraObject camera = cam.CameraObject;
                    foreach (var light in m_LightsList)
                    {
                        light.SetLights(shader, camera);
                    }
                }
            }
            
        }
    }
}