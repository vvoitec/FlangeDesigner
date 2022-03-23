using FlangeDesigner.AbstractEngine;
using FlangeDesigner.Main.Domain.Entities;
using TechTalk.SpecFlow;

namespace FlangeDesigner.Spec.Hooks
{
    [Binding]
    public class ProjectHooks
    {
        private readonly IEngine _engine;

        public ProjectHooks(IEngine engine)
        {
            _engine = engine;
        }
        
        [BeforeScenario]
        [Scope(Tag = "loadProject")]
        public void LoadProject(ScenarioContext scenarioContext)
        {
            var projectPath = "D:\\dev\\FlangeDesigner\\FlangeDesigner.Main\\szablon.sldprt";
            var project = Project.Create(_engine);
            project.Load(projectPath);
            scenarioContext.Add("project", project);
        }
    }
}