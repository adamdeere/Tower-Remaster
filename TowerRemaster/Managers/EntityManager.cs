using TowerRemaster.Components;
using TowerRemaster.Components.Interfaces;
using TowerRemaster.GameObjects;
using TowerRemaster.GameObjects.Lights;
using TowerRemaster.GameObjects.Objects;
using TowerRemaster.Utility;

namespace TowerRemaster.Managers
{
    internal class EntityManager
    {
        private readonly List<GameObject> m_EntityList;
        private readonly List<ILightObject> m_LightsList;

        public EntityManager()
        {
            m_LightsList = new List<ILightObject>();
            m_EntityList = new List<GameObject>();
        }

        public void AddEntity(GameObject entity)
        {
            //Entity result = FindEntity(entity.Name);
            // Debug.Assert(result != null, "Entity '" + entity.Name + "' already exists");
            m_EntityList.Add(entity);
        }

        public List<GameObject> Entities()
        {
            return m_EntityList;
        }

        public GameObject FindEntity(string name)
        {
            GameObject? entity = m_EntityList.Find(delegate (GameObject e)
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
            GameObject cameraEnt = FindEntity("MainCam");

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