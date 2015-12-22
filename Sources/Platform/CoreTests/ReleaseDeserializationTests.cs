﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodAI.Core.Dashboard;
using GoodAI.Core.Execution;
using GoodAI.Core.Memory;
using GoodAI.Core.Nodes;
using GoodAI.Core.Task;
using GoodAI.Core.Utils;
using Xunit;

namespace CoreTests
{
    public class ReleaseDeserializationTests
    {
        [Fact]
        public void ReleaseDeserializationTest()
        {
            const string brainPath = @"Data\release-deserialization-test.brain";

            using (var runner = new MyProjectRunner())
            {
                runner.OpenProject(Path.GetFullPath(brainPath));

                // Must not fail.
                runner.RunAndPause(1);

                MyProject project = runner.Project;

                CheckDashboard(project);

                CheckTensors(project);
            }
        }

        private static void CheckDashboard(MyProject project)
        {
            Dashboard dashboard = project.Dashboard;

            // Check the dashboard for expected properties.
            MyWorkingNode codeBookNode1 = project.Network.GetChildNodeById(338) as MyWorkingNode;
            MyTask performTask1 = codeBookNode1.GetTaskByPropertyName("PerformTask");
            var property1 = dashboard.Get(performTask1, "SimilarityOperator");

            Assert.NotNull(property1);

            MyWorkingNode codeBookNode2 = project.Network.GetChildNodeById(331) as MyWorkingNode;
            MyTask performTask2 = codeBookNode2.GetTaskByPropertyName("PerformTask");
            var property2 = dashboard.Get(performTask2, "SimilarityOperator");

            Assert.NotNull(property2);

            MyWorkingNode hiddenLayerNode = project.Network.GetChildNodeById(451) as MyWorkingNode;
            MyTask shareWeightsTask = hiddenLayerNode.GetTaskByPropertyName("ShareWeightsTask");
            var property3 = dashboard.Get(shareWeightsTask, "SourceNodeName");

            Assert.NotNull(property3);

            MyWorkingNode nodeGroup = project.Network.GetChildNodeById(359) as MyWorkingNode;
            var property4 = dashboard.Get(nodeGroup, "InputBranches");

            Assert.NotNull(property4);

            // Check the grouped dashboard for expected properties.
            GroupDashboard groupedDashboard = project.GroupedDashboard;

            var group = groupedDashboard.Get("f6af17f3-82b0-42b6-89b0-a4eaf6432316");

            Assert.NotNull(@group);

            Assert.True(@group.GroupedProperties.Contains(property1));
            Assert.True(@group.GroupedProperties.Contains(property2));
        }

        private static void CheckTensors(MyProject project)
        {
            TensorDimensions dimensions;

            MyWorkingNode kwmNode = project.Network.GetChildNodeById(330) as MyWorkingNode;
            dimensions = kwmNode.GetOutput(0).Dims;
            Assert.Equal("*, 32", dimensions.ToString());

            MyWorkingNode absoluteValue1 = project.Network.GetChildNodeById(411) as MyWorkingNode;
            dimensions = absoluteValue1.GetOutput(0).Dims;
            Assert.Equal("32, *", dimensions.ToString());

            MyWorkingNode absoluteValue2 = project.Network.GetChildNodeById(413) as MyWorkingNode;
            dimensions = absoluteValue2.GetOutput(0).Dims;
            Assert.Equal("32, 32", dimensions.ToString());
        }
    }
}
