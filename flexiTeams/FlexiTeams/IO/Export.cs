using FlexiTeams.DataClasses.Resource;
using FlexiTeams.FlexiTeamsGraph;
using FlexiTeams.Graph.Nodes;
using FlexiTeams.Inventory;
using FlexiTeams.Util;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

namespace FlexiTeams.IO
{
    public class Export
    {
        public static void Save(string path, string fileName, XDocument document)
        {
            XmlWriterSettings settings = new()
            {
                Indent = true,
                IndentChars = "  ",
                OmitXmlDeclaration = true
            };

            if (document == null) throw new ArgumentNullException();
            using var writer = XmlWriter.Create(new StreamWriter(path + fileName), settings);

            document.Save(writer);

        }

        public static XDocument ToXml(ResourcePool rPool, DataPool dPool, WorkflowPool wPool, TaskPool tPool, AdjListsGraph graph, XmlSchemaSet schemaSet)
        {
            XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");

            XDocument document = new(
                    new XDeclaration("1.0", "UTF-8", "yes"),
                    new XElement("Scenario",
                        new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                        new XAttribute("lang", "en"),
                        new XElement("ResourcePool"),
                        new XElement("DataPool"),
                        new XElement("WorkflowPool"),
                        new XElement("TaskPool"),
                        new XElement("Graph",
                            new XElement("Nodes"),
                            new XElement("Edges")
                        )
                    )
                );

            ToXml(document, rPool);
            ToXml(document, dPool);
            ToXml(document, wPool);
            ToXml(document, tPool);
            ToXml(document, graph);

            Validation.Validate(document, schemaSet);
            return document;
        }

        private static void ToXml(XDocument document, ResourcePool pool)
        {
            var poolNode = document.XPathSelectElement("//ResourcePool");

            foreach (var resource in pool)
            {
                var resourceNode = new XElement("Resource");
                AddResourceAttributs(resourceNode, resource);
                AddResourceElements(resourceNode, resource);
                poolNode.Add(resourceNode);
            }

            void AddResourceAttributs(XElement resourceNode, Resource resource)
            {
                //required Attributes
                resourceNode.Add(
                    new XAttribute("id", resource.Id.ToString()),
                    new XAttribute("age", ISO8601.ToXml(resource.Age.Years, 0, 0, 0, 0, 0)),
                    new XAttribute("maritalState", resource.MaritalState.ToString()),
                    new XAttribute("weeklyHours", ISO8601.ToXml(0, 0, 0, resource.WeeklyHours.Hours, 0, 0)),
                    new XAttribute("yearlyTimeOf", ISO8601.ToXml(0, 0, resource.YearlyTimeOf.Days, 0, 0, 0)),
                    new XAttribute("commuteTime", ISO8601.ToXml(0, 0, 0, 0, resource.CommuteTime.Minutes, 0))
                );

                //optionalAttributes
                if (resource.Prefix != null)
                {
                    resourceNode.Add(new XAttribute("prefix", resource.Prefix.ToString()));
                }
                if (resource.WorkExperience != null)
                {
                    resourceNode.Add(new XAttribute("workExperience", ISO8601.ToXml(resource.WorkExperience.Years, 0, 0, 0, 0, 0)));
                }
                if (resource.TrainingDuration != null)
                {
                    resourceNode.Add(new XAttribute("trainingDuration", ISO8601.ToXml(resource.TrainingDuration.Years, 0, 0, 0, 0, 0)));
                }
                if (resource.Overtime != null)
                {
                    resourceNode.Add(new XAttribute("overtime", ISO8601.ToXml(0, 0, 0, resource.Overtime.Hours, 0, 0)));
                }
                if (resource.YearlyEducation != null)
                {
                    resourceNode.Add(new XAttribute("yearlyEducation", ISO8601.ToXml(0, 0, resource.YearlyEducation.Days, 0, 0, 0)));
                }
            }

            void AddResourceElements(XElement resourceNode, Resource resource)
            {
                //required Elements
                resource.FirstNames.ForEach(firstName =>
                {
                    resourceNode.Add(new XElement("FirstName",
                                            new XAttribute("value", firstName.ToString())
                                        )
                    );
                });

                resource.LastNames.ForEach(lastName =>
                {
                    resourceNode.Add(new XElement("LastName",
                                            new XAttribute("value", lastName.ToString())
                                        )
                    );
                });

                resource.Professions.ForEach(profession =>
                {
                    resourceNode.Add(new XElement("Profession",
                                            new XAttribute("value", profession.ToString())
                                        )
                    );
                });

                resource.Departments.ForEach(department =>
                {
                    resourceNode.Add(new XElement("Department",
                                            new XAttribute("value", department.ToString())
                                        )
                    );
                });

                resource.MeansOfTransport.ForEach(vehicle =>
                {
                    resourceNode.Add(new XElement("MeansOfTransport",
                                            new XAttribute("value", vehicle.ToString())
                                        )
                    );
                });

                resource.Skills.ForEach(skill =>
                {
                    resourceNode.Add(new XElement("Skill",
                                            new XAttribute("value", skill.ToString())
                                        )
                    );
                });

                resource.Traits.ForEach(trait =>
                {
                    resourceNode.Add(new XElement("Trait",
                        new XAttribute("name", trait.Name),
                        new XAttribute("value", trait.Value)
                        )
                    );
                });

                for(int i = 0; i < 7; i++)
                {
                    foreach(var timeIntervall in resource.WorkAgreement[i])
                    {
                        var workAgreementElement = new XElement("WorkAgreement",
                            new XAttribute("value", i + "-" + timeIntervall.ToString()));

                        resourceNode.Add(workAgreementElement);
                    }
                }


                //optional ELements
                resource.Photos?.ForEach(photo =>
                    {
                        resourceNode.Add(new XElement("Photo",
                            new XAttribute("path", photo.Path)));
                    });

                resource.Children?.ForEach(child =>
                    {
                        resourceNode.Add(new XElement("Child",
                            new XAttribute("age", ISO8601.ToXml(child.Age, 0, 0, 0, 0, 0))));
                    });

                resource.Stressors?.ForEach(stressor => { resourceNode.Add(new XElement("Stressor", new XAttribute("value", stressor.ToString()))); });

                resource.PersonalInfos?.ForEach(personalInfo => { resourceNode.Add(new XElement("PersonalInfo", new XAttribute("value", personalInfo.ToString()))); });

                resource.Studies?.ForEach(study =>
                {
                    var studyElement = new XElement("Studies",
                        new XAttribute("name", study.ToString())
                        );

                    if (study.Location != null)
                    {
                        studyElement.Add(new XAttribute("location", study.Location));
                        resourceNode.Add(studyElement);
                    }
                    else
                    {
                        resourceNode.Add(studyElement);
                    }
                });

                resource.Trainings?.ForEach(training => { resourceNode.Add(new XElement("Training", new XAttribute("value", training.ToString()))); });

                resource.Qualifications?.ForEach(qualification => { resourceNode.Add(new XElement("Qualification", new XAttribute("value", qualification.ToString()))); });

                resource.AdditionalJobs?.ForEach(additionalJob =>
                    {
                        var additionalJobElement = new XElement("AdditionalJob",
                            new XAttribute("name", additionalJob.ToString())
                            );
                        if (additionalJob.YearlyRequiredDays != null)
                        {
                            var locationAttribute = new XAttribute("location", ISO8601.ToXml(0, 0, (int) additionalJob.YearlyRequiredDays, 0, 0, 0));
                            additionalJobElement.Add(locationAttribute);
                        }

                        resourceNode.Add(additionalJobElement);
                    });

                resource.ProfessionalInfos?.ForEach(professionalInfo => { resourceNode.Add(new XElement("ProfessionalInfo", new XAttribute("value", professionalInfo.ToString()))); });
            }
        }

        private static void ToXml(XDocument document, DataPool pool)
        {
            var poolNode = document.XPathSelectElement("//DataPool");

            foreach (var data in pool)
            {
                poolNode.Add(
                    new XElement("Data",
                        new XAttribute("id", data.Id.ToString()),
                        new XAttribute("type", data.Name.ToString())

                        )
                    );
            }


        }

        private static void ToXml(XDocument document, WorkflowPool pool)
        {
            var poolNode = document.XPathSelectElement("//WorkflowPool");

            foreach (var workflow in pool)
            {

                var workflowNode = new XElement("Workflow",
                        new XAttribute("id", workflow.Id.ToString()),
                        new XAttribute("type", workflow.Type.ToString()),
                        new XAttribute("venue", workflow.Venue.ToString())
                );

                if (workflow.Minutes != 0) workflowNode.Add(new XAttribute("duration", ISO8601.ToXml(0, 0, 0, 0,workflow.Minutes, 0)));

                poolNode.Add(workflowNode);

            }
        }

        private static void ToXml(XDocument document, TaskPool pool)
        {
            var poolNode = document.XPathSelectElement("//TaskPool");

            foreach (var task in pool)
            {
                var taskNode = new XElement("Task",
                        new XAttribute("id", task.Id.ToString()),
                        new XAttribute("type", task.Type.ToString()),
                        new XAttribute("venue", task.Venue.ToString())
                        );

                if (task.Minutes != 0) taskNode.Add(new XAttribute("duration", ISO8601.ToXml(0, 0, 0, 0, task.Minutes, 0)));

                if (task.RequiredData.Any())
                {
                    foreach (var name in task.RequiredData)
                    {
                        var ConsumedDataNode = new XElement("ConsumedData",
                        new XAttribute("type", name.ToString()));
                        taskNode.Add(ConsumedDataNode);
                    }
                }

                if (task.RequiredProfessions.Any())
                {
                    foreach (var name in task.RequiredProfessions)
                    {
                        var consumedResourceNode = new XElement("ConsumedResource",
                        new XAttribute("type", name.ToString()));
                        taskNode.Add(consumedResourceNode);
                    }
                }

                poolNode.Add(taskNode);
            }
        }

        private static void ToXml(XDocument document, AdjListsGraph graph)
        {
            AddNodes(graph);
            AddEdges(graph);

            void AddNodes(AdjListsGraph graph)
            {
                var nodesNode = document.XPathSelectElement("//Graph/Nodes");

                foreach (var node in graph.Nodes)
                {
                    if (node is WorkflowNode wNode)
                    {
                        nodesNode.Add(new XElement("WorkflowNode",
                            new XAttribute("idref", wNode._id),
                            new XAttribute("startNode", wNode.StartNodeId)
                            )
                        );
                    }
                    if (node is TaskNode)
                    {
                        nodesNode.Add(new XElement("TaskNode",
                            new XAttribute("idref", node._id)));
                    }
                    if (node is ResourceNode)
                    {
                        nodesNode.Add(new XElement("ResourceNode",
                            new XAttribute("idref", node._id)));
                    }
                    if (node is DataNode)
                    {
                        nodesNode.Add(new XElement("DataNode",
                            new XAttribute("idref", node._id)));
                    }
                }

            }
            void AddEdges(AdjListsGraph graph)
            {
                var edgesNode = document.XPathSelectElement("//Graph/Edges");

                foreach (var u in graph.Nodes)
                {
                    foreach (var v in graph.Adj(u))
                    {
                        edgesNode.Add(new XElement("Edge",
                            new XAttribute("idref1", u._id),
                            new XAttribute("idref2", v._id))
                            );
                    }
                }
            }
        }
    }
}