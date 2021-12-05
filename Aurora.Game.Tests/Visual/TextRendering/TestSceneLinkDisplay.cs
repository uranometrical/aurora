using Aurora.Game.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osuTK.Graphics;

namespace Aurora.Game.Tests.Visual.TextRendering
{
    public class TestSceneLinkDisplay : AuroraTestScene
    {
        public TestSceneLinkDisplay()
        {
            AddStep("display colored links", () =>
            {
                LinkTextFlowContainer container = new()
                {
                    RelativeSizeAxes = Axes.Both,
                };

                Add(container);

                container.AddText("The following are a list of links with special properties:");
                container.NewLine();
                container.AddLink("I can be hovered over!", "You are hovering over me!", ""); // cba to add a tooltip container lol
                container.NewLine();
                container.AddLink("I have a different hover color!", "", "", null, null, Color4.Red);
                container.NewLine();
                container.AddLink("I have a different idle color", "", "", null, null, null, Color4.Yellow);
                container.NewLine();
                container.AddLink("I have both!", "", "", null, null, Color4.Red, Color4.Yellow);
                container.NewLine();
                container.AddLink("I have a gradient and open Google!", "Open Google", "https://google.com", null, null, ColourInfo.GradientHorizontal(Colour4.Red, Colour4.Yellow));
            });
        }
    }
}
