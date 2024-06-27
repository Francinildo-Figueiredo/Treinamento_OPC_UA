using Opc.Ua;
using Opc.Ua.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EvaporadorPlanta
{
    class EvaporadorPlantaNodeManager : CustomNodeManager2
    {
        public EvaporadorPlantaNodeManager(IServerInternal server, ApplicationConfiguration configuration)
        :
       base(server, configuration)
        {
            SystemContext.NodeIdFactory = this;

            // set one namespace for the type model and one names for dynamically created nodes.
            string[] namespaceUrls = new string[2];
            namespaceUrls[0] = EvaporadorPlanta.Namespaces.EvaporadorPlanta;
            namespaceUrls[1] = EvaporadorPlanta.Namespaces.EvaporadorPlanta + "/Instance";
            SetNamespaces(namespaceUrls);

            // get the configuration for the node manager.
            m_configuration = configuration.ParseExtension<EvaporadorPlantaServerConfiguration>();

            // use suitable defaults if no configuration exists.
            if (m_configuration == null)
            {
                m_configuration = new EvaporadorPlantaServerConfiguration();
            }
        }

        protected override NodeStateCollection LoadPredefinedNodes(ISystemContext context)
        {
            NodeStateCollection predefinedNodes = new NodeStateCollection();
            predefinedNodes.LoadFromBinaryResource(context,
                "EvaporadorPlanta.PredefinedNodes.uanodes",
                typeof(EvaporadorPlantaNodeManager).GetTypeInfo().Assembly,
                true);

            return predefinedNodes;
        }

        public override void CreateAddressSpace(IDictionary<NodeId, IList<IReference>> externalReferences)
        {
            lock (Lock)
            {
                LoadPredefinedNodes(SystemContext, externalReferences);

                // find the untyped EvaporadorPlanta 1 node that was created when the model was loaded.
                BaseObjectState passiveNode = (BaseObjectState)FindPredefinedNode(new NodeId(EvaporadorPlanta.Objects.PlantaEvaporador1, NamespaceIndexes[0]), typeof(BaseObjectState));

                // convert the untyped node to a typed node that can be manipulated within the server.
                m_evaporadorplanta1 = new EvaporadorPlantaState(null);
                m_evaporadorplanta1.Create(SystemContext, passiveNode);

                // replaces the untyped predefined nodes with their strongly typed versions.
                AddPredefinedNode(SystemContext, m_evaporadorplanta1);

                //m_batchPlant1.StartProcess.OnCallMethod = new GenericMethodCalledEventHandler(OnStartProcess);
                //m_batchPlant1.StopProcess.OnCallMethod = new GenericMethodCalledEventHandler(OnStopProcess);

                //m_simulationTimer = new System.Threading.Timer(DoSimulation, null, 1000, 1000);

            }
        }

        //public void DoSimulation(object state)
        //{
        //    m_batchPlant1.Mixer.LoadcellTransmitter.Output.Value = 85;
        //}

        //private ServiceResult OnStartProcess(ISystemContext context, MethodState method, IList<object> inputArguments,
        //IList<object> outputArguments)
        //{

        //    return ServiceResult.Good;
        //}

        //private ServiceResult OnStopProcess(ISystemContext context, MethodState method, IList<object> inputArguments,
        //IList<object> outputArguments)
        //{

        //    return ServiceResult.Good;
        //}

        private EvaporadorPlantaServerConfiguration m_configuration;
        private static EvaporadorPlantaState m_evaporadorplanta1;
        private System.Threading.Timer m_simulationTimer;

    }
}
