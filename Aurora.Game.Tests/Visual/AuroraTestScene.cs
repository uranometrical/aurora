using osu.Framework.Testing;

namespace Aurora.Game.Tests.Visual
{
    public class AuroraTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new AuroraTestSceneTestRunner();

        private class AuroraTestSceneTestRunner : AuroraGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}
